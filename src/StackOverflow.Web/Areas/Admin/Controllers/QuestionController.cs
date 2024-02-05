using Autofac;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackOverflow.BL.Exceptions;
using StackOverflow.Web.Extensions;
using StackOverflow.Web.Models;
using StackOverflow.Web.Models.QuestionModels;

namespace StackOverflow.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class QuestionController : Controller
    {
        private readonly ILifetimeScope _scope;
        ILogger<QuestionController> _logger;
        public QuestionController(ILifetimeScope scope, ILogger<QuestionController> logger)
        {
            _scope = scope;
            _logger = logger;
        }

        public async Task<IActionResult> Index(QuestionListModel model)
        {
            model.ResolveDependency(_scope);
            var (questions, total, totalToDislplay, totalPages) = await model.GetPaginated(model.SearchText, model.PageIndex, model.PageSize);
            model.Questions = questions;
            model.TotalQuestion = total;
            model.TotalToDisplay = totalToDislplay;
            model.TotalPage = totalPages;
            return View(model);
        }

 
        public async Task<IActionResult> Details(Guid id)
        {
            var model = _scope.Resolve<QuestionDetailsModel>();
            await model.LoadQuestion(id);
            return View(model);
        }

        // GET: QuestionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QuestionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
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
                return RedirectToAction("Index");
            }
            catch (Exception ex) when (ex is NotFoundException || ex is PermissionMissingException)
            {
                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Message = ex.Message,
                    Type = ResponseTypes.Danger
                });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message.ToString(), ex);
                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Message = "Question Delete Failed",
                    Type = ResponseTypes.Danger
                });
            }

            return RedirectToAction("Details", new {id});
        }
    }
}
