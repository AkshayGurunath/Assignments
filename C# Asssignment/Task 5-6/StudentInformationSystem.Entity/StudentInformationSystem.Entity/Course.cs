using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Entity
{
    public class Course
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public List<Teacher> AssignedTeachers { get; set; }
        public List<Enrollment> Enrollments { get; set; } // Added Enrollments property
        public bool Name { get; set; }

        public Course(int courseId, string courseName)
        {
            CourseID = courseId;
            CourseName = courseName;
            AssignedTeachers = new List<Teacher>();
            Enrollments = new List<Enrollment>(); // Initialize Enrollments list
        }

        public Course(string courseName)
        {
            CourseName = courseName;
        }

        // Method to assign a teacher to this course
        public void AssignTeacher(Teacher teacher)
        {
            if (teacher != null && !AssignedTeachers.Contains(teacher))
            {
                AssignedTeachers.Add(teacher);
            }
        }

        // Method to add an enrollment to the course
        public void AddEnrollment(Enrollment enrollment)
        {
            if (enrollment != null && !Enrollments.Contains(enrollment))
            {
                Enrollments.Add(enrollment);
            }
        }
    }


}
