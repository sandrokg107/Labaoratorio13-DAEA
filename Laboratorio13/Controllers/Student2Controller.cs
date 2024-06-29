using Laboratorio13.Models;
using Laboratorio13.Models.Request.Student;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Laboratorio13.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class Student2Controller : ControllerBase
    {
        private readonly SchoolContext _context;
        public Student2Controller(SchoolContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public void Insert(StudentInsertRequest request) {
            try {
                Student student = new Student();
                student.FirstName = request.FirstName;
                student.LastName = request.LastName;
                student.Email = request.Email;
                student.Phone = request.Phone;
                student.GradeId = request.gradeId;
                student.Active = true;

                _context.Students.Add(student);
                _context.SaveChanges();
            }
            catch {
                BadRequest("Student not created");
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut]
        public void UpdateContact(StudentUpdateContactResquest request) {
            var student = _context.Students.FirstOrDefault(s => s.StudentId == request.StudentId);

            if (student == null) {
                BadRequest("Student not found");
            }
            else
            {
                student.Phone = request.Phone;
                student.Email = request.Email;
                _context.SaveChanges();
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut]
        public void UpdatePersonalData(StudentUpdatePersonalResquest request)
        {
            var student = _context.Students.FirstOrDefault(s => s.StudentId == request.StudentId);

            if (student == null)
            {
                BadRequest("Student not found");
            }
            else
            {
                student.FirstName = request.FirstName;
                student.LastName = request.LastName;
                _context.SaveChanges();
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public void InsertStudentsByGrade(StudentsInsertByGrade request) {
            try
            {
                List<Student> students = request.Students.Select(s => new Student
                {
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Email = s.Email,
                    Phone = s.Phone,
                    GradeId = request.GradeId,
                    Active = true
                }).ToList();

                _context.Students.AddRange(students);
                _context.SaveChanges();
            }
            catch
            {
                BadRequest("Failed to create students");
            }
        }
    }
}
