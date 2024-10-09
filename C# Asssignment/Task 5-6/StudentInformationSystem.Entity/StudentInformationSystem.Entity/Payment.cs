using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Entity
{
    public class Payment
    {
        public Student Student { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }

        // Constructor that takes Student, Amount, and PaymentDate as parameters
        public Payment(Student student, decimal amount, DateTime paymentDate)
        {
            Student = student;
            Amount = amount;
            PaymentDate = paymentDate;
        }
    }

}
