namespace Laboratorio13.Models.Request.Student
{
    public class StudentsInsertByGrade
    {
        public List<StudentResquest> Students { get; set; }
        public int GradeId { get; set; }
    }
}
