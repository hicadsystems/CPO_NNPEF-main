


using NNPEFWEB.Models;
using System.Collections.Generic;

namespace NNPEFWEB.ViewModels.Users
{
    public class UsersViewModel
    {
        public List<User> Users { get; set; }

        public IEnumerable<Role> Roles { get; set; }

    }
}
