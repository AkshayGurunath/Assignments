using StudentInformationSystem.BusinessLayer.Repository;
using StudentInformationSystem.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.BusinessLayer.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly EnrollmentRepository _enrollmentRepository;

        // Constructor that takes EnrollmentRepository
        public EnrollmentService(EnrollmentRepository enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
        }

        // Method to retrieve the student associated with the enrollment
        public Student GetStudent(int studentID)
        {
            return _enrollmentRepository.GetStudent(studentID);
        }

        // Method to retrieve the course associated with the enrollment
        public Course GetCourse(int courseID)
        {
            return _enrollmentRepository.GetCourse(courseID);
        }

        // Alternative constructor accepting IEnrollmentRepository
        public EnrollmentService(IEnrollmentRepository enrollmentRepository)
        {
            
            _enrollmentRepository = (EnrollmentRepository)enrollmentRepository;
        }

        public void EnrollStudentInCourse(int studentID, int courseID)
        {
            _enrollmentRepository.EnrollStudentInCourse(studentID, courseID);
        }

        public List<int> GetEnrolledCourses(int studentID)
        {
            return _enrollmentRepository.GetEnrolledCourses(studentID);
        }
    }

}
