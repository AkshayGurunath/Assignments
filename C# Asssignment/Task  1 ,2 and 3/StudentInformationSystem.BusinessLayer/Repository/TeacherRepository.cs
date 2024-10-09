using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.Entity;

namespace StudentInformationSystem.BusinessLayer.Repository
{
    public class TeacherRepository
    {
        private List<Teacher> teachers = new List<Teacher>()
        { 
        new Teacher { TeacherID = 1, FirstName = "Alice", LastName = "Johnson" },
        new Teacher { TeacherID = 2, FirstName = "Bob", LastName = "Smith" }
        };

public TeacherRepository()
        {


        }
        // Constructor to add a teacher initially
        public TeacherRepository(int teacherID, string firstName, string lastName, string email)
        {
            teachers.Add(new Teacher
            {
                TeacherID = teacherID,
                FirstName = firstName,
                LastName = lastName,
                Email = email
            });
        }

        // Method to update teacher information
        public void UpdateTeacherInfo(int teacherID, string firstName, string lastName, string email)
        {
            Teacher teacher = teachers.Find(t => t.TeacherID == teacherID);
            if (teacher != null)
            {
                teacher.FirstName = firstName; 
                teacher.LastName = lastName;
                teacher.Email = email;
            }
        }
        // Method to get a teacher by ID
        public Teacher GetById(int id)
        {
            return teachers.Find(teacher => teacher.TeacherID == id);
        }

        // Method to display detailed information about the teacher
        public void DisplayTeacherInfo()
        {
            // Implementation for displaying teacher info
        }

        // Method to retrieve a list of courses assigned to the teacher
        public List<Course> GetAssignedCourses()
        {
            
            return new List<Course>();
        }
    }

}
