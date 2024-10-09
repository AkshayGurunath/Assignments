using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Entity
{
    public class Teacher
    {
        public int TeacherID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<Course> AssignedCourses { get; set; }

        // Parameterless constructor
        public Teacher()
        {
            AssignedCourses = new List<Course>(); // Initialize the list
        }

        // Optional: Parameterized constructor for convenience
        public Teacher(int teacherId, string firstName, string lastName, string email)
        {
            TeacherID = teacherId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            AssignedCourses = new List<Course>(); // Initialize the list
        }
    }
    //public class Teacher
    //{
    //    public int TeacherID { get; set; }
    //    public string FirstName { get; set; }
    //    public string LastName { get; set; }
    //    public string Email { get; set; }

    //    // Collection to hold references to Course objects
    //    public List<Course> AssignedCourses { get; set; } = new List<Course>();

    //    // Constructor
    //    public Teacher(int teacherId, string teacherName)
    //    {
    //        AssignedCourses = new List<Course>();
    //    }

    //    public Teacher(string teacherName)
    //    {
    //    }
    //}


}
