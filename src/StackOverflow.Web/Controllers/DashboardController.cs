using Autofac;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackOverflow.Web.Models;


namespace StackOverflow.Web.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        private ILifetimeScope _scope;
        public DashboardController(ILifetimeScope scope, ILogger<DashboardController> logger)
        {
            _scope = scope;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            var userModel = _scope.Resolve<UserModel>();
            var user = await userModel.GetUserById(Guid.Parse(User.Identity.GetUserId()));
            return View(user);
        }
    }
}
