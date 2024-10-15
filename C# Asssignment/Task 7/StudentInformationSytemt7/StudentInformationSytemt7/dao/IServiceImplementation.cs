using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSytemt7.dao
{
    internal interface IServiceImplementation
    {
        void UpdateStudentInfo(int studentId, string firstName, string lastName, string email, string phoneNumber);
    }
}
