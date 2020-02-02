using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Laminar.Data.Interfaces;
using Laminar.Interfaces;
using Laminar.Models;
using Laminar.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Laminar.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IRepository<Project> _projectRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IUserService _userService;
        public ProjectController(IRepository<Project> projectRepository,
            IRepository<User> userRepository,
            IUserService userService)
        {
            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _userService = userService;
        }
        public IActionResult Index()
        {
            User user = _userService.GetUser(User.Identity.Name);

            if (user == null)
            {
                return RedirectToAction("Create", "Profile");
            }

            var projects = _projectRepository.AllQueryable().Where(p => p.UserId == user.ID).ToHashSet();

            return View(projects);
        }

        public IActionResult Create()
        {
            User user = _userService.GetUser(User.Identity.Name);

            if (user == null)
            {
                return RedirectToAction("Create", "Profile");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateEditProjectViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                User user = _userService.GetUser(User.Identity.Name);

                if (user == null)
                {
                    return RedirectToAction("Create", "Profile");
                }

                Project project = new Project();
                project.UserId = user.ID;
                project.Title = viewModel.Title;
                project.Description = viewModel.Description;

                _projectRepository.Add(project);

                user.Projects.Add(project);
                _userRepository.Update(user);

                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

    }
}