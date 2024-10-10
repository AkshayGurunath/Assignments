using System;
using System.Data.SqlClient;

namespace StudentInformationSystem
{
    //Task 9
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("1. Assign Teacher to Course");
                Console.WriteLine("2. Retrieve Courses");
                Console.WriteLine("3. Exit");
                Console.Write("Enter your choice: ");
                int choice;

                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            AssignTeacherToCourse();
                            break;
                        case 2:
                            RetrieveCourses();
                            break;
                        case 3:
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Invalid choice, please try again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }
        }

        public static void AssignTeacherToCourse()
        {
            // Define Sarah Smith's information
            string teacherFirstName = "Sarah";
            string teacherLastName = "Smith";
            string teacherEmail = "sarah.smith@example.com";

            // Define course details
            string courseCode = "CS302";
            string courseName = "Advanced Database Management";
            int courseCredits = 4;

            string connectionString = "Data Source=LAPTOP-2H2P4CPV;Initial Catalog=SISDB;Integrated Security=True;Encrypt=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Step 1: Check if the course exists
                string findCourseQuery = "SELECT course_id FROM Courses WHERE course_code = @CourseCode";
                using (SqlCommand findCourseCommand = new SqlCommand(findCourseQuery, connection))
                {
                    findCourseCommand.Parameters.AddWithValue("@CourseCode", courseCode);
                    object courseIdObj = findCourseCommand.ExecuteScalar();

                    if (courseIdObj != null)
                    {
                        // Course exists, get the course_id
                        int courseId = (int)courseIdObj;
                        AssignTeacherToExistingCourse(connection, teacherEmail, teacherFirstName, teacherLastName, courseId);
                    }
                    else
                    {
                        // Course does not exist, add a new course
                        Console.WriteLine("Course with code {0} not found. Adding a new course.", courseCode);
                        AddNewCourseWithTeacher(connection, teacherEmail, teacherFirstName, teacherLastName, courseCode, courseName, courseCredits);
                    }
                }
            }
        }

        private static void AssignTeacherToExistingCourse(SqlConnection connection, string teacherEmail, string teacherFirstName, string teacherLastName, int courseId)
        {
            // Step 2: Check if the teacher exists
            string findTeacherQuery = "SELECT teacher_id FROM Teacher WHERE email = @TeacherEmail";
            using (SqlCommand findTeacherCommand = new SqlCommand(findTeacherQuery, connection))
            {
                findTeacherCommand.Parameters.AddWithValue("@TeacherEmail", teacherEmail);
                object teacherIdObj = findTeacherCommand.ExecuteScalar();
                int teacherId;

                if (teacherIdObj == null)
                {
                    // Step 3: Get the next available teacher_id manually
                    teacherId = GetNextTeacherId(connection);

                    // Insert Sarah Smith into the Teacher table
                    InsertNewTeacher(connection, teacherId, teacherFirstName, teacherLastName, teacherEmail);
                }
                else
                {
                    // Teacher exists, get the teacher_id
                    teacherId = (int)teacherIdObj;
                }

                // Step 4: Update the course with the teacher_id
                UpdateCourseWithTeacherId(connection, courseId, teacherId);
            }
        }

        private static int GetNextTeacherId(SqlConnection connection)
        {
            string getMaxTeacherIdQuery = "SELECT ISNULL(MAX(teacher_id), 0) FROM Teacher";
            using (SqlCommand getMaxTeacherIdCommand = new SqlCommand(getMaxTeacherIdQuery, connection))
            {
                return (int)getMaxTeacherIdCommand.ExecuteScalar() + 1; // Increment for new teacher_id
            }
        }

        private static void InsertNewTeacher(SqlConnection connection, int teacherId, string firstName, string lastName, string email)
        {
            string insertTeacherQuery = "INSERT INTO Teacher (teacher_id, first_name, last_name, email) VALUES (@TeacherId, @FirstName, @LastName, @Email)";
            using (SqlCommand insertTeacherCommand = new SqlCommand(insertTeacherQuery, connection))
            {
                insertTeacherCommand.Parameters.AddWithValue("@TeacherId", teacherId);
                insertTeacherCommand.Parameters.AddWithValue("@FirstName", firstName);
                insertTeacherCommand.Parameters.AddWithValue("@LastName", lastName);
                insertTeacherCommand.Parameters.AddWithValue("@Email", email);
                insertTeacherCommand.ExecuteNonQuery();
            }
        }

        private static void UpdateCourseWithTeacherId(SqlConnection connection, int courseId, int teacherId)
        {
            string updateCourseQuery = "UPDATE Courses SET teacher_id = @TeacherId WHERE course_id = @CourseId";
            using (SqlCommand updateCourseCommand = new SqlCommand(updateCourseQuery, connection))
            {
                updateCourseCommand.Parameters.AddWithValue("@TeacherId", teacherId);
                updateCourseCommand.Parameters.AddWithValue("@CourseId", courseId);
                int rowsAffected = updateCourseCommand.ExecuteNonQuery();

                Console.WriteLine(rowsAffected > 0 ? "Successfully assigned teacher to the course." : "Failed to assign teacher to the course.");
            }
        }

        private static void AddNewCourseWithTeacher(SqlConnection connection, string teacherEmail, string teacherFirstName, string teacherLastName, string courseCode, string courseName, int courseCredits)
        {
            int teacherId = GetNextTeacherId(connection);
            InsertNewTeacher(connection, teacherId, teacherFirstName, teacherLastName, teacherEmail);

            // Step 7: Insert the new course with the assigned teacher
            string insertCourseQuery = "INSERT INTO Courses (course_name, course_code, credits, teacher_id) VALUES (@CourseName, @CourseCode, @Credits, @TeacherId)";
            using (SqlCommand insertCourseCommand = new SqlCommand(insertCourseQuery, connection))
            {
                insertCourseCommand.Parameters.AddWithValue("@CourseName", courseName);
                insertCourseCommand.Parameters.AddWithValue("@CourseCode", courseCode);
                insertCourseCommand.Parameters.AddWithValue("@Credits", courseCredits);
                insertCourseCommand.Parameters.AddWithValue("@TeacherId", teacherId);

                int rowsAffected = insertCourseCommand.ExecuteNonQuery();

                Console.WriteLine(rowsAffected > 0 ? $"New course '{courseName}' added with Teacher {teacherFirstName} {teacherLastName} assigned." : "Failed to add the new course.");
            }
        }

        public static void RetrieveCourses()
        {
            string connectionString = "Data Source=LAPTOP-2H2P4CPV;Initial Catalog=SISDB;Integrated Security=True;Encrypt=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string retrieveCoursesQuery = "SELECT course_id, course_name, course_code, credits, teacher_id FROM Courses";

                using (SqlCommand retrieveCoursesCommand = new SqlCommand(retrieveCoursesQuery, connection))
                {
                    using (SqlDataReader reader = retrieveCoursesCommand.ExecuteReader())
                    {
                        Console.WriteLine("Courses:");

                        // Display column headers
                        Console.WriteLine("{0,-10} {1,-30} {2,-15} {3,-10} {4,-10}", "Course ID", "Course Name", "Course Code", "Credits", "Teacher ID");

                        // Read and display each course
                        while (reader.Read())
                        {
                            int courseId = reader.GetInt32(0);
                            string courseName = reader.GetString(1);
                            string courseCode = reader.IsDBNull(2) ? null : reader.GetString(2);
                            int credits = reader.GetInt32(3);
                            int teacherId = reader.IsDBNull(4) ? 0 : reader.GetInt32(4); // Handle possible null values

                            Console.WriteLine("{0,-10} {1,-30} {2,-15} {3,-10} {4,-10}", courseId, courseName, courseCode, credits, teacherId);
                        }
                    }
                }
            }
        }
    }
}
