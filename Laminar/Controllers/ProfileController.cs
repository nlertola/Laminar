using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Laminar.Data.Interfaces;
using Laminar.Models;
using Laminar.Interfaces;
using Laminar.ViewModels;
using Laminar.ViewModels.Views;
using System.IO;

namespace Laminar.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IRepository<User> _userRepository;
        private readonly IUserService _userService;
        public ProfileController(IRepository<User> userRepository,
            IUserService userService)
        {
            _userRepository = userRepository;
            _userService = userService;
        }

        public IActionResult Index()
        {
            User user = _userService.GetUser(User.Identity.Name);

            if(user == null)
            {
                return RedirectToAction("Create");
            }

            ViewProfileViewModel viewModel = new ViewProfileViewModel(user);

            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View(new CreateEditUserViewModel(User.Identity.Name));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateEditUserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                User user = new User();
                user.EmailAddress = User.Identity.Name;
                user.FirstName = viewModel.FirstName;
                user.LastName = viewModel.LastName;

                if (viewModel.PhotoUpload != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        viewModel.PhotoUpload.CopyTo(memoryStream);
                        user.ProfilePicture = memoryStream.ToArray();
                        user.ProfilePictureContentType = viewModel.PhotoUpload.ContentType;
                    }
                }

                _userRepository.Add(user);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Edit()
        {
            User user = _userService.GetUser(User.Identity.Name);

            if (user == null)
            {
                return RedirectToAction("Create");
            }

            return View(new CreateEditUserViewModel(user));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CreateEditUserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                User user = _userService.GetUser(User.Identity.Name);

                if (user == null)
                {
                    return RedirectToAction("Create");
                }

                user.EmailAddress = User.Identity.Name;
                user.FirstName = viewModel.FirstName;
                user.LastName = viewModel.LastName;

                if (viewModel.PhotoUpload != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        viewModel.PhotoUpload.CopyTo(memoryStream);
                        user.ProfilePicture = memoryStream.ToArray();
                        user.ProfilePictureContentType = viewModel.PhotoUpload.ContentType;
                    }
                }

                _userRepository.Update(user);
            }
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> GetProfilePicture(int id)
        {
            User user = _userService.GetUser(User.Identity.Name);

            if (user == null || 
                user.ID != id ||
                user.ProfilePicture == null)
            {
                return NotFound();
            }

            return File(user.ProfilePicture.ToArray(),
                user.ProfilePictureContentType);
        }
    }
}