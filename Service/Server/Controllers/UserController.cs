using Application.Models.ModelsOfComment;
using Application.Models.ModelsOfPost;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SendMail.Interfaces;
using Server.FileMethods;
using Server.Requests.Form;
using System.Dynamic;
using System.Text.RegularExpressions;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWorkService uow;
        private readonly IEmailSender emailSender;
        private readonly DocumentMethod documentMethod;
        private readonly string baseUrl;

        public UserController(
            IUnitOfWorkService uow,
            IEmailSender emailSender,
            IHttpContextAccessor httpContextAccessor,
            DocumentMethod documentMethod
        )
        {
            var request = httpContextAccessor.HttpContext.Request;
            baseUrl = $"{request.Scheme}://{request.Host}";
            this.uow = uow;
            this.emailSender = emailSender;
            this.documentMethod = documentMethod;
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
                var checkIsLock = uow.UserService.GetUserById(userId);
                if (checkIsLock.Item2 == null)
                {
                    res.State = 0;
                    res.Data = new
                    {
                        mess = checkIsLock.Item1
                    };
                    return new JsonResult(res);
                }
                if (checkIsLock.Item2.IsLock == true)
                {
                    res.State = 0;
                    res.Data.mess = $"User {checkIsLock.Item2.PresentEmail} have been lock";
                    return new JsonResult(res);
                }
                var checkFacultyOpen = uow.FacultyService.GetById(data.FacultyId);
                if (checkFacultyOpen.Item2.IsOpen == false)
                {
                    res.State = 0;
                    res.Data.mess = $"{checkFacultyOpen.Item2.Name} is not open!";
                    return new JsonResult(res);
                }
                var random = new Random(); int randomNumber = random.Next(1000);
                var avatarPostName = $"AvatarPost-{Guid.NewGuid().ToString().Substring(0, 10)}.png";
                var docPostName = $"DocumentPost-{Guid.NewGuid().ToString().Substring(0, 10)}.{documentMethod.GetFileExtension(data.Document)}";
                

                var addPostModel = new AddPostModel()
                {
                    Title = data.Title,
                    AvatarPost = avatarPostName,
                    LinkDocument = docPostName,
                    FacultyId = data.FacultyId,
                    UserId = userId,
                };
                var result  = uow.PostService.Add(addPostModel);
                if (result.Item2 == null)
                {
                    res.State = 0;
                    res.Data.mess = result.Item1;
                }
                else
                {
                    var avatar = documentMethod.SaveFile("PublicFile", data.AvatarPost, avatarPostName);
                    var linkDoc = documentMethod.SaveFile("PublicFile", data.Document, docPostName);
                    var post = result.Item2;
                    var user = uow.UserService.GetUserById(userId).Item2;
                    foreach (var item in post.Faculty.ListAdmin)
                    {
                        emailSender.SendEmailAsync(item.PresentEmail,"Student upload post", $"Student {user.FirstName.Trim()} {user.LastName.Trim()} have uploaded a post");
                    }
                    res.State = 1;
                    res.Data.id = post .Id;
                    res.Data.title = post .Title;
                    res.Data.dateUpload = $"{post .DatePost.Day}/{post .DatePost.Month}/{post .DatePost.Year}"; 
                    res.Data.isApproved = post .IsApproved;
                    res.Data.isCheck = post .IsChecked;
                    res.Data.avatarPost = $"{baseUrl}/public/{post .AvatarPost}";
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
        [Route("get-post-public")]
        public ActionResult GetPostPublic()
        {
            dynamic res = new ExpandoObject();
            try
            {
                var result = uow.PostService.GetPublic();
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
                    var listPostPublic = new List<object>();
                    if (!result.Item2.IsNullOrEmpty())
                    {
                        foreach (var item in result.Item2)
                        {
                            var content = documentMethod.ConvertToHtml("PublicFile", item.LinkDocument, baseUrl);
                            if (!content.IsNullOrEmpty())
                            {
                                string plainText = Regex.Replace(content, "<.*?>", string.Empty);
                                plainText = plainText.Replace("<p>", "").Replace("</p>", "");
                                content = plainText.Length <= 20 ? plainText : plainText.Substring(0, 20);
                            }
                            listPostPublic.Add(new
                            {
                                id = item.Id,
                                title = item.Title,
                                avatarPost = $"{baseUrl}/public/{item.AvatarPost}",
                                facultyId = item.FacultyId,
                                uploadDate = $"{item.DatePost.Day}/{item.DatePost.Month}/{item.DatePost.Year}",
                                description = content
                            });
                        }
                        
                    }
                    res.State = 1;
                    res.Data = new ExpandoObject();
                    res.Data.listPost = listPostPublic;
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
        [Route("get-my-post")]
        public ActionResult GetMyPost()
        {
            dynamic res = new ExpandoObject();
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
                var result = uow.UserService.GetUserById(userId);
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
                    var listPostPublic = new List<object>();
                    if (!user.ListPost.IsNullOrEmpty())
                    {
                        foreach (var item in user.ListPost)
                        {
                            listPostPublic.Add(new
                            {
                                id = item.Id,
                                title = item.Title,
                                isApproved = item.IsApproved,
                                isChecked = item.IsChecked,
                                dateUpload = $"{item.DatePost.Day}/{item.DatePost.Month}/{item.DatePost.Year}",
                                avatarPost = $"{baseUrl}/public/{item.AvatarPost}",
                                linkDocument = $"{baseUrl}/public/{item.LinkDocument}",
                            });
                        }

                    }
                    res.State = 1;
                    res.Data = new ExpandoObject();
                    res.Data.listPost = listPostPublic;
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
        [Route("get-post")]
        public ActionResult GetPostById(string idPost)
        {
            dynamic res = new ExpandoObject();
            res.Data = new ExpandoObject();
            try
            {
                var result = uow.PostService.GetById(idPost);
                if (result.Item2 == null)
                {
                    res.State = 0;
                    res.Data.mess = result.Item1;
                }
                else
                {
                    var post = result.Item2;
                    if (!result.Item2.IsApproved)
                    {
                        res.State = 0;
                        res.Data.mess = $"Post with id: {post.Title} was not approved";
                    }
                    else
                    {
                        res.State = 1;
                        res.Data.id = post.Id;
                        res.Data.title = post.Title;
                        res.Data.uploadDate = $"{post.DatePost.Day}/{post.DatePost.Month}/{post.DatePost.Year}";
                        res.Data.content = documentMethod.ConvertToHtml("PublicFile", post.LinkDocument, baseUrl);
                        var listComment = new List<object>();
                        if (!post.ListComent.IsNullOrEmpty())
                        {
                            foreach (var item in post.ListComent)
                            {
                                listComment.Add(new
                                {
                                    id = item.Id,
                                    content = item.Content,
                                    dateComment = $"{item.DateComment.Day}/{item.DateComment.Month}/{item.DateComment.Year}",
                                    avatarUser = $"{baseUrl}/public/{item.User.Avatar}",
                                });
                            }
                        }
                        res.Data.listComment = listComment;
                    }
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
        [Route("comment")]
        public ActionResult CommentPost([FromForm] CommentForm data)
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
                var cmt = new AddCommentModel()
                {
                    Content = data.content
                };
                var result = uow.CommentService.Add(userId, data.idPost, cmt);
                if (result.Item2 == null)
                {
                    res.State = 0;
                    res.Data.mess = result.Item1;
                }
                else
                {
                    res.State = 1;
                    res.Data.id = result.Item2.Id;
                    res.Data.content = result.Item2.Content;
                    res.Data.dateComment = $"{result.Item2.DateComment.Day}/{result.Item2.DateComment.Month}/{result.Item2.DateComment.Year}";
                    res.Data.avatarUser = $"{baseUrl}/public/{result.Item2.User.Avatar}";
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
