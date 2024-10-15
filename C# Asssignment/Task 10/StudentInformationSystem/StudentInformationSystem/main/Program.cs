using System;


using StudentInformationSystem.Entity; // Assuming Student class is here

namespace StudentInformationSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var service = new ServiceImplemetation())
            {
                // Present the user with options
                Console.WriteLine("Welcome to the Student Information System");
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Add Payment");
                Console.WriteLine("2. Retrieve Payments");
                Console.WriteLine("3. Update Outstanding Balance");

                // Read the user's choice
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddPayment(service);
                        break;
                    case "2":
                        RetrievePayments(service);
                        break;
                    case "3":
                        UpdateStudentBalance(service);
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }

            // Keep console open until a key is pressed
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        // Method for adding a payment
        static void AddPayment(ServiceImplemetation service)
        {
            Console.WriteLine("Enter Student ID:");
            int studentId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter Payment Amount:");
            decimal amount = Convert.ToDecimal(Console.ReadLine());

            Console.WriteLine("Enter Payment Date (yyyy-mm-dd):");
            DateTime paymentDate = DateTime.Parse(Console.ReadLine());

            service.AddPayment(studentId, amount, paymentDate);
            Console.WriteLine("Payment added successfully!");
        }

        // Method for retrieving payments
        static void RetrievePayments(ServiceImplemetation service)
        {
            Console.WriteLine("Enter Student ID:");
            int studentId = Convert.ToInt32(Console.ReadLine());

            service.RetrievePayments(studentId);
        }

        // Method for updating student outstanding balance
        static void UpdateStudentBalance(ServiceImplemetation service)
        {
            Console.WriteLine("Enter Student ID:");
            int studentId = Convert.ToInt32(Console.ReadLine());

            Student student = service.GetStudentById(studentId);
            if (student == null)
            {
                Console.WriteLine("Student not found.");
                return;
            }

            Console.WriteLine("Current Outstanding Balance: " + student.OutstandingBalance);

            Console.WriteLine("Enter New Outstanding Balance:");
            decimal newBalance = Convert.ToDecimal(Console.ReadLine());

            student.OutstandingBalance = newBalance;
            service.UpdateStudentBalance(student);
            Console.WriteLine("Outstanding balance updated successfully!");
        }
    }
}
