using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.Entity;

namespace StudentInformationSystem.BusinessLayer.Repository
{
    public class PaymentRepository
    {
        private List<Payment> _payments = new List<Payment>();

        public void AddPayment(Payment payment)
        {
            _payments.Add(payment);
        }

        public List<Payment> GetPaymentsForStudent(int studentID)
        {
            return _payments.Where(p => p.Student.StudentID == studentID).ToList();
        }

        // Additional methods as needed
    }
}



