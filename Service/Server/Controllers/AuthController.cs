using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork_Service uow;

        public AuthController (IUnitOfWork_Service uow) 
        {
            this.uow = uow;
        }

        //[HttpPost]
        //[HttpOptions]
        //[Route("login")]
        //public ActionResult Login ()
        //{
        //    try
        //    {
        //        var getJWt = uow.UserService.GetJwtUser();
        //    }
        //    catch (Exception)
        //    {

        //    }
        //}
    }
}
