using StudentInformationSystem.BusinessLayer.Repository;
using StudentInformationSystem.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.BusinessLayer.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        // Constructor that takes an IPaymentRepository
        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        // Method to retrieve the student associated with the payment
        public Student GetStudent(int studentID)
        {
            return _paymentRepository.GetStudent(studentID);
        }

        // Method to retrieve the payment amount
        public decimal GetPaymentAmount(int paymentID)
        {
            return _paymentRepository.GetPaymentAmount(paymentID);
        }

        // Method to retrieve the payment date
        public DateTime GetPaymentDate(int paymentID)
        {
            return _paymentRepository.GetPaymentDate(paymentID);
        }

        // Method to record a payment
        public void RecordPayment(int studentId, decimal amount, DateTime paymentDate)
        {
            _paymentRepository.RecordPayment(studentId, amount, paymentDate);
        }

        // Method to get payment history for a student
        public List<(decimal Amount, DateTime PaymentDate)> GetPaymentHistory(int studentId)
        {
            return _paymentRepository.GetPaymentHistory(studentId);
        }

        // Method to add a payment (optional)
        public void AddPayment(int paymentID, int studentID, decimal amount, DateTime paymentDate)
        {
            _paymentRepository.AddPayment(paymentID, studentID, amount, paymentDate);
        }
    }

}
