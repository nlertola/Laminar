using Laminar.Data.Enums;
using Laminar.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laminar.Models
{
    public class Task : BaseModel
    {
        public Task()
        {
            DateCreated = DateTime.Now;
            DateUpdated = DateTime.Now;
        }

        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public CurrentTaskStatus Status { get; set; }
        public TaskType Type { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
