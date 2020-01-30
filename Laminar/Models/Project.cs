using Laminar.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laminar.Models
{
    public class Project : BaseModel
    {
        public Project()
        {
            DateCreated = DateTime.Now;
            DateUpdated = DateTime.Now;
            Tasks = new HashSet<Task>();
        }

        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
