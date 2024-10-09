using StudentInformationSystem.BusinessLayer.Repository;
using StudentInformationSystem.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.BusinessLayer.Services
{
    public class StudentService : IStudentService
    {

        private readonly IPaymentRepository _paymentRepository;
        private readonly StudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;

        // Constructor for StudentService with both repositories injected
        public StudentService(StudentRepository studentRepository, IPaymentRepository paymentRepository)
        {
            _studentRepository = studentRepository;
            _paymentRepository = paymentRepository;
        }

        // Enrolls a student in a course
        public void EnrollInCourse(int studentID, int courseID)
        {
            _studentRepository.EnrollInCourse(studentID, courseID);
        }

        // Updates student's information
        public void UpdateStudentInfo(int studentID, string firstName, string lastName, DateTime dateOfBirth, string email, string phoneNumber)
        {
            _studentRepository.UpdateStudentInfo(studentID, firstName, lastName, dateOfBirth, email, phoneNumber);
        }

        // Displays detailed information about the student
        public void DisplayStudentInfo(int studentID)
        {
            _studentRepository.DisplayStudentInfo(studentID);
        }

        // Records a payment made by the student
        public void MakePayment(int studentID, decimal amount, DateTime paymentDate)
        {
            _studentRepository.MakePayment(studentID, amount, paymentDate);
        }

        // Retrieves a list of courses in which the student is enrolled
        public List<Course> GetEnrolledCourses(int studentID)
        {
            return _studentRepository.GetEnrolledCourses(studentID);
        }

        // Retrieves a list of payment records for the student
        public List<Payment> GetPaymentHistory(int studentID)
        {
            return _studentRepository.GetPaymentHistory(studentID);
        }

        // Generate the enrollment report for a specific course
        public void GenerateEnrollmentReport(Course course)
        {
            _studentRepository.GenerateEnrollmentReport(course);
        }

        public void GeneratePaymentReport(int studentID)
        {
            // Fetch the student from the repository using studentID
            var student = _studentRepository.GetStudentByID(studentID);
            if (student == null)
            {
                Console.WriteLine("Student not found.");
                return;
            }

            // Fetch payment history and generate the report
            List<Payment> payments = _studentRepository.GetPaymentHistory(studentID);

            // Generate the payment report
            if (payments.Count > 0)
            {
                Console.WriteLine($"Payment report for {student.FirstName} {student.LastName}:");
                foreach (var payment in payments)
                {
                    Console.WriteLine($"Payment ID: {payment.PaymentID}, Amount: {payment.Amount}, Date: {payment.PaymentDate.ToShortDateString()}");
                }
            }
            else
            {
                Console.WriteLine($"{student.FirstName} {student.LastName} has no payment records.");
            }
        }
        public CourseStatistics CalculateCourseStatistics(int courseID)
        {
            var numberOfEnrollments = _courseRepository.GetEnrollmentsByCourseID(courseID);
            var payments = _courseRepository.GetPaymentsByCourseID(courseID);
            var totalPayments = payments.Sum(p => p.Amount);

            return new CourseStatistics
            {
                CourseID = courseID,
                NumberOfEnrollments = numberOfEnrollments,
                TotalPayments = totalPayments
            };
        }
    }
}


    


