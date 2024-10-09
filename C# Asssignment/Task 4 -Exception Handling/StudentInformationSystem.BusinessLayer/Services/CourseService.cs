using StudentInformationSystem.BusinessLayer.Repository;
using StudentInformationSystem.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.BusinessLayer.Services
{
    public class CourseService : ICourseService
    {
        private readonly CourseRepository _courseRepository;

        public CourseService(CourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        // Adds a course to the repository
        public void AddCourse(string courseName, string courseCode, string instructor)
        {
            // Call the repository method to add the course with the provided parameters
            _courseRepository.AddCourse(courseName, courseCode, instructor);
        }

        // Retrieves a course by its ID
        public Course GetCourseByID(int courseID)
        {
            return _courseRepository.GetCourseByID(courseID);
        }

        // Updates course information
        public void UpdateCourse(Course updatedCourse)
        {
            _courseRepository.UpdateCourse(updatedCourse);
        }

        // Retrieves all courses
        public List<Course> GetAllCourses()
        {
            return _courseRepository.GetAllCourses();
        }
        // Implement AssignTeacherToCourse
        public void AssignTeacherToCourse(int courseId, int teacherId)
        {
            // Add the logic to assign the teacher to the course.
            Console.WriteLine($"Teacher {teacherId} assigned to Course {courseId}.");
        }

        // Implement GetCourseInfo
        public Course GetCourseInfo(int courseId)
        {
            // Add the logic to retrieve course information
            // This is just a placeholder; you should replace this with actual logic
            return new Course { CourseID = courseId, CourseName = "Example Course" };
        }

    }

}
