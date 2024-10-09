using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.Entity;

namespace StudentInformationSystem.BusinessLayer.Repository
{
    public class StudentRepository
    {
        private List<Student> _students = new List<Student>
        {
            new Student { StudentID = 101, FirstName = "John", LastName = "Doe", DateOfBirth = new DateTime(2000, 1, 1), Email = "john.doe@example.com", PhoneNumber = "1234567890" },
            new Student { StudentID = 102, FirstName = "Jane", LastName = "Smith", DateOfBirth = new DateTime(2001, 2, 2), Email = "jane.smith@example.com", PhoneNumber = "0987654321" }
        };

        private List<Enrollment> _enrollments = new List<Enrollment>();
        private List<Payment> _payments = new List<Payment>();
        private CourseRepository _courseRepository;

        // Constructor for dependency injection
        public StudentRepository(CourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        // Constructor to add a student initially
        public StudentRepository(int studentID, string firstName, string lastName, DateTime dateOfBirth, string email, string phoneNumber)
        {
            _students.Add(new Student
            {
                StudentID = studentID,
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth,
                Email = email,
                PhoneNumber = phoneNumber
            });
        }

        // Method to get a student by ID
        public Student GetStudentByID(int studentID)
        {
            return _students.FirstOrDefault(s => s.StudentID == studentID);
        }

        // Method to enroll student in a course
        public void EnrollInCourse(int studentID, int courseID)
        {
            // Check if the student and course exist
            var student = GetStudentByID(studentID);
            var course = _courseRepository.GetCourseByID(courseID);

            if (student == null)
            {
                Console.WriteLine("Student not found.");
                return;
            }
            if (course == null)
            {
                Console.WriteLine("Course not found.");
                return;
            }

            Enrollment enrollment = new Enrollment
            {
                EnrollmentID = _enrollments.Count + 1,
                StudentID = studentID,
                CourseID = courseID,
                EnrollmentDate = DateTime.Now
            };
            _enrollments.Add(enrollment);
            Console.WriteLine($"{student.FirstName} has been enrolled in {course.CourseName}.");
        }

        // Method to update student info
        public void UpdateStudentInfo(int studentID, string firstName, string lastName, DateTime dateOfBirth, string email, string phoneNumber)
        {
            Student student = _students.Find(s => s.StudentID == studentID);
            if (student != null)
            {
                student.FirstName = firstName;
                student.LastName = lastName;
                student.DateOfBirth = dateOfBirth;
                student.Email = email;
                student.PhoneNumber = phoneNumber;
            }
        }

        // Method to make a payment
        public void MakePayment(int studentID, decimal amount, DateTime paymentDate)
        {
            Payment payment = new Payment
            {
                PaymentID = _payments.Count + 1,
                StudentID = studentID,
                Amount = amount,
                PaymentDate = paymentDate
            };
            _payments.Add(payment);
            Console.WriteLine($"Payment of {amount} recorded for Student ID {studentID} on {paymentDate.ToShortDateString()}.");
        }

        // Method to display detailed student info
        public void DisplayStudentInfo(int studentID)
        {
            Student student = _students.Find(s => s.StudentID == studentID);
            if (student != null)
            {
                Console.WriteLine($"Student ID: {student.StudentID}");
                Console.WriteLine($"Name: {student.FirstName} {student.LastName}");
                Console.WriteLine($"Date of Birth: {student.DateOfBirth.ToShortDateString()}");
                Console.WriteLine($"Email: {student.Email}");
                Console.WriteLine($"Phone Number: {student.PhoneNumber}");
            }
            else
            {
                Console.WriteLine("Student not found.");
            }
        }

        // Method to get list of enrolled courses
        public List<Course> GetEnrolledCourses(int studentID)
        {
            List<Course> enrolledCourses = new List<Course>();
            foreach (var enrollment in _enrollments)
            {
                if (enrollment.StudentID == studentID)
                {
                    Course course = _courseRepository.GetCourseByID(enrollment.CourseID);
                    if (course != null)
                    {
                        enrolledCourses.Add(course);
                    }
                }
            }
            return enrolledCourses;
        }

        // Method to get payment history for a student
        public List<Payment> GetPaymentHistory(int studentID)
        {
            List<Payment> paymentHistory = _payments.FindAll(p => p.StudentID == studentID);
            return paymentHistory;
        }
        public void GenerateEnrollmentReport(Course course)
        {
            // Get all enrollments for the specific course
            var enrolledStudents = _enrollments.Where(e => e.CourseID == course.CourseID).ToList();

            if (enrolledStudents.Count == 0)
            {
                Console.WriteLine($"No students are enrolled in {course.CourseName}.");
                return;
            }

            // Display report header
            Console.WriteLine($"--- Enrollment Report for {course.CourseName} ---");

            // Iterate through each enrollment and display student info
            foreach (var enrollment in enrolledStudents)
            {
                var student = _students.FirstOrDefault(s => s.StudentID == enrollment.StudentID);
                if (student != null)
                {
                    Console.WriteLine($"Student ID: {student.StudentID}, Name: {student.FirstName} {student.LastName}, Enrollment Date: {enrollment.EnrollmentDate.ToShortDateString()}");
                }
            }

        }
    }
}
