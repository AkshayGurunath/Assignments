using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.Entity;

namespace StudentInformationSystem.BusinessLayer.Repository
{
    public interface ITeacherRepository
    {
        void UpdateTeacherInfo(int teacherID, string firstname, string lastname, string email);
        void DisplayTeacherInfo(int teacherID);
        List<Course> GetAssignedCourses(int teacherID);
    }

}
