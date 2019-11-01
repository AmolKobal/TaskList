using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace TaskList.Models
{
    public class Task
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Display(Name = "Complete Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]

        public DateTime CompleteDate { get; set; }

        [Display(Name = "Completed?")]
        public bool Complete { get; set; }
    }

    public class TaskContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }
    }
}