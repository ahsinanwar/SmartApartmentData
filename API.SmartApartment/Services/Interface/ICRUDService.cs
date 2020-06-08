using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.SmartApartment.Services.Interface
{
    /// <summary>
    /// This is an generic interface for CRUD operation for any datamodel where Dto is datamodel
    /// </summary>
    /// <remarks>It contains defination for those functions which are required for CRUD operations for every database table. 
    /// All those services which needs generic CRUD operation they can implement this interface</remarks>
    public interface ICRUDService<Dto> where Dto : class
    {
        /// <summary>
        /// This function extract a list of datamodel
        /// </summary>
        /// <returns>It returns a list of datamodel</returns>
        /////////////*********To-DO****************
        ///Extract datamodel DtoStudent based on below criteria
        ///-->Pagination
        ///-->search citeria
        List<Dto> GetAllItems();

        /// <summary>
        /// This function return datamodel against provided primary or unique key
        /// </summary>
        /// <param name="_id">id is primary or unique Key of datamodel</param>
        /// <returns>It returns a single datamodel object</returns>
        Dto GetSingleItem(int _id);
        /// <summary>
        /// This function add datamodel object into database
        /// </summary>
        /// <param name="_dtoEntity">This is datamodel object which will be created in database</param>
        /// <returns>Returns new created datamodel object</returns>
        Dto AddItem(Dto _dtoEntity);
    }
}
