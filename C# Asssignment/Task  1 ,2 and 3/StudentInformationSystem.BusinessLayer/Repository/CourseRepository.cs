using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.Entity;

namespace StudentInformationSystem.BusinessLayer.Repository
{
    public class CourseRepository 
    {
        private readonly List<Course> courses;
        private readonly List<Enrollment> enrollments;
        private readonly List<Payment> payments;

        public CourseRepository()
        {
            // Initializing with some sample data
            courses = new List<Course>
            {
                new Course { CourseID = 1, CourseName = "Math 101" },
                new Course { CourseID = 2, CourseName = "Physics 101" }
            };

            enrollments = new List<Enrollment>
            {
                new Enrollment { StudentID = 101, CourseID = 1 },
                new Enrollment { StudentID = 102, CourseID = 1 },
                new Enrollment { StudentID = 103, CourseID = 2 }
            };

            payments = new List<Payment>
            {
                new Payment { PaymentID = 1, StudentID = 101, Amount = 5000, PaymentDate = new System.DateTime(2024, 1, 15) },
                new Payment { PaymentID = 2, StudentID = 102, Amount = 3000, PaymentDate = new System.DateTime(2024, 2, 15) },
                new Payment { PaymentID = 3, StudentID = 103, Amount = 4000, PaymentDate = new System.DateTime(2024, 3, 15) }
            };
        }

       
        public void AddCourse(Course course)
        {
            courses.Add(course); 
        }

        // Get a course by its ID
        public Course GetCourseByID(int courseID)
        {
            return courses.FirstOrDefault(c => c.CourseID == courseID); 
        }

        // Update course information
        public void UpdateCourse(Course updatedCourse)
        {
            var course = GetCourseByID(updatedCourse.CourseID);
            if (course != null)
            {
                course.CourseName = updatedCourse.CourseName;
                course.CourseCode = updatedCourse.CourseCode;
                course.InstructorName = updatedCourse.InstructorName;
            }
        }

        // Retrieve all courses
        public List<Course> GetAllCourses()
        {
            return courses; 
        }

        // Assign teacher to course
        public void AssignTeacherToCourse(int teacherID, int courseID)
        {
            Console.WriteLine($"Teacher {teacherID} assigned to course {courseID}.");
        }

        //GetCourseInfo
        public string GetCourseInfo(int courseId)
        {
            var course = GetCourseByID(courseId); 
            if (course != null)
            {
                return $"Course ID: {course.CourseID}, Course Name: {course.CourseName}, CourseCode = {course.CourseCode}, InstructorName = {course.InstructorName}";
            }

            return "Course not found."; 
        }
    }

}
