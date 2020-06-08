using API.SmartApartment.Areas.Student.Data;
using API.SmartApartment.DataObjects;
using API.SmartApartment.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.SmartApartment.Services.Implementation
{
    /// <summary>
    /// This is StudentService class which implement ICRUDService
    /// </summary>
    /// <remarks>This class perform all the business & logic operations which are required for student information</remarks>
    public class StudentService : ICRUDService<DtoStudent>
    {
        private static List<DtoStudent> studentsList = new List<DtoStudent>();
        private DtoStudentsData dtoStudents=new DtoStudentsData();
        /// <summary>
        /// This is the constructor of this class, if we use any other service or repository then we must add its dependency
        /// </summary>
        public StudentService()
        {
            dtoStudents.BootstrapStudentDatabase(studentsList);
        }
        /// <summary>
        /// This function extracted a list of 
        /// </summary>
        /// <returns>It returns a list of DoStudents datamodel</returns>
        public List<DtoStudent> GetAllItems()
        {
            try
            {

                return studentsList;
            }
            catch (Exception ex)
            {

                throw new Exception("",ex);
            }
        }
        /// <summary>
        /// This function return DtoStudent datamodel agains student id
        /// </summary>
        /// <param name="_id">id is primary and unique Key of student which information needs to be extracted from database</param>
        /// <returns>It returns a single student information data in json</returns>
        public DtoStudent GetSingleItem(int _id)
        {
            try
            {

                return studentsList.First(aa => aa.StudentID == _id);
            }
            catch (Exception ex)
            {

                throw new Exception("", ex);
            }
        }
        /// <summary>
        /// This function add DTOStudent object into database
        /// </summary>
        /// <param name="_dtoStudent">Datamodel DtoStudent which is new student informaion profile which needs to be created into the system</param>
        /// <returns>It returns datamodel DTOStudent which is newly created aginst provided DtoStudent object</returns>
        public DtoStudent AddItem(DtoStudent _dtoStudent)
        {
            _dtoStudent.StudentID = studentsList.Max(aa => aa.StudentID) + 1;
            studentsList.Add(_dtoStudent);
            return _dtoStudent;
        }
    }
}
