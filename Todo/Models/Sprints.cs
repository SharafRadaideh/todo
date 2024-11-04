using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Todo.Models
{
    public class Sprints
    {
    public int Id { get; set; }
    public string?  SprintName { get; set; }
    public string? SprintStatus { get; set; }
    public string? discretion {get;set;}
    //public DateOnlyToStringConverter? DateToEnd {get;set;}
    }
}