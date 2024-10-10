using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem
{
    public class DatabaseOperations : IDisposable
    {
        private string connectionString = "Data Source=LAPTOP-2H2P4CPV;Initial Catalog=SISDB;Integrated Security=True;Encrypt=False";
        private SqlConnection connection;

        public DatabaseOperations()
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        public Student GetStudentById(int studentId)
        {
            // SQL query to get the student details
            string query = "SELECT * FROM Students WHERE student_id = @studentId";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@studentId", studentId);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Student
                        {
                            StudentId = Convert.ToInt32(reader["student_id"]),
                            FirstName = reader["first_name"].ToString(),
                            LastName = reader["last_name"].ToString(),

                            // Ensure outstanding_balance is handled correctly, checking for NULL
                            OutstandingBalance = reader["outstanding_balance"] != DBNull.Value
                                ? Convert.ToDecimal(reader["outstanding_balance"])
                                : 0 // Default value if NULL
                        };
                    }
                }
            }
            return null;
        }

        public void AddPayment(int studentId, decimal amount, DateTime date)
        {
            // Get the next payment_id by retrieving the current max payment_id from the Payments table
            string getMaxIdQuery = "SELECT ISNULL(MAX(payment_id), 0) + 1 FROM Payments";
            int newPaymentId;

            using (var getMaxIdCommand = new SqlCommand(getMaxIdQuery, connection))
            {
                // Get the next available payment_id
                newPaymentId = (int)getMaxIdCommand.ExecuteScalar();
            }

            // SQL query to insert the payment record
            string insertQuery = "INSERT INTO Payments (payment_id, student_id, amount, payment_date) VALUES (@paymentId, @studentId, @amount, @paymentDate)";
            using (var command = new SqlCommand(insertQuery, connection))
            {
                // Add parameters for the SQL insert query
                command.Parameters.AddWithValue("@paymentId", newPaymentId);  // Insert the manually generated payment_id
                command.Parameters.AddWithValue("@studentId", studentId);
                command.Parameters.AddWithValue("@amount", amount);
                command.Parameters.AddWithValue("@paymentDate", date);

                // Execute the query to insert the payment record
                command.ExecuteNonQuery();
            }
        }
        public void UpdateStudentBalance(Student student)
        {
            // SQL query to update the student's outstanding balance
            string query = "UPDATE Students SET outstanding_balance = @balance WHERE student_id = @studentId";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@balance", student.OutstandingBalance);
                command.Parameters.AddWithValue("@studentId", student.StudentId);
                command.ExecuteNonQuery();
            }
        }

        public void RetrievePayments(int studentId)
        {
            // SQL query to retrieve payment records
            string query = "SELECT * FROM Payments WHERE student_id = @studentId";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@studentId", studentId);
                using (var reader = command.ExecuteReader())
                {
                    Console.WriteLine("Payments for Student ID: " + studentId);
                    while (reader.Read())
                    {
                        Console.WriteLine("Payment ID: " + reader["payment_id"] + ", Amount: " + reader["amount"] + ", Date: " + reader["payment_date"]);
                    }
                }
            }
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



