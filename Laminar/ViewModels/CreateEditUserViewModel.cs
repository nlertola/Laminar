using Laminar.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Laminar.ViewModels
{
    public class CreateEditUserViewModel
    {
        public CreateEditUserViewModel()
        {

        }

        public CreateEditUserViewModel(string identity)
        {
            EmailAddress = identity;
        }

        public CreateEditUserViewModel(User user)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            EmailAddress = user.EmailAddress;
            ProfilePicture = user.ProfilePicture;
            ProfilePictureContentType = user.ProfilePictureContentType;
        }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Display(Name = "Profile Picture")]
        public byte[] ProfilePicture { get; set; }
        public string ProfilePictureContentType { get; set; }
        public IFormFile PhotoUpload { get; set; }
    }
}
