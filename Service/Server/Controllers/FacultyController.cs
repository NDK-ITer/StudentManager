using Application.Models.ModelsOfFaculty;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Server.Requests.Form;
using System.Dynamic;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacultyController : ControllerBase
    {
        private readonly IUnitOfWorkService uow;

        public FacultyController(
            IUnitOfWorkService uow
        )
        {
            this.uow = uow;
        }

        [HttpPost]
        [HttpOptions]
        [Route("create")]
        public ActionResult Create([FromForm] dynamic data)
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
                        Id = result.Item2.Id,
                        Name = result.Item2.Name,
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

        [HttpPut]
        [HttpOptions]
        [Route("edit")]
        public ActionResult Update([FromForm] dynamic data)
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
                var editFaculty = new EditFacultyModel()
                {
                    Id = data.ID,
                    Name = data.Name,
                };
                var result = uow.FacultyService.Update(userId, editFaculty);
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
                        Id = result.Item2.Id,
                        Name = result.Item2.Name,
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

        [HttpDelete]
        [HttpOptions]
        [Route("del/{id}")]
        public ActionResult Delete([FromRoute] string id)
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

                var result = uow.FacultyService.Delete(userId, id);
                if (result.Item2 == false)
                {
                    res.State = 0;
                }
                else
                {
                    res.State = 1;
                }
                res.Data = new
                {
                    mess = result.Item1
                };
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
        [Route("get-all")]
        public ActionResult GetAll()
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
                            Id = item.Id,
                            Name = item.Name,
                            IsDelete = item.IsDeleted
                        });
                    }
                    res.State = 1;
                    res.Data = listFaculty;
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
        [Route("get-public")]
        public ActionResult GetPublic()
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
                    var listFaculty = new List<object>();
                    foreach (var item in result.Item2)
                    {
                        listFaculty.Add(new
                        {
                            Id = item.Id,
                            Name = item.Name,
                        });
                    }
                    res.State = 0;
                    res.Data = listFaculty;
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


        [HttpPut]
        [HttpOptions]
        [Route("alow-upload/{facultyId}")]
        public ActionResult AlowUploadPost([FromRoute] string facultyId)
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
                var result = uow.FacultyService.AlowUploadPost(userId, facultyId);
                if (result.Item2 == false)
                    res.State = 0;
                else
                    res.State = 1;
                res.Data = new
                {
                    mess = result.Item1
                };
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
    }
}
