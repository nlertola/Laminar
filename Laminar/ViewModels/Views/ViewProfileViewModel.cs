using Laminar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laminar.ViewModels.Views
{
    public class ViewProfileViewModel
    {
        public ViewProfileViewModel()
        {

        }

        public ViewProfileViewModel(User user)
        {
            ID = user.ID;
            FirstName = user.FirstName;
            LastName = user.LastName;
            EmailAddress = user.EmailAddress;
            ProfilePicture = user.ProfilePicture;
            ProfilePictureContentType = user.ProfilePictureContentType;
        }

        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public byte[] ProfilePicture { get; set; }
        public string ProfilePictureContentType { get; set; }
    }
}
