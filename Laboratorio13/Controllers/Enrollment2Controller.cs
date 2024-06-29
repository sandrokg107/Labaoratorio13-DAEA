using Laboratorio13.Models;
using Laboratorio13.Models.Request.Enrollment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Laboratorio13.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class Enrollment2Controller : ControllerBase
    {
        private readonly SchoolContext _context;
        public Enrollment2Controller(SchoolContext context) { 
            _context = context;
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public void Insert(EnrollmentRequest request) {
            try {
                List<int> coursesid = request.CoursesId;
                foreach (var course in coursesid)
                {
                    Enrollment enrollment = new Enrollment();
                    enrollment.StudentId = request.StudentId;
                    enrollment.Active = true;
                    enrollment.CourseId = course;
                    enrollment.Date = DateTime.Now;
                    _context.Enrollments.Add(enrollment);
                };
                _context.SaveChanges();

            }
            catch {
                BadRequest("Enrollments not create");
            }
        }
    }
}
