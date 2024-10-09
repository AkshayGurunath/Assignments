using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using StudentInformationSystem.BusinessLayer.Exceptions;
using StudentInformationSystem.Entity;

namespace StudentInformationSystem.BusinessLayer.Repository
{
    public class StudentRepository
    {
        private List<Student> students;
        private List<Payment> payments;
        private List<Enrollment> enrollments;

        public StudentRepository()
        {
            students = new List<Student>();
            payments = new List<Payment>();
            enrollments = new List<Enrollment>();
        }

        public void AddStudent(Student student)
        {
            students.Add(student);
        }

        public void RemoveStudent(Student student)
        {
            students.Remove(student);
        }

        public Student GetStudentById(int studentId)
        {
            return students.Find(s => s.StudentID == studentId);
        }

        public List<Student> GetAllStudents()
        {
            return students;
        }

        public void AddPayment(Payment payment)
        {
            payments.Add(payment);
            var student = GetStudentById(payment.Student.StudentID);
            if (student != null)
            {
                student.Payments.Add(payment);
            }
        }

        public void AddEnrollmentToCourse(Course course, Student student, DateTime enrollmentDate)
        {
            if (course != null && student != null)
            {
                var enrollment = new Enrollment(student, course, enrollmentDate); // Assuming Enrollment constructor takes Student, Course, and Date
                course.AddEnrollment(enrollment);
            }
        }

        public List<Enrollment> GetEnrollmentsForStudent(Student student)
        {
            return enrollments.FindAll(e => e.Student.StudentID == student.StudentID);
        }
    }
}

