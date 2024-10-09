using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.Entity;

namespace StudentInformationSystem.BusinessLayer.Repository
{
    public interface IPaymentRepository

    {
        
        Student GetStudent(int studentID);
        decimal GetPaymentAmount(int paymentID);
        DateTime GetPaymentDate(int paymentID);
        void RecordPayment(int studentID, decimal amount, DateTime paymentDate);
        List<(decimal Amount, DateTime PaymentDate)> GetPaymentHistory(int studentID);
        List<Payment> GetPaymentsByStudentID(int studentID);
        void AddPayment(int paymentID, int studentId, decimal amount, DateTime paymentDate);
         
    }
}
