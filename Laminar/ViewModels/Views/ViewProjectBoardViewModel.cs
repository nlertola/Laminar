using Laminar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laminar.ViewModels.Views
{
    public class ViewProjectBoardViewModel
    {
        public ViewProjectBoardViewModel()
        {

        }

        public ViewProjectBoardViewModel(Project project)
        {
            Project = project;
            DateCreated = project.DateCreated;
            DateUpdated = project.DateUpdated;
        }

        public Project Project { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
