using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Server.Requests.Form;
using System.Dynamic;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly string baseUrl;
        private readonly IUnitOfWorkService uow;

        public StaffController(
            IUnitOfWorkService uow,
            IHttpContextAccessor httpContextAccessor
        )
        {
            var request = httpContextAccessor.HttpContext.Request;
            baseUrl = $"{request.Scheme}://{request.Host}";
            this.uow = uow;
        }

        [HttpPost]
        [HttpOptions]
        [Route("update-to-string")]
        public ActionResult UpdateToStudent([FromForm] UpdateToStudentForm data)
        {
            dynamic res = new ExpandoObject();
            res.Data = new ExpandoObject();
            try
            {
                string userId = string.Empty;
                if (HttpContext.Items["UserId"] == null)
                {
                    res.State = 0;
                    res.Data.mess = "Login Please!";
                    return new JsonResult(res);
                }
                userId = HttpContext.Items["UserId"].ToString();
                var check = uow.UserService.CheckIsStudent(userId);
                if (check.Item2 == false)
                {
                    res.State = 0;
                    res.Data.mess = check.Item1;
                    return new JsonResult(res);
                }
                var result = uow.RoleService.SetStudent(data.UserId);
                if (result.Item2 == null)
                {
                    res.State = 0;
                    res.Data.mess = result.Item1;
                }else
                {
                    res.State = 1;
                    res.Data.mess = result.Item1;
                    res.Data.id = result.Item2.Id;
                    res.Data.authorize = new
                    {
                        role = result.Item2.Role.Name,
                        name = result.Item2.Role.NormalizeName,
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
        [Route("get-list-user")]
        public ActionResult GetListUser()
        {
            dynamic res = new ExpandoObject();
            res.Data = new ExpandoObject();
            try
            {
                string userId = string.Empty;
                if (HttpContext.Items["UserId"] == null)
                {
                    res.State = 0;
                    res.Data.mess = "Login Please!";
                    return new JsonResult(res);
                }
                userId = HttpContext.Items["UserId"].ToString();
                var check = uow.UserService.CheckIsStudent(userId);
                if (check.Item2 == false)
                {
                    res.State = 0;
                    res.Data.mess = check.Item1;
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
                            authorize = new
                            {
                                role = item.Role.Name,
                                name = item.Role.NormalizeName,
                            }
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
    }
}
