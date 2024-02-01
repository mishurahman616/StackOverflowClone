using Autofac;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackOverflow.Web.Models.QuestionModels;

namespace StackOverflow.Web.Controllers
{
    public class QuestionController : Controller
    {
        private readonly ILifetimeScope _scope;
        ILogger<QuestionController> _logger;
        public QuestionController(ILifetimeScope scope, ILogger<QuestionController> logger)
        {
            _scope = scope;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(QuestionCreateModel model)
        {
            try
            {
                model.ResolveDependency(_scope);
                var userId = User.Identity.GetUserId();
                model.UserId = Guid.Parse(userId);
                await model.CreateQuestion();
                return View(model);
            }catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View(model);
            }
        }
        public IActionResult Edit()
        {
            return View();
        }

    }
}
