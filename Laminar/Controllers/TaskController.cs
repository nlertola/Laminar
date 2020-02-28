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
    public class TaskController : Controller
    {
        private readonly IRepository<Models.Task> _taskRepository;
        private readonly IRepository<Project> _projectRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IUserService _userService;

        public TaskController(IRepository<Models.Task> taskRepository,
            IRepository<Project> projectRepository,
            IRepository<User> userRepository,
            IUserService userService)
        {
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(int id)
        {
            User user = _userService.GetUser(User.Identity.Name);

            if (user == null)
            {
                return RedirectToAction("Create", "Profile");
            }

            Project project = _projectRepository.Get(id);
            if(project == null || project.UserId != user.ID)
            {
                RedirectToAction("Index", "Profile");
            }
            CreateEditTaskViewModel viewModel = new CreateEditTaskViewModel(project);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateEditTaskViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                User user = _userService.GetUser(User.Identity.Name);

                if (user == null)
                {
                    return RedirectToAction("Create", "Profile");
                }

                Project project = _projectRepository.Get(viewModel.ProjectId);
                if (project == null || project.UserId != user.ID)
                {
                    RedirectToAction("Index", "Profile");
                }

                Models.Task task = new Models.Task();
                task.UserId = viewModel.UserId;
                task.ProjectId = viewModel.ProjectId;
                task.Title = viewModel.Title;
                task.Description = viewModel.Description;
                task.Type = viewModel.Type;
                task.Status = Data.Enums.CurrentTaskStatus.BackLog;

                _taskRepository.Add(task);

                project.Tasks.Add(task);
                project.DateUpdated = DateTime.Now;
                _projectRepository.Update(project);

                return RedirectToAction("Board", "Project", new { id = project.ID });
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

            Models.Task task = _taskRepository.Get(id);
            if (task == null || task.UserId != user.ID)
            {
                RedirectToAction("Index", "Profile");
            }
            CreateEditTaskViewModel viewModel = new CreateEditTaskViewModel(task);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CreateEditTaskViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                User user = _userService.GetUser(User.Identity.Name);

                if (user == null)
                {
                    return RedirectToAction("Create", "Profile");
                }

                Project project = _projectRepository.Get(viewModel.ProjectId);
                if (project == null || project.UserId != user.ID)
                {
                    RedirectToAction("Index", "Profile");
                }

                Models.Task task = _taskRepository.Get(viewModel.ID);
                if(task == null || task.UserId != user.ID)
                {
                    RedirectToAction("Index", "Profile");
                }

                task.UserId = viewModel.UserId;
                task.ProjectId = viewModel.ProjectId;
                task.Title = viewModel.Title;
                task.Description = viewModel.Description;
                task.Type = viewModel.Type;
                task.Status = Data.Enums.CurrentTaskStatus.BackLog;

                _taskRepository.Update(task);

                project.DateUpdated = DateTime.Now;
                _projectRepository.Update(project);

                return RedirectToAction("Board", "Project", new { id = project.ID });
            }
            return View(viewModel);
        }

        public IActionResult Delete(int id)
        {
            Models.Task task = _taskRepository.Get(id);
            User user = _userService.GetUser(User.Identity.Name);
            Project project = _projectRepository.Get(task.ProjectId);
            if (user == null)
            {
                return RedirectToAction("Create", "Profile");
            }

            if (task == null || project == null || 
                project.UserId != user.ID ||
                task.UserId != user.ID)
            {
                RedirectToAction("Index", "Profile");
            }

            _taskRepository.Delete(task);

            return RedirectToAction("Board", "Project", new { id = project.ID });
        }
    }
}