using System.Runtime.InteropServices;
using Microsoft.Data.SqlClient;
using Todo.Controllers;
using Todo.Dtos.Roles;
using Todo.Models;

namespace DefaultNamespace;

public class User
{
    public int Id { get; set; }
    public string? FirstName { get; set; } 
    public string? MiddleName { get; set; } 
    public string? LastName { get; set; } 
    public string? PhoneNumber { get; set; }
    public string Email { get; set; } = string.Empty;
    public string? Password { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public string NormalizedEmail => Email.ToUpper();
    
    public RolesEnum? Role {get; set;} 

    // TODO New Maigrations
    public string? RoleName { get; set; } 
}