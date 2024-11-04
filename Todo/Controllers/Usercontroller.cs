using api.interfaces;
using DefaultNamespace;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.Dtos.LoginDtos;
using Todo.Dtos.Roles;
using Todo.Dtos.UserDto;
using Todo.Dtos.UserDtos;
using Todo.Mapper;
using System.Linq;


namespace Todo.Controllers
{
    [ApiController]
    [Route("api/User")]
    public class Usercontroller : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ITokenService _tokenService;

        public Usercontroller(ApplicationDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Create([FromBody] CreateUserDto CreateDto)
        {
            var userModel = CreateDto.ToCreateUserDto();
            _context.Users.Add(userModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = userModel.Id }, userModel.ToUserDto());
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            // make the password hash
            // Build in library
            // Security Wise.
            var userModel = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == loginDto.Email && x.Password == loginDto.Password);

            if (userModel != null)
            {
                var token = _tokenService.CreateToken(userModel);

                return Ok(new
                {
                    Message = "User Login",
                    Token = token
                });
            }

            return NoContent();
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAll()
        {
            // add a role, all users saw by super admin
            var users = await _context.Users.ToListAsync();
            var userDtos = users.Select(u => u.ToUserDto()).ToList();
            return Ok(userDtos);
        }

        [HttpGet("GetUserBy/{id}")]
        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var users = await _context.Users
                                            .FirstOrDefaultAsync(s => s.Id == id);

            if (users == null)
            {
                return NotFound();
            }
            var userDtos = users.ToUserDto();
            return Ok(userDtos);
        }

        [HttpPut("UpdateBy/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateUserDto updateDto)
        {
            User? userModel;
            userModel = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (userModel == null)
            {
                return NotFound();
            }

            userModel.FirstName = updateDto.FirstName;
            userModel.MiddleName = updateDto.MiddleName;
            userModel.LastName = updateDto.LastName;
            userModel.PhoneNumber = updateDto.PhoneNumber;
            // add unique constrains 
            userModel.Email = updateDto.Email;
            userModel.Password = updateDto.Password;
            await _context.SaveChangesAsync();
            return Ok(userModel.ToUserDto());
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Policy = "SharedAccess")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var userModel = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (userModel == null)
            {
                return NotFound();
            }
            // In delete case : we create a column by default has a value = False
            // field name : IsDeleted of Bool
            // Upon click on delete, you conver the field value to true.
            _context.Users.Remove(userModel);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut("GiveRoleBy/{id}")]
        public async Task<IActionResult> UpdateUserRole([FromRoute] int id, [FromBody] RoleUpdateDto roleUpdateDto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound("User not found");

            user.Role = roleUpdateDto.Role;
            user.RoleName = user.Role.ToString() ?? RolesEnum.UnKnown.ToString();

            await _context.SaveChangesAsync();

            return Ok(user);
        }

        [HttpGet("GetUsersBy/{roleId}")]
        public ActionResult<IEnumerable<UserDto>> GetUsersByRole(int roleId)
        {
            var users = _context.Users
                .Where(u => u.Role == (RolesEnum)roleId)
                .ToList();
            var userDtos = users.Select(u => u.ToUserDto()).ToList();
            return Ok(userDtos);
        }
    }
}