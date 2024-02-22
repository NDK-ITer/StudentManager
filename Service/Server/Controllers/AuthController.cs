﻿using Application.Models.ModelsOfUser;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using SendMail.Interfaces;
using Server.Requests.Form;

namespace Server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork_Service uow;
        private readonly IEmailSender sentEmail;
        private readonly string baseUrl;

        public AuthController (
            IUnitOfWork_Service uow,
            IEmailSender sentEmail,
            IHttpContextAccessor httpContextAccessor
        ) 
        {
            this.uow = uow;
            this.sentEmail = sentEmail;
            var request = httpContextAccessor.HttpContext.Request;
            baseUrl = $"{request.Scheme}://{request.Host}";
        }

        [HttpPost]
        [HttpOptions]
        [Route("login")]
        public ActionResult Login([FromForm] LoginForm loginForm)
        {
            var res = new Response();
            try
            {
                var getJWt = uow.UserService.GetJwtUser(loginForm.email, loginForm.password);
                if (getJWt.Item2 == null)
                {
                    res.State = 0;
                    res.Data = getJWt.Item1;
                    return new JsonResult(res);
                }

                res.State = 1;
                res.Data = getJWt.Item2;
                return new JsonResult(res);
            }
            catch (Exception e)
            {
                res.State = -1;
                res.Data = e.Message;
                return new JsonResult(res);
            }
        }

        [HttpPost]
        [HttpOptions]
        [Route("register")]
        public ActionResult Register([FromForm] RegisterForm register)
        {
            var res = new Response();
            try
            {
                var result = uow.UserService.CreatedUser(new RegisterModel
                {
                    UserName = register.UserName,
                    FirstName = register.Firstname,
                    LastName = register.Lastname,
                    Email = register.Email,
                    Password = register.Password,
                    AvatarFile = string.Empty,
                    Birthday = DateTime.Now,
                    PhoneNumber = ""
                });
                if (result.Item2 == null)
                {
                    res.State = 0;
                    res.Data = result.Item1;
                    return new JsonResult(res);
                }
                var user = result.Item2;
                sentEmail.SendEmailAsync("nadade120802@gmail.com", "token access", $"<a href = '{baseUrl}/api/auth/{user.Id}/verify-email/{user.TokenAccess}'>Click here</a> to verify your email.");
                res.State = 1;
                res.Data = new { 
                    mess = "Please check your email", 
                };
                return new JsonResult(res);
            }
            catch (Exception e)
            {
                res.State = -1;
                res.Data = e.Message;
                return new JsonResult(res);
            }
        }

        [HttpGet]
        [HttpOptions]
        [Route("{id}/verify-email/{token}")]
        public ActionResult VerifyEmail([FromRoute] string token, string id)
        {
            var res = new Response();
            try
            {
                var user = uow.UserService.GetUserById(id);
                if (user.Item2 == null)
                {
                    res.State= 0;
                    res.Data = new
                    {
                        mess = user.Item1
                    };
                    return new JsonResult(res);
                }
                var result = uow.UserService.VerifyEmail(id);
                if (result.Item2)
                {
                    res.State = 0;
                    
                }
                res.State = 1;
                res.Data = new
                {
                    mess = result.Item1
                };
                return new JsonResult(res);
            }
            catch (Exception e)
            {
                res.State = -1;
                res.Data = e.Message;
                return new JsonResult(res);
            }
        }
    }
}
