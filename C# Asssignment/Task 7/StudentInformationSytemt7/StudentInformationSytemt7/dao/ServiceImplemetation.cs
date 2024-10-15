using StudentInformationSytemt7.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSytemt7.dao
{
    public static class DatabaseService
    {
        public static void InitializeDatabase()
        {
            try
            {
                using (SqlConnection connection = DatabaseConnectivity.GetDBConnection())
                {
                    connection.Open();
                    string createTables = @"
                        CREATE TABLE IF NOT EXISTS Students (
                            student_id INT PRIMARY KEY IDENTITY,
                            first_name NVARCHAR(50),
                            last_name NVARCHAR(50),
                            date_of_birth DATE,
                            email NVARCHAR(100),
                            phone_number NVARCHAR(15)
                        );
                        CREATE TABLE IF NOT EXISTS Courses (
                            course_id INT PRIMARY KEY IDENTITY,
                            course_name NVARCHAR(100),
                            credits INT,
                            teacher_id INT
                        );
                        CREATE TABLE IF NOT EXISTS Enrollments (
                            enrollment_id INT PRIMARY KEY IDENTITY,
                            student_id INT,
                            course_id INT,
                            enrollment_date DATE,
                            FOREIGN KEY(student_id) REFERENCES Students(student_id),
                            FOREIGN KEY(course_id) REFERENCES Courses(course_id)
                        );
                        CREATE TABLE IF NOT EXISTS Payments (
                            payment_id INT PRIMARY KEY IDENTITY,
                            student_id INT,
                            amount DECIMAL(10,2),
                            payment_date DATE,
                            FOREIGN KEY(student_id) REFERENCES Students(student_id)
                        );
                        CREATE TABLE IF NOT EXISTS Teachers (
                            teacher_id INT PRIMARY KEY IDENTITY,
                            first_name NVARCHAR(50),
                            last_name NVARCHAR(50),
                            email NVARCHAR(100)
                        );";

                    SqlCommand cmd = new SqlCommand(createTables, connection);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Database initialized successfully!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during database initialization: {ex.Message}");
            }
        }

        public static void RetrieveStudents()
        {
            using (SqlConnection connection = DatabaseConnectivity.GetDBConnection())
            {
                connection.Open();
                string sql = "SELECT * FROM Students";
                SqlCommand cmd = new SqlCommand(sql, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"Student: {reader["first_name"]} {reader["last_name"]}, Email: {reader["email"]}");
                }
            }
        }

        public static void RetrieveCourses()
        {
            using (SqlConnection connection = DatabaseConnectivity.GetDBConnection())
            {
                connection.Open();
                string sql = "SELECT * FROM Courses";
                SqlCommand cmd = new SqlCommand(sql, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"Course: {reader["course_name"]}, Credits: {reader["credits"]}");
                }
            }
        }

        public static void RetrieveEnrollments()
        {
            using (SqlConnection connection = DatabaseConnectivity.GetDBConnection())
            {
                connection.Open();
                string sql = "SELECT * FROM Enrollments";
                SqlCommand cmd = new SqlCommand(sql, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"Enrollment ID: {reader["enrollment_id"]}, Student ID: {reader["student_id"]}, Course ID: {reader["course_id"]}, Enrollment Date: {reader["enrollment_date"]}");
                }
            }
        }

        public static void RetrievePayments()
        {
            using (SqlConnection connection = DatabaseConnectivity.GetDBConnection())
            {
                connection.Open();
                string sql = "SELECT * FROM Payments";
                SqlCommand cmd = new SqlCommand(sql, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"Payment ID: {reader["payment_id"]}, Student ID: {reader["student_id"]}, Amount: {reader["amount"]}, Payment Date: {reader["payment_date"]}");
                }
            }
        }

        public static void RetrieveTeachers()
        {
            using (SqlConnection connection = DatabaseConnectivity.GetDBConnection())
            {
                connection.Open();
                string sql = "SELECT * FROM Teachers"; // Fixed the table name from Teacher to Teachers
                SqlCommand cmd = new SqlCommand(sql, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"Teacher: {reader["first_name"]} {reader["last_name"]}, Email: {reader["email"]}");
                }
            }
        }

        public static void UpdateStudentInfo()
        {
            try
            {
                using (SqlConnection connection = DatabaseConnectivity.GetDBConnection())
                {
                    connection.Open();
                    Console.Write("Enter Student ID to update: ");
                    int studentId = int.Parse(Console.ReadLine());

                    Console.Write("Enter new first name: ");
                    string firstName = Console.ReadLine();

                    Console.Write("Enter new last name: ");
                    string lastName = Console.ReadLine();

                    Console.Write("Enter new email: ");
                    string email = Console.ReadLine();

                    Console.Write("Enter new phone number: ");
                    string phoneNumber = Console.ReadLine();

                    string sql = "UPDATE Students SET first_name = @FirstName, last_name = @LastName, email = @Email, phone_number = @PhoneNumber WHERE student_id = @StudentId";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@FirstName", firstName);
                        cmd.Parameters.AddWithValue("@LastName", lastName);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                        cmd.Parameters.AddWithValue("@StudentId", studentId);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Student info updated successfully.");
                        }
                        else
                        {
                            Console.WriteLine("No student found with the given ID.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating student info: {ex.Message}");
            }
        }

        public static bool CourseExists(int courseId)
        {
            using (SqlConnection connection = DatabaseConnectivity.GetDBConnection())
            {
                connection.Open();
                string sql = "SELECT COUNT(*) FROM Courses WHERE course_id = @CourseId";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@CourseId", courseId);
                    return (int)cmd.ExecuteScalar() > 0;
                }
            }

        }
        // Insert Enrollment
        public static bool InsertEnrollment(int studentId, int courseId)
        {
            try
            {
                using (SqlConnection connection = DatabaseConnectivity.GetDBConnection())
                {
                    connection.Open();

                    if (!StudentExists(studentId) || !CourseExists(courseId))
                    {
                        return false; // Student or Course does not exist
                    }

                    string sql = "INSERT INTO Enrollments (student_id, course_id, enrollment_date) VALUES (@StudentId, @CourseId, @EnrollmentDate)";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@StudentId", studentId);
                        cmd.Parameters.AddWithValue("@CourseId", courseId);
                        cmd.Parameters.AddWithValue("@EnrollmentDate", DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true; // Enrollment added successfully
            }
            catch (Exception)
            {
                return false; // Error occurred
            }
        }

        // Insert Payment
        public static bool InsertPayment(int studentId, decimal amount)
        {
            try
            {
                using (SqlConnection connection = DatabaseConnectivity.GetDBConnection())
                {
                    connection.Open();
                    string sql = "INSERT INTO Payments (student_id, amount, payment_date) VALUES (@studentId, @amount, @paymentDate)";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@studentId", studentId);
                        cmd.Parameters.AddWithValue("@amount", amount);
                        cmd.Parameters.AddWithValue("@paymentDate", DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true; // Payment added successfully
            }
            catch (Exception)
            {
                return false; // Error occurred
            }
        }

        // Check if Student Exists
        private static bool StudentExists(int studentId)
        {
            using (SqlConnection connection = DatabaseConnectivity.GetDBConnection())
            {
                connection.Open();
                string sql = "SELECT COUNT(*) FROM Students WHERE student_id = @StudentId";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@StudentId", studentId);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }
        // Assign Teacher Transaction
        public static bool AssignTeacherTransaction(int teacherId, int courseId)
        {
            using(SqlConnection connection = DatabaseConnectivity.GetDBConnection())
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    // Check if Teacher exists
                    if (!TeacherExists(teacherId, connection, transaction))
                    {
                        return false; // Teacher does not exist
                    }

                    // Assign Teacher to Course
                    string assignSql = "UPDATE Courses SET teacher_id = @TeacherId WHERE course_id = @CourseId";
                    using (SqlCommand assignCmd = new SqlCommand(assignSql, connection, transaction))
                    {
                        assignCmd.Parameters.AddWithValue("@TeacherId", teacherId);
                        assignCmd.Parameters.AddWithValue("@CourseId", courseId);
                        int rowsAffected = assignCmd.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            throw new Exception("No course was updated, check if the course ID is valid.");
                        }
                    }

                    transaction.Commit();
                    return true; // Teacher assigned successfully
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false; // Transaction failed
                }
            }
        }

        // Record Payment Transaction
        public static bool RecordPaymentTransaction(int studentId, decimal amount, DateTime paymentDate)
        {
            using(SqlConnection connection = DatabaseConnectivity.GetDBConnection())
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    // Record Payment
                    string paymentSql = "INSERT INTO Payments (student_id, amount, payment_date) VALUES (@StudentId, @Amount, @PaymentDate)";
                    using (SqlCommand paymentCmd = new SqlCommand(paymentSql, connection, transaction))
                    {
                        paymentCmd.Parameters.AddWithValue("@StudentId", studentId);
                        paymentCmd.Parameters.AddWithValue("@Amount", amount);
                        paymentCmd.Parameters.AddWithValue("@PaymentDate", paymentDate);
                        paymentCmd.ExecuteNonQuery();
                    }

                    // Update Student balance (Assuming there's a balance column in your Students table)
                    string updateBalanceSql = "UPDATE Students SET balance = balance - @Amount WHERE student_id = @StudentId";
                    using (SqlCommand updateBalanceCmd = new SqlCommand(updateBalanceSql, connection, transaction))
                    {
                        updateBalanceCmd.Parameters.AddWithValue("@Amount", amount);
                        updateBalanceCmd.Parameters.AddWithValue("@StudentId", studentId);
                        updateBalanceCmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    return true; // Payment recorded successfully
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false; // Transaction failed
                }
            }
        }

        // Check if Teacher Exists
        public static bool TeacherExists(int teacherId, SqlConnection connection, SqlTransaction transaction)
        {
            string sql = "SELECT COUNT(*) FROM Teachers WHERE teacher_id = @TeacherId";
            using (SqlCommand cmd = new SqlCommand(sql, connection, transaction))
            {
                cmd.Parameters.AddWithValue("@TeacherId", teacherId);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        // Dynamic Query Method
        public static DataTable ExecuteDynamicQuery(string query)
        {
            using (SqlConnection connection = DatabaseConnectivity.GetDBConnection())
            {
                DataTable dataTable = new DataTable();
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows) // Check if there are any rows to read
                            {
                                dataTable.Load(reader);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    // Handle error internally, return empty DataTable
                }
                return dataTable; // Return results or empty DataTable
            }
        }
    }
}


       
