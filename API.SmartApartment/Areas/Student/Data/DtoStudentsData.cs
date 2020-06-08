using API.SmartApartment.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.SmartApartment.Areas.Student.Data
{
    /// <summary>
    /// This is DTOStudentData class
    /// </summary>
    /// <remarks>This class bootstrap data into DTOSudent</remarks>
    public class DtoStudentsData
    {

        /// <summary>
        /// This function add DTO Student objects into DTOStudent List
        /// </summary>
        /// <param name="StudentsDatabase">empty list of DtoStudent</param>
        /// <returns>It returns a list of students from pre loaded student data</returns>
        /// <remarks></remarks>
        public List<DtoStudent> BootstrapStudentDatabase(List<DtoStudent> StudentsDatabase)
        {
            if (StudentsDatabase.Count == 0)
            {
                StudentsDatabase.Add(new DtoStudent() { StudentID = 1, StudentName = "John", Grade = "School" });
                StudentsDatabase.Add(new DtoStudent() { StudentID = 2, StudentName = "Wick", Grade = "High School" });
                StudentsDatabase.Add(new DtoStudent() { StudentID = 3, StudentName = "Steve", Grade = "University" });
                StudentsDatabase.Add(new DtoStudent() { StudentID = 4, StudentName = "Mark", Grade = "School" });
                StudentsDatabase.Add(new DtoStudent() { StudentID = 5, StudentName = "Robbert", Grade = "High School" });
            }
            return StudentsDatabase;
        }
    }
}
