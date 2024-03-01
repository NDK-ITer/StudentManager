using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly string baseUrl;
        private readonly IUnitOfWorkService uow;
        public ManagerController(
            IUnitOfWorkService uow,
            IHttpContextAccessor httpContextAccessor
        )
        {
            var request = httpContextAccessor.HttpContext.Request;
            baseUrl = $"{request.Scheme}://{request.Host}";
            this.uow = uow;
        }
    }
}
