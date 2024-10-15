using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using StudentInformationSystem.util;
using StudentInformationSystem;
using StudentInformationSystem.Entity;

namespace StudentInformationSystem
{
    public class ServiceImplemetation : IDisposable
    {
        private SqlConnection connection;

        public ServiceImplemetation()
        {
            // Use the Dbconnection class from util to get the connection
            connection = Dbconnection.GetDBConnection();
            connection.Open();
        }

        public Student GetStudentById(int studentId)
        {
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
                            OutstandingBalance = reader["outstanding_balance"] != DBNull.Value
                                ? Convert.ToDecimal(reader["outstanding_balance"])
                                : 0
                        };
                    }
                }
            }
            return null;
        }

        public void AddPayment(int studentId, decimal amount, DateTime date)
        {
            string getMaxIdQuery = "SELECT ISNULL(MAX(payment_id), 0) + 1 FROM Payments";
            int newPaymentId;
            using (var getMaxIdCommand = new SqlCommand(getMaxIdQuery, connection))
            {
                newPaymentId = (int)getMaxIdCommand.ExecuteScalar();
            }

            string insertQuery = "INSERT INTO Payments (payment_id, student_id, amount, payment_date) VALUES (@paymentId, @studentId, @amount, @paymentDate)";
            using (var command = new SqlCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@paymentId", newPaymentId);
                command.Parameters.AddWithValue("@studentId", studentId);
                command.Parameters.AddWithValue("@amount", amount);
                command.Parameters.AddWithValue("@paymentDate", date);
                command.ExecuteNonQuery();
            }
        }

        public void UpdateStudentBalance(Student student)
        {
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

        // Dispose method to clean up resources
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
