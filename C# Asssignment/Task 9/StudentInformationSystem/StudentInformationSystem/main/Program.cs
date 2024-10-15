using System;
using StudentInformationSystem.service;

namespace StudentInformationSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("1. Assign Teacher to Course");
                Console.WriteLine("2. Retrieve Courses");
                Console.WriteLine("3. Exit");
                Console.Write("Enter your choice: ");
                int choice;

                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            AssignTeacherToCourse();
                            break;
                        case 2:
                            RetrieveCourses();
                            break;
                        case 3:
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Invalid choice, please try again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }
        }

        public static void AssignTeacherToCourse()
        {
            // Service method call for Assigning Teacher to Course
            CourseService.AssignTeacherToCourse("Sarah", "Smith", "sarah.smith@example.com", "CS302", "Advanced Database Management", 4);
        }

        public static void RetrieveCourses()
        {
            // Service method call for Retrieving Courses
            var courses = CourseService.RetrieveCourses();
            Console.WriteLine("Courses:");
            Console.WriteLine("{0,-10} {1,-30} {2,-15} {3,-10} {4,-10}", "Course ID", "Course Name", "Course Code", "Credits", "Teacher ID");

            foreach (var course in courses)
            {
                Console.WriteLine("{0,-10} {1,-30} {2,-15} {3,-10} {4,-10}", course.CourseId, course.CourseName, course.CourseCode, course.Credits, course.TeacherId);
            }
        }
    }
}
