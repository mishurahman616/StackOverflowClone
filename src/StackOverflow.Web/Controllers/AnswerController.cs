﻿using Autofac;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackOverflow.BL.Exceptions;
using StackOverflow.DAL.Enums;
using StackOverflow.Web.Extensions;
using StackOverflow.Web.Models;
using StackOverflow.Web.Models.AnswerModels;

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
                string errorMessage = ModelState.SelectMany(x => x.Value.Errors).First().ErrorMessage;

                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Message = errorMessage,
                    Type = ResponseTypes.Danger,
                });
            }
            return RedirectToAction("Details", "Question", new { id = model.QuestionId });
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var model = _scope.Resolve<AnswerUpdateModel>();
                var userId = Guid.Parse(User.Identity.GetUserId());
                await model.LoadAnswer(id, userId);

                return View(model);
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
                    Message = "Somthing went wrong",
                    Type = ResponseTypes.Danger
                });
            }
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

        [HttpPost]
        public async Task<IActionResult> Edit(AnswerUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.ResolveDependency(_scope);
                    var userId = Guid.Parse(User.Identity.GetUserId());
                    await model.UpdateAnswer(userId);
                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = "Answer Update Successfully",
                        Type = ResponseTypes.Success
                    });
                }
                catch (Exception ex)when (ex is NotFoundException || ex is PermissionMissingException)
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
                        Message = "Answer Update Failed",
                        Type = ResponseTypes.Danger
                    });
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
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
            catch (Exception ex) when (ex is NotFoundException || ex is PermissionMissingException)
            {
                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Message = ex.Message,
                    Type = ResponseTypes.Danger
                });
            } catch (Exception ex)
            {
                _logger.LogError(ex.ToString(), ex);
                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Message = "Answer Delete Failed",
                    Type = ResponseTypes.Danger
                });
            }

            return RedirectToAction("MyAnswer", "User");
        }

        [ValidateAntiForgeryToken]
        public JsonResult UpdateVote(Guid id, string voteType)
        {
            try
            {
                string uId = User.Identity.GetUserId();
                Guid userId = Guid.Parse(uId);
                var model = _scope.Resolve<AnswerVoteModel>();
                model.UserId = userId;
                model.AnswerId = id;
                model.VoteType = Enum.Parse<VoteType>(voteType);
                var message = model.UpdateVote().Result.ToString();
                return Json(new { message });

            }catch(Exception ex)
            {
                _logger?.LogError(ex.ToString(), ex);
                return Json(new { message = "Error" });
            }
        }
    }
}
