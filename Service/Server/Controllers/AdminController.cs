using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Dynamic;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly string baseUrl;
        private readonly IUnitOfWorkService uow;

        public AdminController(
            IUnitOfWorkService uow,
            IHttpContextAccessor httpContextAccessor
        )
        {
            var request = httpContextAccessor.HttpContext.Request;
            baseUrl = $"{request.Scheme}://{request.Host}";
            this.uow = uow;
        }

        [HttpGet]
        [HttpOptions]
        [Route("get-all-user")]
        public ActionResult GetAllUser()
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
                var checkIsAdmin = uow.UserService.CheckIsAdmin(userId);
                if (!checkIsAdmin.Item2)
                {
                    res.State = 0;
                    res.Data = new
                    {
                        mess = checkIsAdmin.Item1
                    };
                    return new JsonResult(res);
                }

                var result = uow.UserService.GetAllUser();
                if (result.Item2.IsNullOrEmpty())
                {
                    res.State = 0;
                    res.Data = new
                    {
                        mess = result.Item1
                    };
                    return new JsonResult(res);
                }
                var listUser = new List<object>();
                foreach (var item in result.Item2)
                {
                    if (item != null)
                    {
                        listUser.Add(new
                        {
                            id = item.Id,
                            email = item.PresentEmail,
                            fullName = $"{item.FirstName.Trim()} {item.LastName.Trim()}",
                            userName = item.UserName,
                            avatar = $"{baseUrl}/public/{item.Avatar}",
                            isLock = item.IsLock,
                            isVerify = item.IsVerified
                        });
                    }
                }
                res.State = 1;
                res.Data = new
                {
                    listUser = listUser
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

        [HttpPut]
        [HttpOptions]
        [Route("set-lock")]
        public ActionResult SetLockUser(string idUserIsLocked)
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
                var checkIsAdmin = uow.UserService.CheckIsAdmin(userId);
                if (!checkIsAdmin.Item2)
                {
                    res.State = 0;
                    res.Data = new
                    {
                        mess = checkIsAdmin.Item1
                    };
                    return new JsonResult(res);
                }
                var checkIsAdminUser = uow.UserService.CheckIsAdmin(idUserIsLocked);
                if (checkIsAdminUser.Item2)
                {
                    res.State = 0;
                    res.Data = new
                    {
                        mess = $"Cannot lock Admin"
                    };
                    return new JsonResult(res);
                }
                var result = uow.UserService.SetIsLockUser(idUserIsLocked);
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
                    res.State = 1;
                    res.Data = new
                    {
                        mess = result.Item1,
                        id = result.Item2.Id,
                        isLock = result.Item2.IsLock,
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

        [HttpGet]
        [HttpOptions]
        [Route("get-all-post")]
        public ActionResult GetAllPost()
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
                var checkIsAdmin = uow.UserService.CheckIsAdmin(userId);
                if (!checkIsAdmin.Item2)
                {
                    res.State = 0;
                    res.Data = new
                    {
                        mess = checkIsAdmin.Item1
                    };
                    return new JsonResult(res);
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
