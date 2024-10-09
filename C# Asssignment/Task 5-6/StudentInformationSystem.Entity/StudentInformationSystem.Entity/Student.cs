using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StudentInformationSystem.Entity
{
    public class Student
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public List<Enrollment> Enrollments { get; set; } // List of enrollments
        public List<Payment> Payments { get; set; } // Optional: List of payments for this student

        // Constructor
        public Student(int studentId, string studentName)
        {
            Enrollments = new List<Enrollment>(); // Initialize the list
            Payments = new List<Payment>(); // Initialize the payments list if needed
        }
        

        public Student(string studentName)
        {
        }
    }
}
