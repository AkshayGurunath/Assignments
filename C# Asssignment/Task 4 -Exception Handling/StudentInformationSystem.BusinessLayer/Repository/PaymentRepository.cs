using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.Entity;

namespace StudentInformationSystem.BusinessLayer.Repository
{
    public class PaymentRepository : IPaymentRepository 
    {
        private readonly List<Payment> payments;
        private readonly StudentRepository _studentRepository;
        private readonly Dictionary<int, List<(decimal Amount, DateTime PaymentDate)>> _paymentData = new Dictionary<int, List<(decimal Amount, DateTime PaymentDate)>>();

        public PaymentRepository()
        {
            payments = new List<Payment>
            {
                new Payment { PaymentID = 1, StudentID = 101, Amount = 5000, PaymentDate = new DateTime(2024, 1, 15) },
                new Payment { PaymentID = 2, StudentID = 101, Amount = 3000, PaymentDate = new DateTime(2024, 2, 15) },
                new Payment { PaymentID = 3, StudentID = 102, Amount = 4000, PaymentDate = new DateTime(2024, 3, 15) }
            };
        }

        // Constructor to initialize the StudentRepository
        public PaymentRepository(StudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public List<Payment> GetPaymentsByStudentID(int studentID)
        {
            return payments.Where(p => p.StudentID == studentID).ToList();
        }

        public void AddPayment(int paymentID, int studentID, decimal amount, DateTime paymentDate)
        {
            var payment = new Payment
            {
                PaymentID = payments.Count + 1,
                StudentID = studentID,
                Amount = amount,
                PaymentDate = paymentDate 
            };
            payments.Add(payment);
            Console.WriteLine($"Payment of {amount:C} recorded for Student ID {studentID}.");
        }

        public Student GetStudent(int studentID)
        {
            return _studentRepository.GetStudentByID(studentID);
        }

        public decimal GetPaymentAmount(int paymentID)
        {
            var payment = payments.Find(p => p.PaymentID == paymentID);
            return payment != null ? payment.Amount : 0;
        }

        public DateTime GetPaymentDate(int paymentID)
        {
            var payment = payments.Find(p => p.PaymentID == paymentID);
            return payment != null ? payment.PaymentDate : DateTime.MinValue;
        }

        public void RecordPayment(int studentID, decimal amount, DateTime paymentDate)
        {
            if (!_paymentData.ContainsKey(studentID))
            {
                _paymentData[studentID] = new List<(decimal Amount, DateTime PaymentDate)>();
            }
            _paymentData[studentID].Add((amount, paymentDate));
            Console.WriteLine($"Payment of {amount} recorded for Student {studentID} on {paymentDate.ToShortDateString()}.");
        }

        public List<(decimal Amount, DateTime PaymentDate)> GetPaymentHistory(int studentID)
        {
            return _paymentData.ContainsKey(studentID) ? _paymentData[studentID] : new List<(decimal Amount, DateTime PaymentDate)>();
        }
    }
}
