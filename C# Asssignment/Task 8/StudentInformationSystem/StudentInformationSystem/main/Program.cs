using System;

namespace StudentInformationSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create an instance of the service implementation
            ServiceImplementation service = new ServiceImplementation();

            // Call the method to enroll John Doe
            service.EnrollJohnDoe();

            // Wait for user input to close the console
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
