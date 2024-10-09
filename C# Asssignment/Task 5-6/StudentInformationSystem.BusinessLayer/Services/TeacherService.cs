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

        public void AddTeacher(Teacher teacher)
        {
            _teacherRepository.AddTeacher(teacher);
        }

        public Teacher GetTeacherById(int teacherID)
        {
            return _teacherRepository.GetById(teacherID);
        }

        public List<Teacher> GetAllTeachers() // Using the new method in the repository
        {
            return _teacherRepository.GetAllTeachers();
        }

        public void UpdateTeacher(Teacher teacher)
        {
            _teacherRepository.UpdateTeacher(teacher);
        }

        public void RemoveTeacher(int teacherID)
        {
            _teacherRepository.RemoveTeacher(teacherID);
        }
    }
}
