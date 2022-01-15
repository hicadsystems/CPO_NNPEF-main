using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NNPEFWEB.Models;
using NNPEFWEB.Service;

namespace NNPEFWEB.Controllers
{

    public class BaseController : Controller
    {
        private readonly IUserService userService;


        public BaseController(IUserService userService)
        {
            this.userService = userService;
        }

       
        public string RefererUrl()
        {
            return Request.Headers["Referer"].ToString();
        }

        public async Task<User> GetCurrentUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if(userId == null)
            {
                return null;
            }
            return await userService.GetUserWithRoles(int.Parse(userId));            
        }

    }
}