using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using StudentInformationSystem.util; // Make sure to include the util namespace

namespace StudentInformationSystem
{
    public class ServiceImplementation
    {
        public void EnrollJohnDoe()
        {
            string firstName = "John";
            string lastName = "Doe";
            DateTime dateOfBirth = new DateTime(1995, 8, 15);
            string email = "John.doe@gmail.com";
            string phoneNumber = "1234567890";

            // Check if the student already exists
            if (StudentExists(email))
            {
                Console.WriteLine("Student with this email already exists.");
                return; // Skip the insertion
            }

            int studentId = GetNextStudentId();

            try
            {
                // Insert student record
                using (SqlConnection connection = DbConnection.GetDBConnection())
                {
                    connection.Open();
                    string insertStudentQuery = "INSERT INTO Students (student_id, first_name, last_name, date_of_birth, email, phone_number) VALUES (@studentId, @firstName, @lastName, @dateOfBirth, @Email, @PhoneNumber)";
                    using (SqlCommand command = new SqlCommand(insertStudentQuery, connection))
                    {
                        command.Parameters.AddWithValue("@studentId", studentId);
                        command.Parameters.AddWithValue("@firstName", firstName);
                        command.Parameters.AddWithValue("@lastName", lastName);
                        command.Parameters.AddWithValue("@dateOfBirth", dateOfBirth);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);

                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine($"{rowsAffected} student record inserted.");
                    }
                }

                // Retrieve course IDs
                int[] courseIds = GetCourseIds(new string[] { "Data Structures", "Operating Systems" });
                if (courseIds.Length == 0)
                {
                    Console.WriteLine("No courses found for enrollment.");
                    return;
                }

                // Enroll in courses
                foreach (var courseId in courseIds)
                {
                    EnrollStudentInCourse(studentId, courseId);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting student: {ex.Message}");
            }
        }

        private bool StudentExists(string email)
        {
            using (SqlConnection connection = DbConnection.GetDBConnection())
            {
                connection.Open();
                string checkStudentQuery = "SELECT COUNT(*) FROM Students WHERE email = @Email";
                using (SqlCommand command = new SqlCommand(checkStudentQuery, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    int count = (int)command.ExecuteScalar();
                    return count > 0; // Return true if the student exists
                }
            }
        }

        private int GetNextStudentId()
        {
            int maxId = 0;
            using (SqlConnection connection = DbConnection.GetDBConnection())
            {
                connection.Open();
                string getMaxIdQuery = "SELECT ISNULL(MAX(student_id), 0) FROM Students";
                using (SqlCommand command = new SqlCommand(getMaxIdQuery, connection))
                {
                    maxId = (int)command.ExecuteScalar();
                }
            }
            return maxId + 1; // Incrementing to get the next available ID
        }

        private int[] GetCourseIds(string[] courseNames)
        {
            var courseIds = new List<int>();
            using (SqlConnection connection = DbConnection.GetDBConnection())
            {
                connection.Open();
                foreach (var courseName in courseNames)
                {
                    string getCourseIdQuery = "SELECT course_id FROM Courses WHERE course_name = @CourseName";
                    using (SqlCommand command = new SqlCommand(getCourseIdQuery, connection))
                    {
                        command.Parameters.AddWithValue("@CourseName", courseName);
                        var result = command.ExecuteScalar();
                        if (result != null)
                        {
                            courseIds.Add((int)result); // Add the course ID to the list
                        }
                    }
                }
            }
            return courseIds.ToArray(); // Return an array of course IDs
        }

        private void EnrollStudentInCourse(int studentId, int courseId)
        {
            try
            {
                using (SqlConnection connection = DbConnection.GetDBConnection())
                {
                    connection.Open();
                    string insertEnrollmentQuery = "INSERT INTO Enrollments (student_id, course_id, enrollment_date) VALUES (@studentId, @courseId, @enrollmentDate)";
                    using (SqlCommand command = new SqlCommand(insertEnrollmentQuery, connection))
                    {
                        command.Parameters.AddWithValue("@studentId", studentId);
                        command.Parameters.AddWithValue("@courseId", courseId);
                        command.Parameters.AddWithValue("@enrollmentDate", DateTime.Now);

                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine($"Enrolled in course {courseId}. {rowsAffected} enrollment record inserted.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error enrolling in course {courseId}: {ex.Message}");
            }
        }
    }
}
