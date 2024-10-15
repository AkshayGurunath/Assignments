using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using StudentInformationSystem.Entity;
using StudentInformationSystem.util;

namespace StudentInformationSystem.service
{
    public class CourseService
    {
        public static void AssignTeacherToCourse(string teacherFirstName, string teacherLastName, string teacherEmail, string courseCode, string courseName, int courseCredits)
        {
            using (SqlConnection connection = Dbconnection.GetDBConnection())
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

                    // Insert the new teacher
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
                return (int)getMaxTeacherIdCommand.ExecuteScalar() + 1;
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
                updateCourseCommand.ExecuteNonQuery();
            }
        }

        private static void AddNewCourseWithTeacher(SqlConnection connection, string teacherEmail, string teacherFirstName, string teacherLastName, string courseCode, string courseName, int courseCredits)
        {
            int teacherId = GetNextTeacherId(connection);
            InsertNewTeacher(connection, teacherId, teacherFirstName, teacherLastName, teacherEmail);

            string insertCourseQuery = "INSERT INTO Courses (course_name, course_code, credits, teacher_id) VALUES (@CourseName, @CourseCode, @Credits, @TeacherId)";
            using (SqlCommand insertCourseCommand = new SqlCommand(insertCourseQuery, connection))
            {
                insertCourseCommand.Parameters.AddWithValue("@CourseName", courseName);
                insertCourseCommand.Parameters.AddWithValue("@CourseCode", courseCode);
                insertCourseCommand.Parameters.AddWithValue("@Credits", courseCredits);
                insertCourseCommand.Parameters.AddWithValue("@TeacherId", teacherId);
                insertCourseCommand.ExecuteNonQuery();
            }
        }

        public static List<Course> RetrieveCourses()
        {
            List<Course> courses = new List<Course>();

            using (SqlConnection connection = Dbconnection.GetDBConnection())
            {
                connection.Open();
                string retrieveCoursesQuery = "SELECT course_id, course_name, course_code, credits, teacher_id FROM Courses";
                using (SqlCommand retrieveCoursesCommand = new SqlCommand(retrieveCoursesQuery, connection))
                {
                    using (SqlDataReader reader = retrieveCoursesCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Course course = new Course
                            {
                                CourseId = reader.GetInt32(0),
                                CourseName = reader.GetString(1),
                                CourseCode = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Credits = reader.GetInt32(3),
                                TeacherId = reader.IsDBNull(4) ? 0 : reader.GetInt32(4)
                            };
                            courses.Add(course);
                        }
                    }
                }
            }

            return courses;
        }
    }

    
}
