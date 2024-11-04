using DefaultNamespace;
using Microsoft.EntityFrameworkCore;
using Todo.Dtos.Roles;
using Todo.Models;

namespace Todo.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<Sprints> Sprints { get; set; }
        public DbSet<SubTask> SubTask { get; set; }

    }
}