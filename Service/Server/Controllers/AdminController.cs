using Application.Models.ModelsOfFaculty;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Server.FileMethods;
using Server.Requests.Form;
using System.Dynamic;
using System.Text.RegularExpressions;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly string baseUrl;
        private readonly IUnitOfWorkService uow;

        public AdminController(
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
        [Route("get-all-user")]
        public ActionResult GetAllUser()
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
                var checkIsAdmin = uow.UserService.CheckIsAdmin(userId);
                if (!checkIsAdmin.Item2)
                {
                    res.State = 0;
                    res.Data = new
                    {
                        mess = checkIsAdmin.Item1
                    };
                    return new JsonResult(res);
                }

                var result = uow.UserService.GetAllUser();
                if (result.Item2.IsNullOrEmpty())
                {
                    res.State = 0;
                    res.Data = new
                    {
                        mess = result.Item1
                    };
                    return new JsonResult(res);
                }
                var listUser = new List<object>();
                foreach (var item in result.Item2)
                {
                    if (item != null)
                    {
                        listUser.Add(new
                        {
                            id = item.Id,
                            email = item.PresentEmail,
                            fullName = $"{item.FirstName.Trim()} {item.LastName.Trim()}",
                            userName = item.UserName,
                            avatar = $"{baseUrl}/public/{item.Avatar}",
                            isLock = item.IsLock,
                            isVerify = item.IsVerified,
                            authorize = new
                            {
                                role = item.Role.Name,
                                name = item.Role.NormalizeName,
                            }
                        });
                    }
                }
                res.State = 1;
                res.Data = new
                {
                    listUser = listUser
                };
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
        [Route("set-lock")]
        public ActionResult SetLockUser(string idUserIsLocked)
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
                var checkIsAdmin = uow.UserService.CheckIsAdmin(userId);
                if (!checkIsAdmin.Item2)
                {
                    res.State = 0;
                    res.Data = new
                    {
                        mess = checkIsAdmin.Item1
                    };
                    return new JsonResult(res);
                }
                var checkIsAdminUser = uow.UserService.CheckIsAdmin(idUserIsLocked);
                if (checkIsAdminUser.Item2)
                {
                    res.State = 0;
                    res.Data = new
                    {
                        mess = $"Cannot lock Admin"
                    };
                    return new JsonResult(res);
                }
                var result = uow.UserService.SetIsLockUser(idUserIsLocked);
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
                    res.State = 1;
                    res.Data = new
                    {
                        mess = result.Item1,
                        id = result.Item2.Id,
                        isLock = result.Item2.IsLock,
                    };
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
        [Route("get-all-post")]
        public ActionResult GetAllPost()
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
                var checkIsAdmin = uow.UserService.CheckIsAdmin(userId);
                if (!checkIsAdmin.Item2)
                {
                    res.State = 0;
                    res.Data.mess = checkIsAdmin.Item1;
                    return new JsonResult(res);
                }

                var result = uow.PostService.GetAll();

                if (result.Item2.IsNullOrEmpty())
                {
                    res.State = 0;
                    res.Data.mess = result.Item1;
                    return new JsonResult(res);
                }
                else
                {
                    var user = result.Item2;
                    var listPostPublic = new List<object>();
                    if (!result.Item2.IsNullOrEmpty())
                    {
                        foreach (var item in result.Item2)
                        {

                            listPostPublic.Add(new
                            {
                                id = item.Id,
                                title = item.Title,
                                avatarPost = $"{baseUrl}/public/{item.AvatarPost}",
                                uploadDate = $"{item.DatePost.Day}/{item.DatePost.Month}/{item.DatePost.Year}",
                                linkDocPost = $"{baseUrl}/public/{item.LinkDocument}",
                            });
                        }

                    }
                    res.State = 1;
                    res.Data.listPost = listPostPublic;
                    return new JsonResult(res);
                }
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
        [Route("set-manager")]
        public ActionResult SetManager(string idUserSet, string idFaculty)
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
                var checkIsAdmin = uow.UserService.CheckIsAdmin(userId);
                if (!checkIsAdmin.Item2)
                {
                    res.State = 0;
                    res.Data = new
                    {
                        mess = checkIsAdmin.Item1
                    };
                    return new JsonResult(res);
                }
                var result = uow.RoleService.SetManager(idUserSet, idFaculty);
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
                    res.State = 1;
                    res.Data = new
                    {
                        id = result.Item2.Id,
                        authorize = new
                        {
                            role = result.Item2.Role.Name,
                            name = result.Item2.Role.NormalizeName,
                        },
                        faculty = result.Item2.Faculty.Name,
                    };
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
        [Route("set-user")]
        public ActionResult SetUser(string idUserSet)
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
                var checkIsAdmin = uow.UserService.CheckIsAdmin(userId);
                if (!checkIsAdmin.Item2)
                {
                    res.State = 0;
                    res.Data = new
                    {
                        mess = checkIsAdmin.Item1
                    };
                    return new JsonResult(res);
                }
                var result = uow.RoleService.SetUser(idUserSet);
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
                    res.State = 1;
                    res.Data = new
                    {
                        id = result.Item2.Id,
                        authorize = new
                        {
                            role = result.Item2.Role.Name,
                            name = result.Item2.Role.NormalizeName,
                        }
                    };
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
        [Route("set-student")]
        public ActionResult SetStudent(string idUserSet)
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
                var checkIsAdmin = uow.UserService.CheckIsAdmin(userId);
                var checkIsStaff = uow.UserService.CheckIsStaff(userId);
                if (!checkIsAdmin.Item2 && !checkIsStaff.Item2)
                {
                    res.State = 0;
                    res.Data = new
                    {
                        mess = "You can't do it!"
                    };
                    return new JsonResult(res);
                }

                var result = uow.RoleService.SetStudent(idUserSet);
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
                    res.State = 1;
                    res.Data = new
                    {
                        id = result.Item2.Id,
                        authorize = new
                        {
                            role = result.Item2.Role.Name,
                            name = result.Item2.Role.NormalizeName,
                        }
                    };
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
        [Route("set-staff")]
        public ActionResult SetStaff(string idUserSet)
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
                var checkIsAdmin = uow.UserService.CheckIsAdmin(userId);
                if (!checkIsAdmin.Item2)
                {
                    res.State = 0;
                    res.Data = new
                    {
                        mess = checkIsAdmin.Item1
                    };
                    return new JsonResult(res);
                }
                var result = uow.RoleService.SetStaff(idUserSet);
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
                    res.State = 1;
                    res.Data = new
                    {
                        id = result.Item2.Id,
                        authorize = new
                        {
                            role = result.Item2.Role.Name,
                            name = result.Item2.Role.NormalizeName,
                        }
                    };
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
        [Route("add-faculty")]
        public ActionResult AddFaculty([FromForm] CreateFacultyForm data)
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
                var checkIsAdmin = uow.UserService.CheckIsAdmin(userId);
                if (!checkIsAdmin.Item2)
                {
                    res.State = 0;
                    res.Data = new
                    {
                        mess = checkIsAdmin.Item1
                    };
                    return new JsonResult(res);
                }
                var newFaculty = new AddFacultyModel()
                {
                    Name = data.Name
                };
                var result = uow.FacultyService.Add(userId, newFaculty);
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
                    res.State = 1;
                    res.Data = new
                    {
                        id = result.Item2.Id,
                        name = result.Item2.Name,
                        isDelete = result.Item2.IsDeleted,
                    };
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

        [HttpDelete]
        [HttpOptions]
        [Route("del-faculty")]
        public ActionResult DeleteFaculty(string idFaculty)
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
                var checkIsAdmin = uow.UserService.CheckIsAdmin(userId);
                if (!checkIsAdmin.Item2)
                {
                    res.State = 0;
                    res.Data = new
                    {
                        mess = checkIsAdmin.Item1
                    };
                    return new JsonResult(res);
                }
                
                var result = uow.FacultyService.Delete(userId, idFaculty);
                if (result.Item2 == false)
                {
                    res.State = 0;
                    res.Data = new
                    {
                        mess = result.Item1
                    };
                }
                else
                {
                    res.State = 1;
                    res.Data = new
                    {
                        id = idFaculty,
                        isDelete = true,
                        mess = "Delete successful!"
                    };
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
        [Route("restore-faculty")]
        public ActionResult RestoreFaculty(string idFaculty)
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
                var checkIsAdmin = uow.UserService.CheckIsAdmin(userId);
                if (!checkIsAdmin.Item2)
                {
                    res.State = 0;
                    res.Data = new
                    {
                        mess = checkIsAdmin.Item1
                    };
                    return new JsonResult(res);
                }

                var result = uow.FacultyService.Restore(userId, idFaculty);
                if (result.Item2 == false)
                {
                    res.State = 0;
                    res.Data = new
                    {
                        mess = result.Item1
                    };
                }
                else
                {
                    res.State = 1;
                    res.Data = new
                    {
                        id = idFaculty,
                        isDelete = false,
                        mess = "Restore successful!"
                    };
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
        [Route("update-faculty")]
        public ActionResult UpdateFaculty([FromForm] EditFacultyForm data)
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
                var checkIsAdmin = uow.UserService.CheckIsAdmin(userId);
                if (!checkIsAdmin.Item2)
                {
                    res.State = 0;
                    res.Data = new
                    {
                        mess = checkIsAdmin.Item1
                    };
                    return new JsonResult(res);
                }
                var newFaculty = new EditFacultyModel()
                {
                    Id = data.Id,
                    Name = data.Name
                };
                var result = uow.FacultyService.Update(userId, newFaculty);
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
                    res.State = 1;
                    res.Data = new
                    {
                        id = result.Item2.Id,
                        name = result.Item2.Name,
                    };
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
        [Route("get-all-faculty")]
        public ActionResult GetAllFaculty()
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
                var checkIsAdmin = uow.UserService.CheckIsAdmin(userId);
                if (!checkIsAdmin.Item2)
                {
                    res.State = 0;
                    res.Data = new
                    {
                        mess = checkIsAdmin.Item1
                    };
                    return new JsonResult(res);
                }
                userId = HttpContext.Items["UserId"].ToString();
                var result = uow.FacultyService.GetAll(userId);
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
                    var listFaculty = new List<object>();
                    foreach (var item in result.Item2)
                    {
                        listFaculty.Add(new
                        {
                            id = item.Id,
                            name = item.Name,
                            isDelete = item.IsDeleted,
                        });
                    }
                    res.State = 1;
                    res.Data = new
                    {
                        listFaculty = listFaculty
                    };
                }
                return new JsonResult(res);
            }
            catch (Exception e)
            {
                res.State = -1;
                res.Data = new
                {
                    mess = e.Message,
                };
                return new JsonResult(res);
            }
        }

        [HttpGet]
        [HttpOptions]
        [Route("get-report-faculty")]
        public ActionResult GetReportPost()
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
                var checkIsAdmin = uow.UserService.CheckIsAdmin(userId);
                if (!checkIsAdmin.Item2)
                {
                    res.State = 0;
                    res.Data = new
                    {
                        mess = "You can't get it!"
                    };
                    return new JsonResult(res);
                }
                var result = uow.FacultyService.GetAllFacultyReport();
                if (result.Item2 == null)
                {
                    res.State = 0;
                    res.Data.mess = result.Item1;
                }
                else
                {
                    res.State = 1;
                    res.Data = result.Item2;
                }
                return new JsonResult(res);
            }
            catch (Exception e)
            {
                res.State = -1;
                res.Data.mess = e.Message;
                return new JsonResult(res);
            }
        }

        [HttpGet]
        [HttpOptions]
        [Route("get-date-value")]
        public ActionResult GetDateValue()
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
                var checkIsAdmin = uow.UserService.CheckIsAdmin(userId);
                var checkIsManager = uow.UserService.CheckIsMaanager(userId);
                if (!checkIsAdmin.Item2 && !checkIsManager.Item2)
                {
                    res.State = 0;
                    res.Data = new
                    {
                        mess = "You can't get it!"
                    };
                    return new JsonResult(res);
                }
                var maxYear = uow.PostService.GetMaxYearValue();
                var minYear = uow.PostService.GetMinYearValue();
                res.State = 1;
                res.Data.maxYear = maxYear;
                res.Data.minYear = minYear;
                return new JsonResult(res);
            }
            catch (Exception e)
            {
                res.State = -1;
                res.Data.mess = e.Message;
                return new JsonResult(res);
            }
        }
    }
}
