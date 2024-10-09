using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.BusinessLayer.Exceptions;
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
        //Method to add teacher
        public void AddTeacher(Teacher teacher)
        {
            // Validate teacher data
            if (string.IsNullOrWhiteSpace(teacher.FirstName) || string.IsNullOrWhiteSpace(teacher.LastName) || string.IsNullOrWhiteSpace(teacher.Email))
            {
                throw new InvalidTeacherDataException("Teacher's first name, last name, and email must not be empty.");
            }

            // Add the teacher to the list
            teachers.Add(teacher);
        }

        // Method to get the next available Teacher ID
        public int GetNextTeacherId()
        {
            if (teachers.Count == 0)
            {
                return 1; 
            }

            // Return the next ID
            return teachers.Max(t => t.TeacherID) + 1;
        }

       

        // Method to get a teacher by ID
        public Teacher GetById(int id)
        {
            var foundTeacher = teachers.Find(t => t.TeacherID == id);
            if (foundTeacher == null)
            {
                throw new TeacherNotFoundException($"Teacher with ID {id} not found.");
            }
            return foundTeacher;
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
