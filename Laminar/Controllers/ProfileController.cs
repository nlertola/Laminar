using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Laminar.Data.Interfaces;
using Laminar.Models;

namespace Laminar.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IRepository<User> _userRepository;
        public ProfileController(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        public IActionResult Index()
        {
            ViewBag.User = User.Identity.Name;
            return View();
        }
    }
}