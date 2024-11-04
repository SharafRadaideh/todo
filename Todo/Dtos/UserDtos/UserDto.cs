using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Todo.Dtos.Roles;
using Todo.Models;

namespace Todo.Dtos.UserDto
{
    public class UserDto
    {
    public int Id { get; set; }
    public string? FirstName { get; set; } 
    public string? MiddleName { get; set; } 
    public string? LastName { get; set; } 
    public string? PhoneNumber { get; set; }
    public string Email { get; set; } = String.Empty;
    public string? Password { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public string NormalizedEmail => Email.ToUpper();
    public RolesEnum? Role { get;  set; }
    public string RoleName { get; set; } = "Unknown";

    }
}