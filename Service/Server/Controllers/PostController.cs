using Microsoft.AspNetCore.Mvc;
using SendMail.Interfaces;
using Server.Requests.Form;
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
        public ActionResult Create(UploadPostForm data)
        {
            var res = new Response();
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
