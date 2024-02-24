using Microsoft.AspNetCore.Mvc;
using SendMail.Interfaces;
using Server.Requests.Form;
using System.Dynamic;
using XAct.Domain.Repositories;

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
