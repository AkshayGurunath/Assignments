using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.Entity;

namespace StudentInformationSystem.BusinessLayer.Repository
{
    public interface IEnrollmentRepository
    {
        // Method to retrieve the student associated with the enrollment
        Student GetStudent(int studentID);

        // Method to retrieve the course associated with the enrollment
        Course GetCourse(int courseID);
        void EnrollStudentInCourse(int studentID, int courseID);
        List<int> GetEnrolledCourses(int studentID);
    }

}
