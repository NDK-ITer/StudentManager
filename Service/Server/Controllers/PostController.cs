using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendMail.Interfaces;
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
    }
}
