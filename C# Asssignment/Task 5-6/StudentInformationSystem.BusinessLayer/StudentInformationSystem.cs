using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.BusinessLayer.Exceptions;
using StudentInformationSystem.BusinessLayer.Repository;
using StudentInformationSystem.Entity;
using StudentInformationSystem.BusinessLayer.Services;
namespace StudentInformationSystem.BusinessLayer
{
    public class SIS
    {
        private StudentRepository studentRepository;
        private CourseRepository courseRepository;
        private TeacherRepository teacherRepository;
        private PaymentRepository paymentRepository;

        public SIS(StudentRepository studentRepo, CourseRepository courseRepo, TeacherRepository teacherRepo,PaymentRepository paymentRepo)
        {
            studentRepository = studentRepo;
            courseRepository = courseRepo;
            teacherRepository = teacherRepo;
            paymentRepository = paymentRepo;
        }

        public void AddStudent(Student student)
        {
            studentRepository.AddStudent(student); 
        }

        public void AddEnrollment(Student student, Course course, DateTime enrollmentDate)
        {
            var enrollment = new Enrollment(student, course, enrollmentDate);
            
        }

        public void AssignCourseToTeacher(Course course, Teacher teacher)
        {
            course.AssignTeacher(teacher);
            
        }

        public void AddPayment(Student student, decimal amount, DateTime paymentDate)
        {
            var payment = new Payment(student, amount, paymentDate);
            studentRepository.AddPayment(payment);
        }

        public List<Enrollment> GetEnrollmentsForStudent(Student student)
        {
            return studentRepository.GetEnrollmentsForStudent(student);
        }

        public List<Course> GetCoursesForTeacher(Teacher teacher)
        {
            return teacher.AssignedCourses;
        }

        public void AddCourse(Course newCourse)
        {
            throw new NotImplementedException();
        }

        public void AddTeacher(Teacher newTeacher)
        {
            throw new NotImplementedException();
        }

        public void AddEnrollment(int studentId, int courseId)
        {
            throw new NotImplementedException();
        }

        public void AssignCourseToTeacher(int teacherId, int courseId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Enrollment> GetEnrollmentsForStudent(int studentId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Course> GetCoursesForTeacher(int teacherId)
        {
            throw new NotImplementedException();
        }

        public void AddPayment(int studentId, decimal amount, DateTime now)
        {
            throw new NotImplementedException();
        }
    }
}



