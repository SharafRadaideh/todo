using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DefaultNamespace;
using Todo.Dtos.Roles;
using Todo.Dtos.UserDto;
using Todo.Dtos.UserDtos;

namespace Todo.Mapper
{
    public static class UserMapper
    {
        public static UserDto ToUserDto(this User userModel)
        {
            return new UserDto
            {
                Id = userModel.Id,
                FirstName = userModel.FirstName,
                MiddleName = userModel.MiddleName,
                LastName = userModel.LastName,
                PhoneNumber = userModel.PhoneNumber,
                Email = userModel.Email,
                Password = userModel.Password,
                CreatedOn = userModel.CreatedOn,
                Role = userModel.Role,
                RoleName = userModel.Role.ToString()!
            };
        }

        public static User ToCreateUserDto ( this CreateUserDto createUserDto)
        {
            return new User 
            {
                FirstName = createUserDto.FirstName,
                MiddleName = createUserDto.MiddleName,
                LastName = createUserDto.LastName,
                PhoneNumber = createUserDto.PhoneNumber,
                Email = createUserDto!.Email!,
                Password = createUserDto.Password,
                Role = RolesEnum.UnKnown,
                RoleName = createUserDto!.Role!.ToString()!
            };
        }
    }
}