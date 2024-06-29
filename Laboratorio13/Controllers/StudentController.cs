using Laboratorio13.Models;
using Laboratorio13.Models.Request;
using Laboratorio13.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Laboratorio13.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly SchoolContext _context;
        public StudentController(SchoolContext context)
        {
            _context = context;
        }

        [HttpPost]
        public ActionResult<IEnumerable<StudentResponse>> GetStudentsByData1(StudentRequestD1 request)
        {
            try {
                string name = request.Name?.ToLower();
                string lastName = request.LastName?.ToLower();
                string email = request.Email?.ToLower();
                var students = _context.Students
                    .Where(s =>
                    (string.IsNullOrEmpty(name) || s.FirstName.ToLower().Contains(name)) &&
                    (string.IsNullOrEmpty(lastName) || s.LastName.ToLower().Contains(lastName)) &&
                    (string.IsNullOrEmpty(email) || s.Email.ToLower().Contains(email)))
                    .OrderByDescending(s => s.LastName)
                    .Select(s => new StudentResponse
                    {
                        FirstName = s.FirstName,
                        LastName = s.LastName,
                        Phone = s.Phone,
                        Email = s.Email
                    })
                    .ToList();

                return Ok(students);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<IEnumerable<StudentResponse>> GetStudentsByData2(StudentResquestD2 request)
        {
            try
            {
                string name = request.Name?.ToLower();
                var students = _context.Enrollments
                    .Include(enrollment => enrollment.Student)
                    .ThenInclude(student => student.Grade)
                    .Where(enrollment => (string.IsNullOrEmpty(name) || enrollment.Student.FirstName == name) &&
                    enrollment.Student.Grade.GradeId == request.GradeId)
                    .OrderByDescending(enrollment => enrollment.Course.Name)
                    .Select(
                        enrollment => new StudentResponse
                        {
                            FirstName = enrollment.Student.FirstName,
                            LastName = enrollment.Student.LastName,
                            Phone = enrollment.Student.Phone,
                            Email = enrollment.Student.Email,
                            Grade = enrollment.Student.Grade
                        }
                    )
                    .ToList();

                return Ok(students);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
