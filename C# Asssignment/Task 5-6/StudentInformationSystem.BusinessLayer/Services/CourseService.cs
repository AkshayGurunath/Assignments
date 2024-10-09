using StudentInformationSystem.BusinessLayer.Repository;
using StudentInformationSystem.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.BusinessLayer.Services
{
    public class CourseService
    {
        private CourseRepository _courseRepository;

        public CourseService()
        {
            _courseRepository = new CourseRepository();
        }

        public void AddCourse(Course course)
        {
            _courseRepository.AddCourse(course);
        }

        public Course GetCourseByID(int courseID)
        {
            return _courseRepository.GetCourseById(courseID);
        }

        public void UpdateCourse(Course course)
        {
            _courseRepository.UpdateCourse(course);
        }

        public void RemoveCourse(int courseID)
        {
            _courseRepository.RemoveCourse(courseID);
        }

        public void AssignTeacherToCourse(int courseId, Teacher teacher)
        {
            _courseRepository.AssignTeacherToCourse(courseId, teacher);
        }
    }
}

