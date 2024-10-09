using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.Entity;

namespace StudentInformationSystem.BusinessLayer.Repository
{
    public class EnrollmentRepository
    {
        private List<Enrollment> enrollments = new List<Enrollment>();
        private StudentRepository _studentRepo;
        private CourseRepository _courseRepo;
        private readonly Dictionary<int, List<int>> _enrollmentData
            = new Dictionary<int, List<int>>(); 

        // Constructor to add an enrollment initially
        public EnrollmentRepository(int enrollmentID, int studentID, int courseID, DateTime enrollmentDate)
        {
            enrollments.Add(new Enrollment
            {
                EnrollmentID = enrollmentID,
                StudentID = studentID,
                CourseID = courseID,
                EnrollmentDate = enrollmentDate
            });
        }

        // Constructor that takes StudentRepository and CourseRepository as parameters
        public EnrollmentRepository(StudentRepository studentRepo, CourseRepository courseRepo)
        {
            _studentRepo = studentRepo;
            _courseRepo = courseRepo;
        }

        // Method to retrieve the student associated with the enrollment
        public Student GetStudent(int studentID)
        {
            return _studentRepo.GetStudentByID(studentID); 
        }

        // Method to retrieve the course associated with the enrollment
        public Course GetCourse(int courseID)
        {
            return _courseRepo.GetCourseByID(courseID); 
        }

        // Enroll Student in Course
        public void EnrollStudentInCourse(int studentID, int courseID)
        {
            if (!_enrollmentData.ContainsKey(studentID))
            {
                _enrollmentData[studentID] = new List<int>();
            }
            _enrollmentData[studentID].Add(courseID);
        }

        // Method to retrieve enrolled courses for a student
        public List<int> GetEnrolledCourses(int studentID)
        {
            return _enrollmentData.ContainsKey(studentID) ? _enrollmentData[studentID] : new List<int>();
        }
    }

}
