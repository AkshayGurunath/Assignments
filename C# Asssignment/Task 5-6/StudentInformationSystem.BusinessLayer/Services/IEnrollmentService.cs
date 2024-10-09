using StudentInformationSystem.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.BusinessLayer.Services
{
    public interface IEnrollmentService
    {
        Student GetStudent(int studentID);
        Course GetCourse(int courseID);
        void EnrollStudentInCourse(int studentID, int courseID);
        List<int> GetEnrolledCourses(int studentID);


    }

}
