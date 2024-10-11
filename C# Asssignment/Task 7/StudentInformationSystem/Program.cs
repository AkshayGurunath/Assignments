using System;
using System.Data.SqlClient;
class Program
{
    static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("Welcome to the Student Information System!");

            bool exitToMain = true;
            while (exitToMain)
            {
                Console.WriteLine("\nEnter Section: 1. Database Initialization, 2. Data Retrieval, 3. Data Insertion/Update, 4. Transaction Management, 5. Dynamic Query Builder, 6. Exit");
                string section = Console.ReadLine();

                switch (section)
                {
                     Database Initialization
                    case "1":
                        InitializeDatabase();
                        break;

                     Data Retrieval
                    case "2":
                        Console.WriteLine("Choose: 1. Retrieve Students, 2. Retrieve Courses, 3. Retrieve Enrollments, 4. Retrieve Payments, 5. Retrieve Teachers");
                        string retrieveOption = Console.ReadLine();
                        switch (retrieveOption)
                        {
                            case "1":
                                RetrieveStudents();
                                break;
                            case "2":
                                RetrieveCourses();
                                break;
                            case "3":
                                RetrieveEnrollments();
                                break;
                            case "4":
                                RetrievePayments();
                                break;
                            case "5":
                                RetrieveTeachers();
                                break;
                        }
                        break;

                     Data Insertion and Updating
                    case "3":
                        Console.WriteLine("Choose: 1. Insert Enrollment, 2. Insert Payment, 3. Update Student Information");
                        string insertUpdateOption = Console.ReadLine();
                        switch (insertUpdateOption)
                        {
                            case "1":
                                InsertEnrollment();
                                break;
                            case "2":
                                InsertPayment();
                                break;
                            case "3":
                                UpdateStudentInfo(); // Call the UpdateStudentInfo method here
                                break;
                        }
                        break;

                     Transaction Management
                    case "4":
                        Console.WriteLine("Choose: 1. Enroll Student, 2. Assign Teacher, 3. Record Payment");
                        string transactionOption = Console.ReadLine();
                        switch (transactionOption)
                        {
                            case "1":
                                EnrollStudentTransaction();
                                break;
                            case "2":
                                AssignTeacherTransaction();
                                break;
                            case "3":
                                RecordPaymentTransaction();
                                break;
                        }
                        break;

                     Dynamic Query Builder
                    case "5":
                        Console.WriteLine("Enter your SQL query: ");
                        string query = Console.ReadLine();
                        DynamicQuery(query);
                        break;

                     Exit
                    case "6":
                        exitToMain = false;
                        break;

                    default:
                        Console.WriteLine("Invalid Input! Try again.");
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

         Keep the console open until the user presses a key
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }

     Update Student Info Method
    public static void UpdateStudentInfo()
    {
        try
        {
            using (SqlConnection connection = GetDBConnection())
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


     Database Initialization Method
    public static void InitializeDatabase()
    {
        try
        {
            using (SqlConnection connection = GetDBConnection())
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

     Data Retrieval Methods
    public static void RetrieveStudents()
    {
        using (SqlConnection connection = GetDBConnection())
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
        using (SqlConnection connection = GetDBConnection())
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
        using (SqlConnection connection = GetDBConnection())
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
        using (SqlConnection connection = GetDBConnection())
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
        using (SqlConnection connection = GetDBConnection())
        {
            connection.Open();
            string sql = "SELECT * FROM Teachers";
            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"Teacher: {reader["first_name"]} {reader["last_name"]}, Email: {reader["email"]}");
            }
        }
    }
    public static bool CourseExists(int courseId)
    {
        using (SqlConnection connection = GetDBConnection())
        {
            connection.Open();
            string sql = "SELECT COUNT(*) FROM Courses WHERE course_id = @CourseId";
            using (SqlCommand cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@CourseId", courseId);
                int count = (int)cmd.ExecuteScalar();
                return count > 0; // Return true if course exists
            }
        }
    }

    public static bool StudentExists( int studentId)
    {
        using (SqlConnection connection = GetDBConnection())
        {
            connection.Open();
            string sql = "SELECT COUNT(*) FROM Students WHERE student_id = @StudentId";
            using (SqlCommand cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@StudentId", studentId);
                int count = (int)cmd.ExecuteScalar();
                return count > 0; // Return true if student exists
            }
        }
    }


    public static void InsertEnrollment()
    {
        try
        {
            using (SqlConnection connection = GetDBConnection())
            {
                connection.Open();
                Console.WriteLine("Enter Student ID: ");
                int studentId = int.Parse(Console.ReadLine());

                if (!StudentExists(studentId))
                {
                    Console.WriteLine("Student does not exist.");
                    return;
                }

                Console.WriteLine("Enter Course ID: ");
                int courseId = int.Parse(Console.ReadLine());

                if (!CourseExists(courseId))
                {
                    Console.WriteLine("Course does not exist.");
                    return;
                }

                string sql = "INSERT INTO Enrollments (student_id, course_id, enrollment_date) VALUES (@StudentId, @CourseId, @EnrollmentDate)";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@StudentId", studentId);
                    cmd.Parameters.AddWithValue("@CourseId", courseId);
                    cmd.Parameters.AddWithValue("@EnrollmentDate", DateTime.Now);
                    cmd.ExecuteNonQuery();

                    Console.WriteLine("Enrollment added successfully.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error inserting enrollment: {ex.Message}");
        }
    }



    public static void InsertPayment()
    {
        using (SqlConnection connection = GetDBConnection())
        {
            connection.Open();
            Console.WriteLine("Enter Student ID: ");
            int studentId = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Amount: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            string sql = "INSERT INTO Payments (student_id, amount, payment_date) VALUES (@studentId, @amount, @paymentDate)";
            SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@studentId", studentId);
            cmd.Parameters.AddWithValue("@amount", amount);
            cmd.Parameters.AddWithValue("@paymentDate", DateTime.Now);
            cmd.ExecuteNonQuery();

            Console.WriteLine("Payment added successfully.");
        }
    }

     Transaction Management Methods
    public static void EnrollStudentTransaction()
    {

        Console.WriteLine("Enroll Student Transaction not yet implemented.");
    }

    public static void AssignTeacherTransaction()
    {

        Console.WriteLine("Assign Teacher Transaction not yet implemented.");
    }

    public static void RecordPaymentTransaction()
    {
         Implement the logic for recording payment transactions here.
        Console.WriteLine("Record Payment Transaction not yet implemented.");
    }

     Dynamic Query Method
    public static void DynamicQuery(string query)
    {
        using (SqlConnection connection = GetDBConnection())
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                 Display results based on the dynamic query
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.Write($"{reader[i]} ");
                }
                Console.WriteLine();
            }
        }

    }
    public static SqlConnection GetDBConnection()
    {
        string connectionString = "Data Source=LAPTOP-2H2P4CPV;Initial Catalog=SISDB;Integrated Security=True;Encrypt=False";
        SqlConnection connection = new SqlConnection(connectionString);
        return connection;
    }



}
}
//namespace StudentInformationSystem
//{
//    //Task 9
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            bool exit = false;
//            while (!exit)
//            {
//                Console.WriteLine("1. Assign Teacher to Course");
//                Console.WriteLine("2. Retrieve Courses");
//                Console.WriteLine("3. Exit");
//                Console.Write("Enter your choice: ");
//                int choice;

//                if (int.TryParse(Console.ReadLine(), out choice))
//                {
//                    switch (choice)
//                    {
//                        case 1:
//                            AssignTeacherToCourse();
//                            break;
//                        case 2:
//                            RetrieveCourses();
//                            break;
//                        case 3:
//                            exit = true;
//                            break;
//                        default:
//                            Console.WriteLine("Invalid choice, please try again.");
//                            break;
//                    }
//                }
//                else
//                {
//                    Console.WriteLine("Invalid input. Please enter a number.");
//                }
//            }
//        }

//        public static void AssignTeacherToCourse()
//        {
//            // Define Sarah Smith's information
//            string teacherFirstName = "Sarah";
//            string teacherLastName = "Smith";
//            string teacherEmail = "sarah.smith@example.com";

//            // Define course details
//            string courseCode = "CS302";
//            string courseName = "Advanced Database Management";
//            int courseCredits = 4;

//            string connectionString = "Data Source=LAPTOP-2H2P4CPV;Initial Catalog=SISDB;Integrated Security=True;Encrypt=False";
//            using (SqlConnection connection = new SqlConnection(connectionString))
//            {
//                connection.Open();

//                // Step 1: Check if the course exists
//                string findCourseQuery = "SELECT course_id FROM Courses WHERE course_code = @CourseCode";
//                using (SqlCommand findCourseCommand = new SqlCommand(findCourseQuery, connection))
//                {
//                    findCourseCommand.Parameters.AddWithValue("@CourseCode", courseCode);
//                    object courseIdObj = findCourseCommand.ExecuteScalar();

//                    if (courseIdObj != null)
//                    {
//                        // Course exists, get the course_id
//                        int courseId = (int)courseIdObj;
//                        AssignTeacherToExistingCourse(connection, teacherEmail, teacherFirstName, teacherLastName, courseId);
//                    }
//                    else
//                    {
//                        // Course does not exist, add a new course
//                        Console.WriteLine("Course with code {0} not found. Adding a new course.", courseCode);
//                        AddNewCourseWithTeacher(connection, teacherEmail, teacherFirstName, teacherLastName, courseCode, courseName, courseCredits);
//                    }
//                }
//            }
//        }

//        private static void AssignTeacherToExistingCourse(SqlConnection connection, string teacherEmail, string teacherFirstName, string teacherLastName, int courseId)
//        {
//            // Step 2: Check if the teacher exists
//            string findTeacherQuery = "SELECT teacher_id FROM Teacher WHERE email = @TeacherEmail";
//            using (SqlCommand findTeacherCommand = new SqlCommand(findTeacherQuery, connection))
//            {
//                findTeacherCommand.Parameters.AddWithValue("@TeacherEmail", teacherEmail);
//                object teacherIdObj = findTeacherCommand.ExecuteScalar();
//                int teacherId;

//                if (teacherIdObj == null)
//                {
//                    // Step 3: Get the next available teacher_id manually
//                    teacherId = GetNextTeacherId(connection);

//                    // Insert Sarah Smith into the Teacher table
//                    InsertNewTeacher(connection, teacherId, teacherFirstName, teacherLastName, teacherEmail);
//                }
//                else
//                {
//                    // Teacher exists, get the teacher_id
//                    teacherId = (int)teacherIdObj;
//                }

//                // Step 4: Update the course with the teacher_id
//                UpdateCourseWithTeacherId(connection, courseId, teacherId);
//            }
//        }

//        private static int GetNextTeacherId(SqlConnection connection)
//        {
//            string getMaxTeacherIdQuery = "SELECT ISNULL(MAX(teacher_id), 0) FROM Teacher";
//            using (SqlCommand getMaxTeacherIdCommand = new SqlCommand(getMaxTeacherIdQuery, connection))
//            {
//                return (int)getMaxTeacherIdCommand.ExecuteScalar() + 1; // Increment for new teacher_id
//            }
//        }

//        private static void InsertNewTeacher(SqlConnection connection, int teacherId, string firstName, string lastName, string email)
//        {
//            string insertTeacherQuery = "INSERT INTO Teacher (teacher_id, first_name, last_name, email) VALUES (@TeacherId, @FirstName, @LastName, @Email)";
//            using (SqlCommand insertTeacherCommand = new SqlCommand(insertTeacherQuery, connection))
//            {
//                insertTeacherCommand.Parameters.AddWithValue("@TeacherId", teacherId);
//                insertTeacherCommand.Parameters.AddWithValue("@FirstName", firstName);
//                insertTeacherCommand.Parameters.AddWithValue("@LastName", lastName);
//                insertTeacherCommand.Parameters.AddWithValue("@Email", email);
//                insertTeacherCommand.ExecuteNonQuery();
//            }
//        }

//        private static void UpdateCourseWithTeacherId(SqlConnection connection, int courseId, int teacherId)
//        {
//            string updateCourseQuery = "UPDATE Courses SET teacher_id = @TeacherId WHERE course_id = @CourseId";
//            using (SqlCommand updateCourseCommand = new SqlCommand(updateCourseQuery, connection))
//            {
//                updateCourseCommand.Parameters.AddWithValue("@TeacherId", teacherId);
//                updateCourseCommand.Parameters.AddWithValue("@CourseId", courseId);
//                int rowsAffected = updateCourseCommand.ExecuteNonQuery();

//                Console.WriteLine(rowsAffected > 0 ? "Successfully assigned teacher to the course." : "Failed to assign teacher to the course.");
//            }
//        }

//        private static void AddNewCourseWithTeacher(SqlConnection connection, string teacherEmail, string teacherFirstName, string teacherLastName, string courseCode, string courseName, int courseCredits)
//        {
//            int teacherId = GetNextTeacherId(connection);
//            InsertNewTeacher(connection, teacherId, teacherFirstName, teacherLastName, teacherEmail);

//            // Step 7: Insert the new course with the assigned teacher
//            string insertCourseQuery = "INSERT INTO Courses (course_name, course_code, credits, teacher_id) VALUES (@CourseName, @CourseCode, @Credits, @TeacherId)";
//            using (SqlCommand insertCourseCommand = new SqlCommand(insertCourseQuery, connection))
//            {
//                insertCourseCommand.Parameters.AddWithValue("@CourseName", courseName);
//                insertCourseCommand.Parameters.AddWithValue("@CourseCode", courseCode);
//                insertCourseCommand.Parameters.AddWithValue("@Credits", courseCredits);
//                insertCourseCommand.Parameters.AddWithValue("@TeacherId", teacherId);

//                int rowsAffected = insertCourseCommand.ExecuteNonQuery();

//                Console.WriteLine(rowsAffected > 0 ? $"New course '{courseName}' added with Teacher {teacherFirstName} {teacherLastName} assigned." : "Failed to add the new course.");
//            }
//        }

//        public static void RetrieveCourses()
//        {
//            string connectionString = "Data Source=LAPTOP-2H2P4CPV;Initial Catalog=SISDB;Integrated Security=True;Encrypt=False";
//            using (SqlConnection connection = new SqlConnection(connectionString))
//            {
//                connection.Open();
//                string retrieveCoursesQuery = "SELECT course_id, course_name, course_code, credits, teacher_id FROM Courses";

//                using (SqlCommand retrieveCoursesCommand = new SqlCommand(retrieveCoursesQuery, connection))
//                {
//                    using (SqlDataReader reader = retrieveCoursesCommand.ExecuteReader())
//                    {
//                        Console.WriteLine("Courses:");

//                        // Display column headers
//                        Console.WriteLine("{0,-10} {1,-30} {2,-15} {3,-10} {4,-10}", "Course ID", "Course Name", "Course Code", "Credits", "Teacher ID");

//                        // Read and display each course
//                        while (reader.Read())
//                        {
//                            int courseId = reader.GetInt32(0);
//                            string courseName = reader.GetString(1);
//                            string courseCode = reader.IsDBNull(2) ? null : reader.GetString(2);
//                            int credits = reader.GetInt32(3);
//                            int teacherId = reader.IsDBNull(4) ? 0 : reader.GetInt32(4); // Handle possible null values

//                            Console.WriteLine("{0,-10} {1,-30} {2,-15} {3,-10} {4,-10}", courseId, courseName, courseCode, credits, teacherId);
//                        }
//                    }
//                }
//            }
//        }
//    }
//}
