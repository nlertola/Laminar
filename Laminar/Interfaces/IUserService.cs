using Laminar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laminar.Interfaces
{
    public interface IUserService
    {
        User GetUser(string identity);
    }
}
