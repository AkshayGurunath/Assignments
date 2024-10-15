using System;
using System.Data;
using StudentInformationSytemt7.dao;

namespace StudentInformationSytemt7
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Retrieve Data");
                Console.WriteLine("2. Insert or Update Records");
                Console.WriteLine("3. Handle Transactions");
                Console.WriteLine("4. Execute Dynamic Query");
                Console.WriteLine("5. Exit");
                Console.Write("Your choice: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        RetrieveData();
                        break;
                    case "2":
                        InsertOrUpdateRecords();
                        break;
                    case "3":
                        HandleTransactions();
                        break;
                    case "4":
                        ExecuteDynamicQuery();
                        break;
                    case "5":
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            }
        }

        static void RetrieveData()
        {
            Console.WriteLine("Retrieving Data...");
            Console.WriteLine("1. Retrieve Students");
            Console.WriteLine("2. Retrieve Courses");
            Console.WriteLine("3. Retrieve Enrollments");
            Console.WriteLine("4. Retrieve Payments");
            Console.WriteLine("5. Retrieve Teachers");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    DatabaseService.RetrieveStudents();
                    break;
                case "2":
                    DatabaseService.RetrieveCourses();
                    break;
                case "3":
                    DatabaseService.RetrieveEnrollments();
                    break;
                case "4":
                    DatabaseService.RetrievePayments();
                    break;
                case "5":
                    DatabaseService.RetrieveTeachers();
                    break;
                default:
                    Console.WriteLine("Invalid choice, please try again.");
                    break;
            }
        }

        static void InsertOrUpdateRecords()
        {
            Console.WriteLine("1. Update Student Info");
            Console.WriteLine("2. Insert Enrollment");
            Console.WriteLine("3. Insert Payment");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    DatabaseService.UpdateStudentInfo();
                    break;
                case "2":
                    InsertEnrollment();
                    break;
                case "3":
                    InsertPayment();
                    break;
                default:
                    Console.WriteLine("Invalid choice, please try again.");
                    break;
            }
        }

        static void HandleTransactions()
        {
            Console.WriteLine("1. Assign Teacher to Course");
            Console.WriteLine("2. Record Payment Transaction");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    AssignTeacherToCourse();
                    break;
                case "2":
                    RecordPaymentTransaction();
                    break;
                default:
                    Console.WriteLine("Invalid choice, please try again.");
                    break;
            }
        }

        static void ExecuteDynamicQuery()
        {
            Console.Write("Enter your SQL query: ");
            string query = Console.ReadLine();
            var result = DatabaseService.ExecuteDynamicQuery(query);

            // Display results
            if (result.Rows.Count > 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    foreach (var item in row.ItemArray)
                    {
                        Console.Write(item + "\t");
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("No results found.");
            }
        }

        static void InsertEnrollment()
        {
            Console.Write("Enter Student ID: ");
            int studentId = int.Parse(Console.ReadLine());

            Console.Write("Enter Course ID: ");
            int courseId = int.Parse(Console.ReadLine());

            bool success = DatabaseService.InsertEnrollment(studentId, courseId);
            Console.WriteLine(success ? "Enrollment added successfully." : "Failed to add enrollment.");
        }

        static void InsertPayment()
        {
            Console.Write("Enter Student ID: ");
            int studentId = int.Parse(Console.ReadLine());

            Console.Write("Enter Payment Amount: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            bool success = DatabaseService.InsertPayment(studentId, amount);
            Console.WriteLine(success ? "Payment added successfully." : "Failed to add payment.");
        }

        static void AssignTeacherToCourse()
        {
            Console.Write("Enter Teacher ID: ");
            int teacherId = int.Parse(Console.ReadLine());

            Console.Write("Enter Course ID: ");
            int courseId = int.Parse(Console.ReadLine());

            bool success = DatabaseService.AssignTeacherTransaction(teacherId, courseId);
            Console.WriteLine(success ? "Teacher assigned successfully." : "Failed to assign teacher.");
        }

        static void RecordPaymentTransaction()
        {
            Console.Write("Enter Student ID: ");
            int studentId = int.Parse(Console.ReadLine());

            Console.Write("Enter Payment Amount: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            bool success = DatabaseService.RecordPaymentTransaction(studentId, amount, DateTime.Now);
            Console.WriteLine(success ? "Payment recorded successfully." : "Failed to record payment.");
        }
    }
}
