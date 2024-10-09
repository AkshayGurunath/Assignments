using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.BusinessLayer.Exceptions;
using StudentInformationSystem.Entity;

namespace StudentInformationSystem.BusinessLayer.Repository
{
    public class CourseRepository
    {
        private List<Course> _courses;

        public CourseRepository()
        {
            _courses = new List<Course>();
        }

        public void AddCourse(Course course)
        {
            if (course != null)
            {
                _courses.Add(course);
            }
        }

        // Method to get a course by ID
        public Course GetCourseById(int courseId)
        {
            return _courses.Find(c => c.CourseID == courseId);
        }

        public void UpdateCourse(Course course)
        {
            var existingCourse = GetCourseById(course.CourseID); // Corrected to GetCourseById
            if (existingCourse != null)
            {
                existingCourse.CourseName = course.CourseName;
                // Update other properties as necessary
            }
        }

        public void RemoveCourse(int courseId) // Changed courseID to courseId for consistency
        {
            var course = GetCourseById(courseId); // Corrected to GetCourseById
            if (course != null)
            {
                _courses.Remove(course);
            }
        }

        public void AssignTeacherToCourse(int courseId, Teacher teacher)
        {
            var course = GetCourseById(courseId); // Corrected to GetCourseById
            if (course != null)
            {
                course.AssignTeacher(teacher);
            }
        }
    }

}


