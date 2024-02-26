using Application.Services;
using Microsoft.AspNetCore.Mvc;
using SendMail.Interfaces;
using Server.Requests.Form;
using System.Dynamic;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IUnitOfWorkService uow;
        private readonly IEmailSender sender;

        public PostController(
            IUnitOfWorkService uow,
            IEmailSender sender
        )
        {
            this.uow = uow;
            this.sender = sender;
        }

        [HttpPost]
        [HttpOptions]
        [Route("upload")]
        public ActionResult Create([FromForm] dynamic data)
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
                return new JsonResult(res);
            }
            catch (Exception e)
            {
                res.State = -1;
                res.Data = new
                {
                    mess = e.Message,
                };
                return new JsonResult(res);
            }
        }
    }
}
