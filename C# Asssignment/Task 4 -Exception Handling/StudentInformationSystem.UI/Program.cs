using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.Entity;
using StudentInformationSystem.BusinessLayer.Repository;
using StudentInformationSystem.BusinessLayer.Services;
using StudentInformationSystem.BusinessLayer.Exceptions;

namespace StudentInformationSystem.UI
{
    class Program
    {
        static void Main(string[] args)
        {

            TeacherRepository teacherRepo = new TeacherRepository();
            CourseRepository courseRepo = new CourseRepository();
            StudentRepository studentRepo = new StudentRepository(courseRepo);
            PaymentRepository paymentRepo = new PaymentRepository(); // Initialize PaymentRepository
            StudentService studentService = new StudentService(studentRepo, paymentRepo);

            while (true)
            {
                Console.WriteLine("\nSelect an option:");
                Console.WriteLine("1. Enroll a Student in a Course");
                Console.WriteLine("2. Assign a Teacher to a Course");
                Console.WriteLine("3. Record a Payment");
                Console.WriteLine("4. Generate Course Enrollment Report");
                Console.WriteLine("5. Generate Payment Report");
                Console.WriteLine("6. Update Student Info");
                Console.WriteLine("7. Add a Techer");
                Console.WriteLine("8. Add a course");
                
                Console.WriteLine("9. Calculate Course Statistics ");

                Console.WriteLine("10. Exit");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        EnrollStudentInCourse(studentRepo, courseRepo);
                        break;

                    case "2":
                        AssignTeacherToCourse(teacherRepo, courseRepo);
                        break;

                    case "3":
                        RecordPayment(studentRepo, paymentRepo);
                        break;

                    case "4":
                        GenerateCourseEnrollmentReport(studentService, courseRepo);
                        break;

                    case "5":
                        GeneratePaymentReport(studentService, studentRepo);
                        break;

                    case "6":
                        UpdateStudentInfo(studentRepo); 
                        break;

                    case "7":
                        AddTeacher(teacherRepo); 
                        break;

                    case "8":
                        AddNewCourse(courseRepo);
                        break;



                    case "9":
                        Console.WriteLine("Exiting the application.");
                        return;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void EnrollStudentInCourse(StudentRepository studentRepository, CourseRepository courseRepository)
        {
            Console.Write("Enter Student ID to enroll: ");
            int studentID = int.Parse(Console.ReadLine());

            Console.Write("Enter Course ID to enroll in: ");
            int courseID = int.Parse(Console.ReadLine());

            Console.Write("Enter Payment Amount: ");
            decimal paymentAmount = decimal.Parse(Console.ReadLine());

            try
            {
                // Enroll the student in the course
                studentRepository.EnrollInCourse(studentID, courseID, paymentAmount);
                var student = studentRepository.GetStudentByID(studentID);
                var course = courseRepository.GetCourseByID(courseID);
                Console.WriteLine($"{student.FirstName} has been enrolled in {course.CourseName}.");
            }
            catch (StudentNotFoundException ex)
            {
                Console.WriteLine(ex.Message); // Handle student not found
            }
            catch (CourseNotFoundException ex)
            {
                Console.WriteLine(ex.Message); // Handle course not found
            }
            catch (DuplicateEnrollmentException ex)
            {
                Console.WriteLine(ex.Message); // Handle duplicate enrollment
            }
            catch (InvalidEnrollmentDataException ex) // Handle invalid enrollment data
            {
                Console.WriteLine(ex.Message);
            }
            catch (PaymentValidationException ex)
            {
                Console.WriteLine(ex.Message); // Handle invalid payment
            }

            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred: " + ex.Message); 
            }
        }

        static void AssignTeacherToCourse(TeacherRepository teacherRepo, CourseRepository courseRepo)
        {
            Console.WriteLine("Enter Teacher ID to assign:");
            int teacherId = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Course ID to assign to:");
            int courseId = int.Parse(Console.ReadLine());

            try
            {
                Teacher teacher = teacherRepo.GetById(teacherId); 
                Course course = courseRepo.GetCourseByID(courseId);

                if (course != null)
                {
                    // Code to assign the teacher to the course
                    Console.WriteLine($"{teacher.FirstName} has been assigned to {course.CourseName}.");
                }
                else
                {
                    Console.WriteLine("Course not found.");
                }
            }
            catch (TeacherNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}"); // Handle teacher not found
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }

        static void RecordPayment(StudentRepository studentRepo, PaymentRepository paymentRepo)
        {
            try
            {
                Console.WriteLine("Enter Student ID to record payment:");
                int studentId = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter payment amount:");
                decimal amount = decimal.Parse(Console.ReadLine());

                Console.WriteLine("Enter payment date (yyyy-mm-dd):");
                DateTime paymentDate = DateTime.Parse(Console.ReadLine());

                // Record the payment
                paymentRepo.RecordPayment(studentId, amount, paymentDate);
                Console.WriteLine($"Payment of {amount} recorded for student ID {studentId} on {paymentDate.ToShortDateString()}.");
            }
            catch (StudentNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (PaymentValidationException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }

        static void GenerateCourseEnrollmentReport(StudentService studentService, CourseRepository courseRepo)
        {
            Console.Write("Enter Course ID for the report: ");
            int courseID = int.Parse(Console.ReadLine());


            var course = courseRepo.GetCourseByID(courseID);
            if (course == null)
            {
                Console.WriteLine("Course not found.");
                return;
            }

            studentService.GenerateEnrollmentReport(course);
        }

        static void GeneratePaymentReport(StudentService studentService, StudentRepository studentRepo)
        {

            Console.Write("Enter Student ID for the payment report: ");
            int studentID = int.Parse(Console.ReadLine());


            var student = studentRepo.GetStudentByID(studentID);


            if (student == null)
            {
                Console.WriteLine("Student not found.");
                return;
            }


            studentService.GeneratePaymentReport(studentID);
        }

        static void UpdateStudentInfo(StudentRepository studentRepo)
        {
            int studentId;
            while (true)
            {
                Console.Write("Enter Student ID to update: ");
                if (int.TryParse(Console.ReadLine(), out studentId))
                {
                    break; // Exit the loop if the input is a valid integer
                }
                Console.WriteLine("Invalid input. Please enter a valid Student ID.");
            }

            string firstName;
            while (true)
            {
                Console.Write("Enter new First Name: ");
                firstName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(firstName))
                {
                    break; // Exit the loop if the input is valid
                }
                Console.WriteLine("First Name cannot be empty. Please enter a valid First Name.");
            }

            string lastName;
            while (true)
            {
                Console.Write("Enter new Last Name: ");
                lastName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(lastName))
                {
                    break; // Exit the loop if the input is valid
                }
                Console.WriteLine("Last Name cannot be empty. Please enter a valid Last Name.");
            }

            DateTime dateOfBirth;
            while (true)
            {
                Console.Write("Enter new Date of Birth (yyyy-mm-dd): ");
                if (DateTime.TryParse(Console.ReadLine(), out dateOfBirth) && dateOfBirth <= DateTime.Now)
                {
                    break; // Exit the loop if the input is a valid date
                }
                Console.WriteLine("Invalid input. Please enter a valid Date of Birth (it cannot be in the future).");
            }

            string email;
            while (true)
            {
                Console.Write("Enter new Email: ");
                email = Console.ReadLine();
                if (email.Contains("@") && email.Contains("."))
                {
                    break; // Basic email validation
                }
                Console.WriteLine("Invalid email format. Please enter a valid Email.");
            }

            string phoneNumber;
            while (true)
            {
                Console.Write("Enter new Phone Number: ");
                phoneNumber = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(phoneNumber))
                {
                    break; // Exit the loop if the input is valid
                }
                Console.WriteLine("Phone Number cannot be empty. Please enter a valid Phone Number.");
            }

            try
            {
                studentRepo.UpdateStudentInfo(studentId, firstName, lastName, dateOfBirth, email, phoneNumber);
                Console.WriteLine("Student information updated successfully.");
            }
            catch (StudentNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (InvalidStudentDataException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }


        }
        static void AddTeacher(TeacherRepository teacherRepo)
        {
            Console.Write("Enter First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter Last Name: ");
            string lastName = Console.ReadLine();

            Console.Write("Enter Email: ");
            string email = Console.ReadLine();

            // Create a new Teacher object
            Teacher newTeacher = new Teacher
            {
                TeacherID = teacherRepo.GetNextTeacherId(), 
                FirstName = firstName,
                LastName = lastName,
                Email = email
            };

            try
            {
                teacherRepo.AddTeacher(newTeacher);
                Console.WriteLine("Teacher added successfully.");
            }
            catch (InvalidTeacherDataException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }
        static void AddNewCourse(CourseRepository courseRepo)
        {
            Console.Write("Enter Course Name: ");
            string courseName = Console.ReadLine();

            Console.Write("Enter Course Code: ");
            string courseCode = Console.ReadLine();

            Console.Write("Enter Instructor Name: ");
            string instructor = Console.ReadLine();

            try
            {
                courseRepo.AddCourse(courseName, courseCode, instructor);
                Console.WriteLine("Course added successfully.");
            }
            catch (InvalidCourseDataException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }


    }
}



    
