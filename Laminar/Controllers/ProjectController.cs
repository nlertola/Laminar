﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Laminar.Data.Interfaces;
using Laminar.Interfaces;
using Laminar.Models;
using Laminar.ViewModels;
using Laminar.ViewModels.Views;
using Microsoft.AspNetCore.Mvc;

namespace Laminar.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IRepository<Project> _projectRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Models.Task> _taskRepository;
        private readonly IUserService _userService;
        public ProjectController(IRepository<Project> projectRepository,
            IRepository<User> userRepository,
            IRepository<Models.Task> taskRepository,
            IUserService userService)
        {
            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _taskRepository = taskRepository;
            _userService = userService;
        }
        public IActionResult Index()
        {
            User user = _userService.GetUser(User.Identity.Name);

            if (user == null)
            {
                return RedirectToAction("Create", "Profile");
            }

            var projects = _projectRepository.AllQueryable().Where(p => p.UserId == user.ID)
                .OrderByDescending(p => p.DateUpdated).ToHashSet();

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

        public IActionResult Edit(int id)
        {
            User user = _userService.GetUser(User.Identity.Name);

            if (user == null)
            {
                return RedirectToAction("Create", "Profile");
            }

            Project project = _projectRepository.Get(id);
            if(project == null || project.UserId != user.ID)
            {
                RedirectToAction("Index");
            }

            CreateEditProjectViewModel viewModel = new CreateEditProjectViewModel(project);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CreateEditProjectViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                User user = _userService.GetUser(User.Identity.Name);

                if (user == null)
                {
                    return RedirectToAction("Create", "Profile");
                }

                Project project = _projectRepository.Get(viewModel.ID);
                if (project == null || project.UserId != user.ID)
                {
                    RedirectToAction("Index");
                }

                project.UserId = user.ID;
                project.Title = viewModel.Title;
                project.Description = viewModel.Description;
                project.DateUpdated = DateTime.Now;

                _projectRepository.Update(project);

                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        public IActionResult Board(int id)
        {
            User user = _userService.GetUser(User.Identity.Name);

            if (user == null)
            {
                return RedirectToAction("Create", "Profile");
            }

            Project project = _projectRepository.Get(id);
            if (project == null || project.UserId != user.ID)
            {
                RedirectToAction("Index");
            }

            ViewProjectBoardViewModel viewModel = new ViewProjectBoardViewModel(project);

            return View(viewModel);
        }

        public IActionResult Delete(int id)
        {
            User user = _userService.GetUser(User.Identity.Name);
            
            if (user == null)
            {
                return RedirectToAction("Create", "Profile");
            }

            Project project = _projectRepository.Get(id);

            if (project == null || project.UserId != user.ID)
            {
                RedirectToAction("Index", "Profile");
            }

            foreach(Models.Task task in project.Tasks.ToList())
            {
                _taskRepository.Delete(task);
            }

            _projectRepository.Delete(project);

            return RedirectToAction("Index", "Profile");
        }

    }
}