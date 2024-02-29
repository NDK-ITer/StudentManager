using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Dynamic;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWorkService uow;
        private readonly string baseUrl;

        public UserController(
            IUnitOfWorkService uow,
            IHttpContextAccessor httpContextAccessor
        )
        {
            var request = httpContextAccessor.HttpContext.Request;
            baseUrl = $"{request.Scheme}://{request.Host}";
            this.uow = uow;
        }

        [HttpGet]
        [HttpOptions]
        [Route("{id}")]
        public ActionResult GetUserById([FromRoute] string id)
        {
            dynamic res = new ExpandoObject();
            try
            {
                var result = uow.UserService.GetUserById(id);
                if (result.Item2 == null)
                {
                    res.State = 0;
                    res.Data = new
                    {
                        mess = result.Item1
                    };
                }
                else
                {
                    var user = result.Item2;
                    var listPost = new List<object>();
                    if (result.Item2.ListPost.IsNullOrEmpty())
                    {
                        foreach (var item in result.Item2.ListPost)
                        {
                            listPost.Add(new
                            {
                                idPost = item.Id,
                                title = item.Title,
                                isApproved = item.IsApproved,
                                linkDoc = $"{baseUrl}/public/{item.LinkDocument}"
                            });
                        }
                    }
                    res.State = 1;
                    res.Data = new
                    {
                        id = user.Id,
                        email = user.PresentEmail,
                        fullName = $"{user.FirstName.Trim()} {user.LastName.Trim()}",
                        avatar = $"{baseUrl}/public/{user.Avatar}",
                        userName = user.UserName,
                        isLock = user.IsLock,
                        isVerify = user.IsVerified,
                        authorize = new
                        {
                            role = user.Role.Name,
                            name = user.Role.NormalizeName,
                        },
                        createDate = $"{user.CreatedDate.Day}/{user.CreatedDate.Month}/{user.CreatedDate.Year}",
                        listPost = listPost
                    };
                    if (user.Faculty != null) res.Data.faculty = user.Faculty.Name;
                }
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
