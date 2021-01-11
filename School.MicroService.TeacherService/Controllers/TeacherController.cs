using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.MicroService.TeacherService.Models;
using School.MicroService.TeacherService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.MicroService.TeacherService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private ITeacherService _service;

        public TeacherController(ITeacherService service)
        {
            _service = service;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Teacher>> GetTeachers()
        {
            return _service.GetTeachers().ToList();
        }
    }
}
