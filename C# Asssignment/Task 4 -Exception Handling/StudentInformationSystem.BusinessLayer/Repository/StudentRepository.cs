using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using StudentInformationSystem.BusinessLayer.Exceptions;
using StudentInformationSystem.Entity;

namespace StudentInformationSystem.BusinessLayer.Repository
{
    public class StudentRepository
    {
        private List<Student> _students = new List<Student>
        {
            new Student { StudentID = 101, FirstName = "John", LastName = "Doe", DateOfBirth = new DateTime(2000, 1, 1), Email = "john.doe@example.com", PhoneNumber = "1234567890", Balance = 1000.00m },
            new Student { StudentID = 102, FirstName = "Jane", LastName = "Smith", DateOfBirth = new DateTime(2001, 2, 2), Email = "jane.smith@example.com", PhoneNumber = "0987654321" , Balance = 1000.00m}
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

        public void EnrollInCourse(int studentID, int courseID, decimal payment)
        {
            // Check if the student exists
            var student = GetStudentByID(studentID);
            if (student == null)
            {
                throw new StudentNotFoundException($"Student with ID {studentID} not found.");
            }

            // Check if the course exists
            var course = _courseRepository.GetCourseByID(courseID);
            if (course == null)
            {
                throw new CourseNotFoundException($"Course with ID {courseID} not found.");
            }

            // Check for duplicate enrollment
            if (IsStudentEnrolledInCourse(studentID, courseID))
            {
                throw new DuplicateEnrollmentException($"Student with ID {studentID} is already enrolled in course {courseID}.");
            }
            // Check for invalid enrollment data
            if (studentID <= 0 || courseID <= 0)
            {
                throw new InvalidEnrollmentDataException("Invalid student ID or course ID.");
            }

            // Payment Validation
            if (payment <= 0)
            {
                throw new PaymentValidationException("Invalid payment amount. Payment must be greater than 0.");
            }

            // Check if the student has sufficient funds
            decimal availableFunds = GetStudentBalance(studentID);  // Assume this method returns the student's current balance

            if (payment > availableFunds)
            {
                throw new InsufficientFundsException(payment, availableFunds);
            }

            // If all checks pass, create and add the enrollment
            Enrollment enrollment = new Enrollment
            {
                EnrollmentID = _enrollments.Count + 1,
                StudentID = studentID,
                CourseID = courseID,
                EnrollmentDate = DateTime.Now
            };
            _enrollments.Add(enrollment);

            // Deduct the payment from the student's balance (if your system tracks this)
            UpdateStudentBalance(studentID, availableFunds - payment);  // Assuming this method updates the student's balance
        }



        private bool IsStudentEnrolledInCourse(int studentID, int courseID)
        {
            // Check if there's an enrollment matching the studentID and courseID
            return _enrollments.Any(enrollment => enrollment.StudentID == studentID && enrollment.CourseID == courseID);
        }

        // Method to get the student's current balance
        public decimal GetStudentBalance(int studentId)
        {
            var student = GetStudentByID(studentId);
            if (student == null)
            {
                throw new StudentNotFoundException($"Student with ID {studentId} not found.");
            }

            return student.Balance; // Assuming you have a Balance property in the Student entity
        }

        // Method to update the student's balance
        public void UpdateStudentBalance(int studentId, decimal newBalance)
        {
            var student = GetStudentByID(studentId);
            if (student == null)
            {
                throw new StudentNotFoundException($"Student with ID {studentId} not found.");
            }

            student.Balance = newBalance; // Assuming you have a Balance property in the Student entity
        }


        // Method to update student info
        public void UpdateStudentInfo(int studentID, string firstName, string lastName, DateTime dateOfBirth, string email, string phoneNumber)
        {
            // Find the student by ID
            Student student = _students.Find(s => s.StudentID == studentID);

            // Throw an exception if the student is not found
            if (student == null)
            {
                throw new StudentNotFoundException($"Student with ID {studentID} not found.");
            }

            // Validate the student data
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                throw new InvalidStudentDataException("Student's first name or last name cannot be empty.");
            }

            if (!IsValidEmail(email))
            {
                throw new InvalidStudentDataException("Invalid email format.");
            }

            if (dateOfBirth > DateTime.Now || dateOfBirth < new DateTime(1900, 1, 1))
            {
                throw new InvalidStudentDataException("Invalid date of birth.");
            }

            // If all validations pass, update the student info
            student.FirstName = firstName;
            student.LastName = lastName;
            student.DateOfBirth = dateOfBirth;
            student.Email = email;
            student.PhoneNumber = phoneNumber;
        }

        // Helper method to validate email format
        private bool IsValidEmail(string email)
        {
            // Simple regex check for email format
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
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
