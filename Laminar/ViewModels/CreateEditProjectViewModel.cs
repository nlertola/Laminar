using Laminar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laminar.ViewModels
{
    public class CreateEditProjectViewModel
    {
        public CreateEditProjectViewModel()
        {

        }

        public CreateEditProjectViewModel(Project project)
        {
            ID = project.ID;
            Title = project.Title;
            Description = project.Description;
        }

        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
