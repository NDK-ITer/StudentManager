using Microsoft.AspNetCore.Mvc;
using Server.FileMethods;

namespace Server.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class TempController : ControllerBase
    {
        private readonly DocumentMethod documentMethod;
        private readonly string baseUrl;

        public TempController( DocumentMethod documentMethod, IHttpContextAccessor httpContextAccessor)
        {
            this.documentMethod = documentMethod;
            var request = httpContextAccessor.HttpContext.Request;
            baseUrl = $"{request.Scheme}://{request.Host}";
        }

        [HttpGet("get-doc-content")]
        public ActionResult GetUserProfile()
        {
            var content = documentMethod.ConvertToHtml("PublicFile", "testDoc.docx", baseUrl);

            return Ok(content);
        }

    }
}
