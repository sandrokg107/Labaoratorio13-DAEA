namespace Laboratorio13.Models.Request.Enrollment
{
    public class EnrollmentRequest
    {
        public int StudentId { get; set; }
        public List<int> CoursesId { get; set; }
    }
}
