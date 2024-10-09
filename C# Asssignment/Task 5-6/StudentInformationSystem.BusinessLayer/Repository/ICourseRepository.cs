using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.Entity;

namespace StudentInformationSystem.BusinessLayer.Repository
{
    public interface ICourseRepository
    {
        void AssignTeacher(int courseID, Teacher teacher);
        void UpdateCourseInfo(int courseID, string courseCode, string courseName, string instructor);
        void DisplayCourseInfo(int courseID);
        List<Enrollment> GetEnrollments(int courseID);
        Teacher GetTeacher(int courseID);
        void AssignTeacherToCourse(int teacherID, int courseID);
        string GetCourseInfo(string courseID);
        int GetEnrollmentsByCourseID(int courseID);
        List<Payment> GetPaymentsByCourseID(int courseID);
        Course GetCourseById(int courseId);
    }

}
