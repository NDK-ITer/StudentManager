using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XAct.Domain.Repositories;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacultyController : ControllerBase
    {
        private readonly IUnitOfWorkService uow;

        public FacultyController(
            IUnitOfWorkService uow
        )
        {
            this.uow = uow;
        }
    }
}
