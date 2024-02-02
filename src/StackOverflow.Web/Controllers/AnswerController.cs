using Autofac;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackOverflow.DAL.Enums;
using StackOverflow.Web.Extensions;
using StackOverflow.Web.Models;
using StackOverflow.Web.Models.AnswerModels;
using StackOverflow.Web.Models.QuestionModels;

namespace StackOverflow.Web.Controllers
{
    [Authorize]
    public class AnswerController : Controller
    {
        private ILifetimeScope _scope;
        private ILogger<AnswerController> _logger;
        public AnswerController(ILifetimeScope scope, ILogger<AnswerController> logger) 
        {
            _scope = scope;
            _logger = logger;
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] AnswerCreateModel model)
        {
            var stringUserId = User.Identity.GetUserId();
            var userId = Guid.Parse(stringUserId);
            model.UserId = userId;
            if (ModelState.IsValid)
            {
                model.ResolveDependency(_scope);
                
                await model.CreateAnswer();
                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Message = "Answer Added Successfully",
                    Type = ResponseTypes.Success,
                });
            }
            else
            {
                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Message = "Answer is Invalid",
                    Type = ResponseTypes.Danger,
                });
            }
            return RedirectToAction("Details", "Question", new { id = model.QuestionId });
        }

        [ValidateAntiForgeryToken]
        public string UpdateVote(Guid id, string voteType)
        {
            string uId = User.Identity.GetUserId();
            Guid userId = Guid.Parse(uId);
            var model = _scope.Resolve<AnswerVoteModel>();
            model.UserId = userId;
            model.AnswerId = id;
            model.VoteType = Enum.Parse<VoteType>(voteType);
            return model.UpdateVote().Result.ToString();
        }
    }
}
