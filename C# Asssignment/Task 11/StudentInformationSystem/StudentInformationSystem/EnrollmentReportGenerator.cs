using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem
{
    public class EnrollmentReportGenerator : IDisposable
    {
        private string connectionString = "Data Source=LAPTOP-2H2P4CPV;Initial Catalog=SISDB;Integrated Security=True;Encrypt=False";
        private SqlConnection connection;

        public EnrollmentReportGenerator()
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        public List<string> GenerateEnrollmentReport(string courseName)
        {
            var enrollmentList = new List<string>();

            // SQL query to retrieve students enrolled in the specified course
            string query = @"
                SELECT s.first_name, s.last_name 
                FROM Students s
                JOIN Enrollments e ON s.student_id = e.student_id
                JOIN Courses c ON e.course_id = c.course_id
                WHERE c.course_name = @courseName";

            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@courseName", courseName);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string studentName = $"{reader["first_name"]} {reader["last_name"]}";
                        enrollmentList.Add(studentName);
                    }
                }
            }

            return enrollmentList;
        }

        // Method to close and dispose the connection when the object is disposed
        public void Dispose()
        {
            if (connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
                connection.Dispose();
            }
        }
    }
}

