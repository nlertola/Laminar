using Laminar.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Laminar.Models
{
    public class User : BaseModel
    {
        public User()
        {
            DateJoined = DateTime.Now;
            Projects = new HashSet<Project>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        public byte[] ProfilePicture { get; set; }
        public string ProfilePictureContentType { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public DateTime DateJoined { get; set; }
    }
}
