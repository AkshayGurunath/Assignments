using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Entity
{
    public class CourseStatistics
    {
        public int CourseID { get; set; }
        public int NumberOfEnrollments { get; set; }
        public decimal TotalPayments { get; set; }
    }
}
