using StudentInformationSystem.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.BusinessLayer.Services
{
    public interface ICourseService
    {
        void AddCourse(string courseName, string courseCode, string instructor);
        Course GetCourseByID(int courseID);
        void UpdateCourse(Course updatedCourse);
        List<Course> GetAllCourses();
        void AssignTeacherToCourse(int teacherID, int courseID);
        Course GetCourseInfo(int courseID);
    }

}
