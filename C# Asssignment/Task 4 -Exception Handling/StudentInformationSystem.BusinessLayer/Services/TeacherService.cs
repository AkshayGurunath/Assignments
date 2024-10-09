using StudentInformationSystem.BusinessLayer.Repository;
using StudentInformationSystem.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.BusinessLayer.Services
{
    public class TeacherService
    {
        private readonly TeacherRepository _teacherRepository;

        public TeacherService(TeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public void UpdateTeacherInfo(int teacherID, string firstName, string lastName, string email)
        {
            _teacherRepository.UpdateTeacherInfo(teacherID, firstName, lastName, email);
        }

        public void DisplayTeacherInfo()
        {
            _teacherRepository.DisplayTeacherInfo();
        }

        public List<Course> GetAssignedCourses()
        {
            return _teacherRepository.GetAssignedCourses();
        }
    }

}
