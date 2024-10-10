using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            string courseName = "Computer Science 101";
            List<string> enrollmentReport;

            using (var reportGenerator = new EnrollmentReportGenerator())
            {
                enrollmentReport = reportGenerator.GenerateEnrollmentReport(courseName);
            }

            // Display the report
            Console.WriteLine($"Enrollment Report for {courseName}:");
            if (enrollmentReport.Count > 0)
            {
                foreach (var student in enrollmentReport)
                {
                    Console.WriteLine(student);
                }
            }
            else
            {
                Console.WriteLine("No students are enrolled in this course.");
               
            }
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();  // Waits for user input before closing the console
        }
    }
}

