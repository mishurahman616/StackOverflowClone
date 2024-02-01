﻿using Autofac;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackOverflow.DAL.Enums;
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

        public async Task<IActionResult> Index()
        {
            var model = _scope.Resolve<QuestionListModel>();
            await model.LoadQuestions();
            return View(model);
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

        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVote(Guid questionId, string voteType)
        {
            string id = User.Identity.GetUserId();
            Guid userId = Guid.Parse(id);
            var model = _scope.Resolve<QuestionVoteModel>();
            model.UserId = userId;
            model.QuestionId = questionId;
            model.VoteType = Enum.Parse<VoteType>(voteType);
            await model.UpdateVote();

            return View();
        }

    }
}