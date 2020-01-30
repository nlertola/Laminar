using Laminar.Data.Interfaces;
using Laminar.Interfaces;
using Laminar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laminar.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public User GetUser(string identity)
        {
            return _userRepository.AllQueryable().FirstOrDefault(u => u.EmailAddress == identity);
        }
    }
}
