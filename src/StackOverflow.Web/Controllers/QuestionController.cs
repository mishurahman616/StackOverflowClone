using Autofac;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackOverflow.BL.Exceptions;
using StackOverflow.DAL.Enums;
using StackOverflow.Web.Extensions;
using StackOverflow.Web.Models;
using StackOverflow.Web.Models.QuestionModels;

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
            if (ModelState.IsValid)
            {
                try
                {
                    model.ResolveDependency(_scope);
                    var userId = User.Identity.GetUserId();
                    model.UserId = Guid.Parse(userId);
                    await model.CreateQuestion();
                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = "Question Created Successfully",
                        Type = ResponseTypes.Success
                    });
                    _logger.LogInformation("Question Created By " + userId);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = "Question Create Failed",
                        Type = ResponseTypes.Success
                    });
                }
            }
            return View(model);
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
            catch(Exception ex) when (ex is NotFoundException || ex is PermissionMissingException)
            {
                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Message = ex.Message,
                    Type = ResponseTypes.Danger
                });

                // Get the referrer URL from the HttpContext
                string referrerUrl = HttpContext.Request.Headers["Referer"];

                // Check if the referrer URL is not null or empty
                if (!string.IsNullOrEmpty(referrerUrl))
                {
                    // Redirect the user back to the referrer URL
                    return Redirect(referrerUrl);
                }

                return View();
            }
            catch(Exception ex)
            {
                //log error 
                _logger.LogError($"{ex.Message}", ex);

                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Message = "Something went wrong",
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
                catch (Exception ex) when(ex is NotFoundException || ex is PermissionMissingException)
                {
                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = ex.Message,
                        Type = ResponseTypes.Danger
                    });
                    return View(model);
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex.ToString(), ex);

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

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var model = _scope.Resolve<QuestionListModel>();
                await model.DeleteQuestionByUser(Guid.Parse(User.Identity.GetUserId()), id);
                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Message = "Question Deleted Successfully",
                    Type = ResponseTypes.Success
                });
            }
            catch (Exception ex) when (ex is NotFoundException || ex is PermissionMissingException)
            {
                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Message = ex.Message,
                    Type = ResponseTypes.Danger
                });

            }catch(Exception ex)
            {
                _logger.LogError(ex.Message.ToString(), ex);
                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Message = "Question Delete Failed",
                    Type = ResponseTypes.Danger
                });
            }

            return RedirectToAction("MyQuestion", "User");
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
