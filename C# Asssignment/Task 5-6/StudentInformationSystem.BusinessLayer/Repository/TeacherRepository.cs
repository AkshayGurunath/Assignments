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
        private List<Teacher> _teachers = new List<Teacher>();

        public TeacherRepository()
        {
            // Example of initializing a teacher
            _teachers.Add(new Teacher { TeacherID = 301, FirstName = "Alice", LastName = "Johnson", Email = "alice.johnson@example.com", AssignedCourses = new List<Course>() });
            _teachers.Add(new Teacher { TeacherID = 302, FirstName = "Bob", LastName = "Williams", Email = "bob.williams@example.com", AssignedCourses = new List<Course>() });
        }


        public void AddTeacher(Teacher teacher)
        {
            _teachers.Add(teacher);
        }

        public Teacher GetById(int teacherID)
        {
            return _teachers.Find(t => t.TeacherID == teacherID);
        }

        public List<Teacher> GetAllTeachers() // New method to get all teachers
        {
            return _teachers;
        }

        public void UpdateTeacher(Teacher teacher)
        {
            var existingTeacher = GetById(teacher.TeacherID);
            if (existingTeacher != null)
            {
                existingTeacher.FirstName = teacher.FirstName;
                existingTeacher.LastName = teacher.LastName;
                existingTeacher.Email = teacher.Email;
                // Update other properties as necessary
            }
        }

        public void RemoveTeacher(int teacherID)
        {
            var teacher = GetById(teacherID);
            if (teacher != null)
            {
                _teachers.Remove(teacher);
            }
        }
    }

}
