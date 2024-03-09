using Application.Models.ModelsOfPost;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Server.FileMethods;
using Server.Requests.Form;
using System.Dynamic;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly string baseUrl;
        private readonly IUnitOfWorkService uow;
        private readonly DocumentMethod documentMethod;

        public ManagerController(
            IUnitOfWorkService uow,
            IHttpContextAccessor httpContextAccessor,
            DocumentMethod documentMethod
        )
        {
            var request = httpContextAccessor.HttpContext.Request;
            baseUrl = $"{request.Scheme}://{request.Host}";
            this.uow = uow;
            this.documentMethod = documentMethod;
        }

        [HttpGet]
        [HttpOptions]
        [Route("get-post-faculty")]
        public ActionResult GetPostFaculty(string idFaculty)
        {
            dynamic res = new ExpandoObject();
            res.Data = new ExpandoObject();
            try
            {
                string userId = string.Empty;
                if (HttpContext.Items["UserId"] == null)
                {
                    res.State = 0;
                    res.Data = new
                    {
                        mess = "Login Please!"
                    };
                    return new JsonResult(res);
                }
                userId = HttpContext.Items["UserId"].ToString();
                var checkIsManager = uow.UserService.CheckIsMaanager(userId);
                if (!checkIsManager.Item2)
                {
                    res.State = 0;
                    res.Data.mess = checkIsManager.Item1;
                    return new JsonResult(res);
                }
                var result = uow.PostService.GetPostOfFaculty(userId, idFaculty);
                if (result.Item2 == null)
                {
                    res.State = 0;
                    res.Data.mess = checkIsManager.Item1;
                    return new JsonResult(res);
                }
                else
                {
                    res.State = 1;
                    var listPost = new List<object>();
                    if (!result.Item2.IsNullOrEmpty())
                    {
                        foreach (var item in result.Item2)
                        {
                            listPost.Add(new
                            {
                                id = item.Id,
                                title = item.Title,
                                isCheck = item.IsChecked,
                                isApproved = item.IsApproved,
                                datePost = item.DatePost,
                                avatarPost = $"{baseUrl}/public/{item.AvatarPost}",
                                linkDocument = $"{baseUrl}/public/{item.LinkDocument}"
                            });
                        }
                    }
                    res.Data.listPost = listPost;
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
        [Route("get-post-faculty/{id}")]
        public ActionResult GetPostFacultyById([FromRoute]string id)
        {
            dynamic res = new ExpandoObject();
            res.Data = new ExpandoObject();
            try
            {
                string userId = string.Empty;
                if (HttpContext.Items["UserId"] == null)
                {
                    res.State = 0;
                    res.Data = new
                    {
                        mess = "Login Please!"
                    };
                    return new JsonResult(res);
                }
                userId = HttpContext.Items["UserId"].ToString();
                var checkIsManager = uow.UserService.CheckIsMaanager(userId);
                if (!checkIsManager.Item2)
                {
                    res.State = 0;
                    res.Data.mess = checkIsManager.Item1;
                    return new JsonResult(res);
                }

                var result = uow.PostService.GetById(id);
                if (result.Item2 == null)
                {
                    res.State = 0;
                    res.Data.mess = result.Item1;
                }
                else
                {
                    res.State = 1;
                    res.Data.id = result.Item2.Id;
                    res.Data.title = result.Item2.Title;
                    res.Data.avatarPost = $"{baseUrl}/public/{result.Item2.AvatarPost}";
                    res.Data.linkDocument = $"{baseUrl}/public/{result.Item2.LinkDocument}";
                    res.Data.isApproved = result.Item2.IsApproved;
                    res.Data.content = documentMethod.ConvertToHtml("PublicFile",result.Item2.LinkDocument,baseUrl);
                    uow.PostService.CheckPost(id);
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

        [HttpPut]
        [HttpOptions]
        [Route("approved-post")]
        public ActionResult ApprovedPost([FromForm] EditPostForm data)
        {
            dynamic res = new ExpandoObject();
            res.Data = new ExpandoObject();
            try
            {
                string userId = string.Empty;
                if (HttpContext.Items["UserId"] == null)
                {
                    res.State = 0;
                    res.Data = new
                    {
                        mess = "Login Please!"
                    };
                    return new JsonResult(res);
                }
                userId = HttpContext.Items["UserId"].ToString();
                var checkIsManager = uow.UserService.CheckIsMaanager(userId);
                if (!checkIsManager.Item2)
                {
                    res.State = 0;
                    res.Data.mess = checkIsManager.Item1;
                    return new JsonResult(res);
                }

                var editPost = new EditPostModel()
                {
                    Id = data.Id,
                    Title = data.Title,
                    IsApproved = data.IsApproved,
                };
                var result = uow.PostService.Update(userId ,editPost);
                if (result.Item2 == null)
                {
                    res.State = 0;
                    res.Data.mess = result.Item1;
                }
                else
                {
                    var post = result.Item2;
                    if (data.LinkDocument != null) documentMethod.SaveFile("PublicFile", data.LinkDocument, post.LinkDocument);
                    if (data.AvatarPost != null) documentMethod.SaveFile("PublicFile", data.AvatarPost, post.AvatarPost);
                    res.State = 1;
                    res.Data.id = post.Id;
                    res.Data.title = post.Title;
                    res.Data.isApproved = post.IsApproved;
                    res.Data.content = documentMethod.ConvertToHtml("PublicFile", post.LinkDocument, baseUrl);
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
        [Route("get-faculty")]
        public ActionResult GetFacultyById(string idFaculty)
        {
            dynamic res = new ExpandoObject();
            res.Data = new ExpandoObject();
            try
            {
                string userId = string.Empty;
                if (HttpContext.Items["UserId"] == null)
                {
                    res.State = 0;
                    res.Data = new
                    {
                        mess = "Login Please!"
                    };
                    return new JsonResult(res);
                }
                userId = HttpContext.Items["UserId"].ToString();
                var checkIsManager = uow.UserService.CheckIsMaanager(userId);
                if (!checkIsManager.Item2)
                {
                    res.State = 0;
                    res.Data.mess = checkIsManager.Item1;
                    return new JsonResult(res);
                }

                var result = uow.FacultyService.GetById(idFaculty);
                if (result.Item2 == null)
                {
                    res.State = 0;
                    res.Data.mess = result.Item1;
                }
                else
                {
                    var faculty = result.Item2;
                    res.State = 1;
                    res.Data.id = faculty.Id;
                    res.Data.name = faculty.Name;
                    res.Data.isOpen = faculty.IsOpen;
                    res.Data.isDelete = faculty.IsDeleted;
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


        [HttpPut]
        [HttpOptions]
        [Route("state-faculty")]
        public ActionResult StateFaculty(string idFaculty)
        {
            dynamic res = new ExpandoObject();
            res.Data = new ExpandoObject();
            try
            {
                string userId = string.Empty;
                if (HttpContext.Items["UserId"] == null)
                {
                    res.State = 0;
                    res.Data = new
                    {
                        mess = "Login Please!"
                    };
                    return new JsonResult(res);
                }
                userId = HttpContext.Items["UserId"].ToString();
                var checkIsManager = uow.UserService.CheckIsMaanager(userId);
                if (!checkIsManager.Item2)
                {
                    res.State = 0;
                    res.Data.mess = checkIsManager.Item1;
                    return new JsonResult(res);
                }

                var result = uow.FacultyService.OpenCloseFaculty(idFaculty);
                if (result.Item2 == null)
                {
                    res.State = 0;
                    res.Data.mess = result.Item1;
                }
                else
                {
                    res.State = 1;
                    res.Data.isOpen = result.Item2.IsOpen;
                    res.Data.mess = result.Item1;
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
