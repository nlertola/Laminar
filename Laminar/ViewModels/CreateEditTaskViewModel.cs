using Laminar.Data.Enums;
using Laminar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laminar.ViewModels
{
    public class CreateEditTaskViewModel
    {
        public CreateEditTaskViewModel()
        {

        }

        public CreateEditTaskViewModel(Project project)
        {
            UserId = project.UserId;
            ProjectId = project.ID;
        }

        public CreateEditTaskViewModel(Models.Task task)
        {
            ID = task.ID;
            UserId = task.UserId;
            ProjectId = task.ID;
            Title = task.Title;
            Description = task.Description;
            Status = task.Status;
            Type = task.Type;
        }

        public int ID { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public CurrentTaskStatus Status { get; set; }
        public TaskType Type { get; set; }
    }
}
