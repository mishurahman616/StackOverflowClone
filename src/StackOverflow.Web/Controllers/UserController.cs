using Autofac;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackOverflow.Web.Models;

namespace StackOverflow.Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        public ILifetimeScope _scope;
        public UserController(ILifetimeScope scope)
        {
            _scope = scope;
        }
        public async Task<IActionResult> MyQuestion()
        {
            var model = _scope.Resolve<UserModel>();
            var user = await model.GetUserById(Guid.Parse(User.Identity.GetUserId()));
            return View(user);
        }

        public async Task<IActionResult> MyAnswer()
        {
            var model = _scope.Resolve<UserModel>();
            var user = await model.GetUserById(Guid.Parse(User.Identity.GetUserId()));
            return View(user);
        }

        public async Task<IActionResult> MyVotedQuestion()
        {
            var model = _scope.Resolve<UserModel>();
            var user = await model.GetUserById(Guid.Parse(User.Identity.GetUserId()));
            return View(user);
        }
        public async Task<IActionResult> MyVotedAnswer()
        {
            var model = _scope.Resolve<UserModel>();
            var user = await model.GetUserById(Guid.Parse(User.Identity.GetUserId()));
            return View(user);
        }
    }
}
