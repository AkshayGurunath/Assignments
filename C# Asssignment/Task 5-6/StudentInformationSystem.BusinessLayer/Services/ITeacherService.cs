using StudentInformationSystem.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.BusinessLayer.Services
{
    public interface ITeacherService
    {
        void UpdateTeacherInfo(int teacherID, string firstName, string lastName, string email);
        void DisplayTeacherInfo(int teacherID);
        List<Course> GetAssignedCourses(int teacherID);
    }

}
