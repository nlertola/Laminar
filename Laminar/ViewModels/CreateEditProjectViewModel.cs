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

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
