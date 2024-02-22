using Microsoft.AspNetCore.Mvc;
using XAct.Domain.Repositories;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IUnitOfWorkService uow;

        public CommentController(
            IUnitOfWorkService uow    
        )
        {
            this.uow = uow;
        }
    }
}
