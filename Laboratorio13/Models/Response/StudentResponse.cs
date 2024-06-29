namespace Laboratorio13.Models.Response
{
    public class StudentResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int ?GradeId { get; set; }
        public Grade ?Grade { get; set; }
    }
}
