using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.Entity;
using StudentInformationSystem.BusinessLayer.Repository;
using StudentInformationSystem.BusinessLayer.Services;

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
            StudentService studentService = new StudentService(studentRepo ,paymentRepo); 

            while (true)
            {
                Console.WriteLine("\nSelect an option:");
                Console.WriteLine("1. Enroll a Student in a Course");
                Console.WriteLine("2. Assign a Teacher to a Course");
                Console.WriteLine("3. Record a Payment");
                Console.WriteLine("4. Generate Course Enrollment Report");
                Console.WriteLine("5. Generate Payment Report");
                Console.WriteLine("6. Calculate Course Statistics ");
                Console.WriteLine("7. Exit");

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
                        RecordPayment(studentRepo, paymentRepo); // Pass paymentRepo to RecordPayment
                        break;

                    case "4":
                        GenerateCourseEnrollmentReport(studentService, courseRepo);
                        break;

                    case "5":
                        GeneratePaymentReport(studentService, studentRepo);
                        break;
                        

                    case "6":
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

            
            var student = studentRepository.GetStudentByID(studentID);
            if (student == null)
            {
                Console.WriteLine("Student not found.");
                return;
            }

            
            var course = courseRepository.GetCourseByID(courseID);
            if (course == null)
            {
                Console.WriteLine("Course not found.");
                return;
            }

         
            studentRepository.EnrollInCourse(studentID, courseID);
            Console.WriteLine($"{student.FirstName} has been enrolled in {course.CourseName}.");
        }

        static void AssignTeacherToCourse(TeacherRepository teacherRepo, CourseRepository courseRepo)
        {
            Console.WriteLine("Enter Teacher ID to assign:");
            int teacherId = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Course ID to assign to:");
            int courseId = int.Parse(Console.ReadLine());

            
            Teacher teacher = teacherRepo.GetById(teacherId);
            Course course = courseRepo.GetCourseByID(courseId);

            if (teacher != null && course != null)
            {
                
                Console.WriteLine($"{teacher.FirstName} has been assigned to {course.CourseName}.");
            }
            else
            {
                Console.WriteLine("Teacher or Course not found.");
            }
        }

        static void RecordPayment(StudentRepository studentRepo, PaymentRepository paymentRepo) 
        {
            Console.WriteLine("Enter Student ID to record payment:");
            int studentId = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter payment amount:");
            decimal amount = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Enter payment date (yyyy-mm-dd):");
            DateTime paymentDate = DateTime.Parse(Console.ReadLine());

            
            Student student = studentRepo.GetStudentByID(studentId);

            if (student != null)
            {
                
                paymentRepo.RecordPayment(studentId, amount, paymentDate); 
                Console.WriteLine($"Payment of {amount} recorded for {student.FirstName} on {paymentDate.ToShortDateString()}.");
            }
            else
            {
                Console.WriteLine("Student not found.");
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
    }


}
