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
                res.Data = new ExpandoObject();
                res.Data.userName = result.Item2.UserName;
                res.Data.firstName = result.Item2.FirstName;
                res.Data.lastName = result.Item2.LastName;
                res.Data.email = result.Item2.Email;
                res.Data.role = result.Item2.Role;
                res.Data.linkAvatar = $"{baseUrl}/public/{result.Item2.Avatar}";
                res.jwt = result.Item2.JwtToken;
                if (uow.UserService.CheckIsMaanager(result.Item2.Id).Item2)
                {
                    res.Data.facultyId = uow.UserService.GetUserById(result.Item2.Id).Item2.FacultyID;
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
                sentEmail.SendEmailAsync("nguyenduykhuong12ta2@gmail.com", "token access", $"<a href = '{baseUrl}/api/auth/{user.Id}/verify-email/{user.TokenAccess}'>Click here</a> to verify your email.");
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
                        firstName = result.Item2.FirstName,
                        lastName = result.Item2.LastName,
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
        public ActionResult EditAvatar([FromForm] IFormFile NewAvatar)
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
                var checkIsLock = uow.UserService.GetUserById(userId);
                if (checkIsLock.Item2 == null) 
                {
                    res.State = 0;
                    res.Data = new
                    {
                        mess = checkIsLock.Item1
                    };
                }
                if (checkIsLock.Item2.IsLock == true)
                {
                    res.State = 0;
                    res.Data = new
                    {
                        mess = $"User {checkIsLock.Item2.PresentEmail} have been lock"
                    };
                }
                var getUser = uow.UserService.GetUserById(userId);
                if (getUser.Item2 == null)
                {
                    res.State = 0;
                    res.Data = new
                    {
                        mess = getUser.Item1
                    };
                    return new JsonResult(res);
                }
                var avatarUser = getUser.Item2.Avatar;
                var newNameAvatar = imgMethod.SaveFile("PublicFile", NewAvatar, avatarUser);
                if (newNameAvatar.IsNullOrEmpty())
                {
                    res.State = 0;
                    res.Data = new
                    {
                        mess = "Upload avatar is Fail."
                    };
                    return new JsonResult(res);
                }
                res.State = 1;
                res.Data = new
                {
                    mess = "Your avatar have been update."
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

        [HttpPost]
        [HttpOptions]
        [Route("change-password")]
        public ActionResult ChangePassword([FromForm] string newPassword)
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
                var result = uow.UserService.ResetPassword(userId, newPassword);
                if (result.Item2 == null)
                    res.State = 0;
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

        [HttpPost]
        [HttpOptions]
        [Route("edit-profile")]
        public ActionResult EditProfile([FromForm] EditUserForm data)
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
                var editUserModel = new EditUserModel()
                {
                    IdUser = userId,
                    FirstName = data.firstName,
                    LastName = data.lastName,
                    UserName = data.userName,
                };
                var result = uow.UserService.EditUser(editUserModel);
                if (result.Item2 == null)
                {
                    res.State = 0;
                    res.Data = new
                    {
                        mess = result.Item2
                    };
                }
                    
                res.State = 1;
                res.Data = new
                {
                    userName = result.Item2.UserName,
                    firstName = result.Item2.FirstName,
                    lastName = result.Item2.LastName,
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
        [Route("get-all-role")]
        public ActionResult GetAllRole()
        {
            dynamic res = new ExpandoObject();
            try
            {
                var result = uow.RoleService.GetAll();
                if (result.Item2 == null)
                {
                    res.State = 0;
                    res.Data = new
                    {
                        mess = result.Item1
                    };

                }else
                {
                    var listRole = new List<object>();
                    foreach (var item in result.Item2)
                    {
                        listRole.Add(new
                        {
                            Id = item.Id,
                            Name = item.Name,
                            NormalizeName = item.NormalizeName,
                            Decription = item.Description,
                        });
                    }
                    res.State = 1;
                    res.Data = new
                    {
                        listRole = listRole
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
    }
}
