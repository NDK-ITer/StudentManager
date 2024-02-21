using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Server.Requests.Form;

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

        [HttpPost]
        [HttpOptions]
        [Route("login")]
        public ActionResult Login([FromForm] LoginForm loginForm)
        {
            var res = new Response();
            try
            {
                var getJWt = uow.UserService.GetJwtUser(loginForm.email, loginForm.password);
                if (getJWt.Item2 == null)
                {
                    res.State = 0;
                    res.Data = getJWt.Item1;
                    return new JsonResult(res);
                }

                res.State = 1;
                res.Data = getJWt.Item2;
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
