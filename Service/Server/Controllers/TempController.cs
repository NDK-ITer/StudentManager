using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class TempController : ControllerBase
    {
        public TempController()
        {
           
        }

        [HttpGet("profile")]
        public ActionResult GetUserProfile()
        {
            var userId = string.Empty;
            if (HttpContext.Items["UserId"] != null)
            {
                userId = HttpContext.Items["UserId"].ToString();
            }

            return Ok(new { UserId = userId });
        }
    }
}
