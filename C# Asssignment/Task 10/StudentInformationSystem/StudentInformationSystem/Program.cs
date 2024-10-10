using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DatabaseOperations dbOperations = new DatabaseOperations();
            bool running = true;

            while (running)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Record Payment");
                Console.WriteLine("2. Retrieve Payments");
                Console.WriteLine("3. Exit");

                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        // Record Payment for Jane Johnson
                        RecordPayment(dbOperations, 101, 500.00m, new DateTime(2023, 4, 10));
                        break;

                    case 2:
                        // Retrieve payments
                        dbOperations.RetrievePayments(101);
                        break;

                    case 3:
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please select again.");
                        break;
                }
            }
        }

        private static void RecordPayment(DatabaseOperations dbOperations, int studentId, decimal paymentAmount, DateTime paymentDate)
        {
            // Retrieve the student's record
            var student = dbOperations.GetStudentById(studentId);
            if (student == null)
            {
                Console.WriteLine("Student not found.");
                return;
            }

            // Record the payment
            dbOperations.AddPayment(studentId, paymentAmount, paymentDate);

            // Update the student's outstanding balance
            student.OutstandingBalance -= paymentAmount; // Update balance
            dbOperations.UpdateStudentBalance(student);

            Console.WriteLine("Payment recorded successfully for " + student.FirstName + " " + student.LastName);
        }
    }

    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal OutstandingBalance { get; set; }
    }
} 
    

