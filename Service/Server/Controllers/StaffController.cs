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
                var check = uow.UserService.CheckIsStaff(userId);
                if (check.Item2 == false)
                {
                    res.State = 0;
                    res.Data.mess = check.Item1;
                    return new JsonResult(res);
                }
                var result = uow.UserService.GetListUser();
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
                        });
                    }
                }
                res.State = 1;
                res.Data.listUser = listUser;
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
