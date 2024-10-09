using StudentInformationSystem.BusinessLayer.Exceptions;
using StudentInformationSystem.BusinessLayer.Repository;
using StudentInformationSystem.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.BusinessLayer.Services
{
    public class StudentService
    {
        private StudentRepository _studentRepository;
        private CourseRepository _courseRepository; // Added CourseRepository reference

        public StudentService(StudentRepository studentRepository, CourseRepository courseRepository)
        {
            _studentRepository = studentRepository;
            _courseRepository = courseRepository; // Initialize CourseRepository
        }

        public void AddStudent(Student student)
        {
            _studentRepository.AddStudent(student);
        }

        public void RemoveStudent(int studentId)
        {
            var student = _studentRepository.GetStudentById(studentId);
            if (student != null)
            {
                _studentRepository.RemoveStudent(student);
            }
            else
            {
                throw new ArgumentException("Student not found");
            }
        }

        public void AddPayment(Payment payment)
        {
            _studentRepository.AddPayment(payment);
        }

        // Add an enrollment for a student to a course
        public void AddEnrollmentToCourse(int studentId, int courseId, DateTime enrollmentDate)
        {
            var student = _studentRepository.GetStudentById(studentId);
            var course = _courseRepository.GetCourseById(courseId); // Use the correct method

            if (student == null)
            {
                throw new ArgumentException("Student not found");
            }
            if (course == null)
            {
                throw new ArgumentException("Course not found");
            }

            _studentRepository.AddEnrollmentToCourse(course, student, enrollmentDate);
        }

        public List<Enrollment> GetEnrollmentsForStudent(int studentId)
        {
            var student = _studentRepository.GetStudentById(studentId);
            if (student == null)
            {
                throw new ArgumentException("Student not found");
            }
            return _studentRepository.GetEnrollmentsForStudent(student);
        }

        public List<Student> GetAllStudents()
        {
            return _studentRepository.GetAllStudents();
        }
    }
}







