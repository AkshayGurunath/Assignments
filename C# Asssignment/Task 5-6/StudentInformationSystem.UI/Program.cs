using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.Entity;
using StudentInformationSystem.BusinessLayer.Repository;
using StudentInformationSystem.BusinessLayer.Services;
using StudentInformationSystem.BusinessLayer.Exceptions;
using StudentInformationSystem.BusinessLayer;


namespace StudentInformationSystem.UI
{
    class Program
    {
            static void Main(string[] args)
            {
                // Initialize repositories
                var studentRepo = new StudentRepository();
                var courseRepo = new CourseRepository();
                var teacherRepo = new TeacherRepository();
                var paymentRepo = new PaymentRepository();

                // Create an instance of the SIS
                var sis = new SIS(studentRepo, courseRepo, teacherRepo, paymentRepo);

                while (true)
                {
                    Console.WriteLine("Select an option:");
                    Console.WriteLine("1. Add Enrollment");
                    Console.WriteLine("2. Assign Course to Teacher");
                    Console.WriteLine("3. Add Payment");
                    Console.WriteLine("4. Get Enrollments for Student");
                    Console.WriteLine("5. Get Courses for Teacher");
                    Console.WriteLine("0. Exit");

                    string input = Console.ReadLine();
                    if (input == "0")
                    {
                        break; // Exit the loop and terminate the program
                    }

                    switch (input)
                    {
                        case "1": // Add Enrollment
                            Console.Write("Enter Student ID: ");
                            int studentId = int.Parse(Console.ReadLine());
                            Console.Write("Enter Course ID: ");
                            int courseId = int.Parse(Console.ReadLine());

                            var student = studentRepo.GetStudentById(studentId); // Implement this method
                            var course = courseRepo.GetCourseById(courseId); // Implement this method

                            if (student != null && course != null)
                            {
                                sis.AddEnrollment(student, course, DateTime.Now);
                                Console.WriteLine("Enrollment added successfully!");
                            }
                            else
                            {
                                Console.WriteLine("Invalid Student or Course ID.");
                            }
                            break;

                        case "2": // Assign Course to Teacher
                            Console.Write("Enter Teacher ID: ");
                            int teacherId = int.Parse(Console.ReadLine());
                            Console.Write("Enter Course ID: ");
                            int courseToAssignId = int.Parse(Console.ReadLine());

                            var teacher = teacherRepo.GetById(teacherId); // Implement this method
                            var courseToAssign = courseRepo.GetCourseById(courseToAssignId); // Implement this method

                            if (teacher != null && courseToAssign != null)
                            {
                                sis.AssignCourseToTeacher(courseToAssign, teacher);
                                Console.WriteLine("Course assigned to teacher successfully!");
                            }
                            else
                            {
                                Console.WriteLine("Invalid Teacher or Course ID.");
                            }
                            break;

                        case "3": // Add Payment
                            Console.Write("Enter Student ID: ");
                            int paymentStudentId = int.Parse(Console.ReadLine());
                            Console.Write("Enter Payment Amount: ");
                            decimal paymentAmount = decimal.Parse(Console.ReadLine());

                            var paymentDate = DateTime.Now;
                            sis.AddPayment(paymentStudentId, paymentAmount, paymentDate);
                            Console.WriteLine("Payment added successfully!");
                            break;

                        case "4": // Get Enrollments for Student
                            Console.Write("Enter Student ID: ");
                            int enrollmentsStudentId = int.Parse(Console.ReadLine());
                            var enrollments = sis.GetEnrollmentsForStudent(enrollmentsStudentId);

                            Console.WriteLine("Enrollments for Student:");
                            foreach (var enrollment in enrollments)
                            {
                                Console.WriteLine($"Course: {enrollment.Course.Name}, Enrollment Date: {enrollment.EnrollmentDate}");
                            }
                            break;

                        case "5": // Get Courses for Teacher
                            Console.Write("Enter Teacher ID: ");
                            int coursesTeacherId = int.Parse(Console.ReadLine());
                            var courses = sis.GetCoursesForTeacher(coursesTeacherId);

                            Console.WriteLine("Courses for Teacher:");
                            foreach (var courseItem in courses)
                            {
                                Console.WriteLine(courseItem.Name);
                            }
                            break;

                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }
            }
        }
    }










