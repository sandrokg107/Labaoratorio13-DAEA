using Laboratorio13.Models;
using Laboratorio13.Models.Request.Grade;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Laboratorio13.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class Grades2Controller : ControllerBase
    {
        private readonly SchoolContext _context;

        public Grades2Controller(SchoolContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public void Insert(GradeInsertRequest request) {
            try { 
                Grade grade = new Grade();
                grade.Name = request.Name;
                grade.Description = request.Description;
                grade.Active = true;
                _context.Grades.Add(grade);
                _context.SaveChanges();
            }
            catch {
                BadRequest("Grade not created");
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete]
        public void Delete(GradeDeleteRequest request) {
            var gradeDelete = _context.Grades.FirstOrDefault(g => g.GradeId == request.GradeId);

            if (gradeDelete == null) {
                BadRequest("Grade not found");
            } else
            {
                gradeDelete.Active = false;
                _context.SaveChanges();
            }
        }
    }
}
