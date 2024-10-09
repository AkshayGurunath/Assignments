using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.Entity;

namespace StudentInformationSystem.BusinessLayer.Repository
{
    public interface IStudentRepository
    {
        void EnrollInCourse(int studentID, int courseID);
        void UpdateStudentInfo(int studentID, string firstName, string lastName, DateTime dateOfBirth, string email, string phoneNumber);
        void MakePayment(int studentID, decimal amount, DateTime paymentDate);
        void DisplayStudentInfo(int studentID);
        List<Course> GetEnrolledCourses(int studentID);
        List<Payment> GetPaymentHistory(int studentID);
        void GenerateEnrollmentReport(Course course);
    }
}
    


