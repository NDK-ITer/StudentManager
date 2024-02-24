using Application.Models.ModelsOfUser;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SendMail.Interfaces;
using Server.FileMethods;
using Server.Requests.Form;
using System;
using System.Dynamic;
using XAct;

namespace Server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWorkService uow;
        private readonly IEmailSender sentEmail;
        private readonly ImageMethod imgMethod;
        private readonly string baseUrl;

        public AuthController(
            IUnitOfWorkService uow,
            IEmailSender sentEmail,
            IHttpContextAccessor httpContextAccessor,
            ImageMethod imgMethod
        )
        {
            this.uow = uow;
            this.sentEmail = sentEmail;
            this.imgMethod = imgMethod;
            var request = httpContextAccessor.HttpContext.Request;
            baseUrl = $"{request.Scheme}://{request.Host}";
        }

        [HttpPost]
        [HttpOptions]
        [Route("login")]
        public ActionResult Login([FromForm] LoginForm loginForm)
        {
            dynamic res = new ExpandoObject();
            try
            {
                var result = uow.UserService.GetJwtUser(loginForm.email, loginForm.password);
                if (result.Item2 == null)
                {
                    res.State = 0;
                    res.Data = new
                    {
                        mess = result.Item1
                    };
                    return new JsonResult(res);
                }

                res.State = 1;
                res.Data = new
                {
                    UserName = result.Item2.UserName,
                    LinkAvatar = $"{baseUrl}/public/{result.Item2.Avatar}"
                };
                res.jwt = result.Item2.JwtToken;
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
            dynamic res = new ExpandoObject();
            try
            {

                var imageFile = imgMethod.ReadFile("FileMethods\\ImagesDefault", "default-avatar.png");
                var random = new Random(); int randomNumber = random.Next(1000);
                var avatarName = $"AvatarUser-{Guid.NewGuid().ToString().Substring(0, 10)}.png";
                var avatar = imgMethod.SaveFile("PublicFile", imageFile, avatarName);

                var result = uow.UserService.CreatedUser(new RegisterModel
                {
                    UserName = register.UserName,
                    FirstName = register.FirstName,
                    LastName = register.LastName,
                    Email = register.Email,
                    Password = register.Password,
                    AvatarFile = avatar,
                    Birthday = DateTime.Now,
                    PhoneNumber = ""
                });
                if (result.Item2 == null)
                {
                    res.State = 0;
                    res.Data = new
                    {
                        mess = result.Item1
                    };
                    return new JsonResult(res);
                }
                var user = result.Item2;
                sentEmail.SendEmailAsync("nadade120802@gmail.com", "token access", $"<a href = '{baseUrl}/api/auth/{user.Id}/verify-email/{user.TokenAccess}'>Click here</a> to verify your email.");
                res.State = 1;
                res.Data = new
                {
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
            dynamic res = new ExpandoObject();
            try
            {
                var user = uow.UserService.GetUserById(id);
                if (user.Item2 == null)
                {
                    res.State = 0;
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

        [HttpGet]
        [HttpOptions]
        [Route("information")]
        public ActionResult MyInformation()
        {
            dynamic res = new ExpandoObject();
            try
            {
                string userId = string.Empty;
                if (HttpContext.Items["UserId"] == null)
                {
                    res.State = 0;
                    res.Data = new
                    {
                        mess = "Login Please!"
                    };
                    return new JsonResult(res);
                }
                userId = HttpContext.Items["UserId"].ToString();
                var result = uow.UserService.GetUserById(userId);
                if (result.Item2 == null)
                {
                    res.State = 0;
                    res.Data = new
                    {
                        mess = result.Item1
                    };
                }
                else
                {
                    var listPost = new List<object>();
                    if (!result.Item2.ListPost.IsNullOrEmpty())
                    {
                        foreach (var item in result.Item2.ListPost)
                        {
                            listPost.Add(new
                            {
                                id = item.Id,
                                name = item.Title,
                                isDelete = item.IsApproved
                            });
                        }
                    }
                    res.State = 1;
                    res.Data = new
                    {
                        fullName = $"{result.Item2.FirstName.Trim()} {result.Item2.LastName.Trim()}",
                        userName = result.Item2.UserName,
                        email = result.Item2.PresentEmail,
                        listPost = listPost
                    };
                }
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
        [Route("edit-avatar")]
        public ActionResult EditAvatar([FromForm] IFormFile newAvatar)
        {
            dynamic res = new ExpandoObject();
            try
            {
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
