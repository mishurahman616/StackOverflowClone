using Autofac;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackOverflow.DAL.Entities;
using StackOverflow.Web.Extensions;
using StackOverflow.Web.Models;
using StackOverflow.Web.Models.AnswerModels;
using StackOverflow.Web.Models.QuestionModels;

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
 
        public async Task<IActionResult> DeleteAnswer(Guid id)
        {
            try
            {
                var model = _scope.Resolve<AnswerListModel>();
                await model.DeleteAnswerByUser(Guid.Parse(User.Identity.GetUserId()), id);
                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Message = "Answer Deleted Successfully",
                    Type = ResponseTypes.Success
                });
            }
            catch (Exception ex)
            {
                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Message = "Answer Delete Failed",
                    Type = ResponseTypes.Danger
                });
            }

            return RedirectToAction("MyAnswer");
        }
    }
}
