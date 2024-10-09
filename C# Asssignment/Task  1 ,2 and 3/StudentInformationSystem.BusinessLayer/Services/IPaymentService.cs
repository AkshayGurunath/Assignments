using StudentInformationSystem.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.BusinessLayer.Services
{
    public interface IPaymentService
    {
        Student GetStudent(int studentID);
        decimal GetPaymentAmount(int paymentID);
        DateTime GetPaymentDate(int paymentID);
        void RecordPayment(int studentId, decimal amount, DateTime paymentDate);
        List<(decimal Amount, DateTime PaymentDate)> GetPaymentHistory(int studentId);
        void AddPayment(int paymentID, int studentID, decimal amount, DateTime paymentDate);
    }

}
