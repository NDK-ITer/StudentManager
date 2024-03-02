using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Server.Requests.Form;
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
                    if (!result.Item2.ListPost.IsNullOrEmpty())
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
                    res.Data = new ExpandoObject();
                    res.Data.id = user.Id;
                    res.Data.email = user.PresentEmail;
                    res.Data.fullName = $"{user.FirstName.Trim()} {user.LastName.Trim()}";
                    res.Data.avatar = $"{baseUrl}/public/{user.Avatar}";
                    res.Data.userName = user.UserName;
                    res.Data.isLock = user.IsLock;
                    res.Data.isVerify = user.IsVerified;
                    res.Data.authorize = new
                    {
                        role = user.Role.Name,
                        name = user.Role.NormalizeName,
                    };
                    res.Data.createDate = $"{user.CreatedDate.Day}/{user.CreatedDate.Month}/{user.CreatedDate.Year}";
                    res.Data.listPost = listPost;
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

        [HttpGet]
        [HttpOptions]
        [Route("get-faculty-public")]
        public ActionResult GetFacultyPublic()
        {
            dynamic res = new ExpandoObject();
            try
            {
                var result = uow.FacultyService.GetPublic();
                if (result.Item2.IsNullOrEmpty())
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
                    var listFacultyPublic = new List<object>();
                    if (!result.Item2.IsNullOrEmpty())
                    {
                        foreach (var item in result.Item2)
                        {
                            listFacultyPublic.Add(new
                            {
                                id = item.Id,
                                name = item.Name,
                            });
                        }
                    }
                    res.State = 1;
                    res.Data = new ExpandoObject();
                    res.Data.listFaculty = listFacultyPublic;
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

        [HttpPost]
        [HttpOptions]
        [Route("upload-post")]
        public ActionResult UploadPost([FromForm] UploadPostForm data)
        {
            dynamic res = new ExpandoObject();
            res.Data = new ExpandoObject();
            try
            {
                string userId = string.Empty;
                if (HttpContext.Items["UserId"] == null)
                {
                    res.State = 0;
                    res.Data.mess = "Login Please!";
                    return new JsonResult(res);
                }
                userId = HttpContext.Items["UserId"].ToString();
                var check = uow.UserService.CheckIsUser(userId);
                if (check.Item2 == false)
                {
                    res.State = 0;
                    res.Data.mess = check.Item1;
                    return new JsonResult(res);
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
