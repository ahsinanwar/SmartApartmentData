using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.SmartApartment.DataObjects
{
    /// <summary>
    /// This is Student datamodel class
    /// </summary>
    /// <remarks>It contains all information about student</remarks>
    public class DtoStudent
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public string Grade { get; set; }
    }
}
