using Autofac;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackOverflow.DAL.Enums;
using StackOverflow.Web.Extensions;
using StackOverflow.Web.Models;
using StackOverflow.Web.Models.QuestionModels;
using System.Reflection;

namespace StackOverflow.Web.Controllers
{
    [Authorize]
    public class QuestionController : Controller
    {
        private readonly ILifetimeScope _scope;
        ILogger<QuestionController> _logger;
        public QuestionController(ILifetimeScope scope, ILogger<QuestionController> logger)
        {
            _scope = scope;
            _logger = logger;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var model = _scope.Resolve<QuestionListModel>();
            await model.LoadQuestions();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(QuestionCreateModel model)
        {
            try
            {
                model.ResolveDependency(_scope);
                var userId = User.Identity.GetUserId();
                model.UserId = Guid.Parse(userId);
                await model.CreateQuestion();
                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Message = "Question Create Successfully",
                    Type = ResponseTypes.Success
                });
                _logger.LogInformation("Question Created By " + userId);
                return View(model);
            }catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View(model);
            }
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var model = _scope.Resolve<QuestionUpdateModel>();
                var userId = Guid.Parse(User.Identity.GetUserId());
                await model.LoadQuestion(id, userId);

                return View(model);
            }
            catch(Exception ex)
            {
                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Message = "You do not have permission",
                    Type = ResponseTypes.Danger
                });
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(QuestionUpdateModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    model.ResolveDependency(_scope);
                    var userId = Guid.Parse(User.Identity.GetUserId());
                    await model.UpdateQuestion(userId);
                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = "Question Update Successfully",
                        Type = ResponseTypes.Success
                    });
                    return View(model);
                }
                catch (Exception ex)
                {
                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = "Question Update Failed",
                        Type = ResponseTypes.Danger
                    });
                    return View(model);
                }
            }
            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid id)
        {
            var model = _scope.Resolve<QuestionDetailsModel>();
            await model.LoadQuestion(id);
            return View(model);
        }


        [ValidateAntiForgeryToken]
        public string UpdateVote(Guid id, string voteType)
        {
            string uId = User.Identity.GetUserId();
            Guid userId = Guid.Parse(uId);
            var model = _scope.Resolve<QuestionVoteModel>();
            model.UserId = userId;
            model.QuestionId = id;
            model.VoteType = Enum.Parse<VoteType>(voteType);
            return model.UpdateVote().Result.ToString();
        }

    }
}
