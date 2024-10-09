using StudentInformationSystem.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.BusinessLayer.Services
{
    public interface IStudentService
    {
        void EnrollInCourse(int studentID, int courseID, decimal paymentAmount);
        void UpdateStudentInfo(int studentID, string firstName, string lastName, DateTime dateOfBirth, string email, string phoneNumber); 
        void MakePayment(int studentID, decimal amount, DateTime paymentDate); 
        List<Course> GetEnrolledCourses(int studentID); 
        List<Payment> GetPaymentHistory(int studentID); 
        void DisplayStudentInfo(int studentID); 
        void GenerateEnrollmentReport(Course course);
        void GeneratePaymentReport(int StudentID);
       


    }
}
