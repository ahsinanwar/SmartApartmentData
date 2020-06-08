using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using API.SmartApartment.Controllers;
using API.SmartApartment.DataObjects;
using API.SmartApartment.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace API.SmartApartment.Areas.Student.Controllers
{
    /// <summary>
    /// This is a student API Controller class.
    /// </summary>
    /// <remarks>This class receives Rest API requests e.g Get, Put, Post, Delete for student information. Authentication and 
    /// Authorization are handled by Auth0. To access these api requests client must have selective user permission in there 
    /// JWT token</remarks>
    //////////***********TODO**************////
    ///Autehticate and Authorize for third party developer with OAuth2.0 and they do not need credentials to access the api
    ///Add user Roles instead of permission, because roles have permission and it is much convenient to 
    ///use roles instead of permissions.
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAPIController : APIBaseController
    {
        ICRUDService<DtoStudent> studentCRUDService;
        /// <summary>
        /// This is the constructor of API Controller and responsible for dependencies of this api controller class
        /// </summary>
        /// <param name="_studentCRUDService">This is instance of Student service</param>
        /// <remarks></remarks>
        public StudentAPIController(ICRUDService<DtoStudent> _studentCRUDService)
        {
            studentCRUDService = _studentCRUDService;

        }
        /// <summary>
        /// This function receive get all student api request and extract that List through StudentService
        /// </summary>
        /// <returns>It returns list of students data in json</returns>
        /// <remarks>Api request needs read:students permission to authorize</remarks>
        [HttpGet]
        [Authorize("read:students")]
        public IActionResult Get()
        {
            try
            {

                return Ok(studentCRUDService.GetAllItems());
            }
            catch (Exception ex)
            {
                throw new Exception("", ex);
            }
        }

        /// <summary>
        /// This function get single student based on its primary or unique key
        /// </summary>
        /// <param name="id">id is primary and unique Key of student which information needs to be extracted from database</param>
        /// <returns>It returns a single student information data in json</returns>
        /// <remarks>Api request needs read:student permission to authorize. If id is less than 0 then we send 
        /// BadRequest as response and if no object found by service than we send ArgumentException with no student found message</remarks>
        // GET api/values/5
        [HttpGet("{id}")]
        [Authorize("read:student")]
        public IActionResult Get(int id)
        {
            try
            {
                if (id<0)
                    return BadRequest("Not a valid id");
                DtoStudent _dtoStudent = studentCRUDService.GetSingleItem(id);
                if (_dtoStudent == null)
                {
                    throw new ArgumentException($"No student found against id {id}.", nameof(id));
                }
                return Ok(_dtoStudent);
            }
            catch (Exception ex)
            {
                throw new Exception("", ex);
            }
        }
        /// <summary>
        /// This function create Student object into database
        /// </summary>
        /// <param name="_dtoStudent"></param>
        /// <returns>It returns new created student</returns>
        /// <remarks>Api request needs write:student permission to authorize. If Model is not valid than we send Badrequest 
        /// as reponse.</remarks>
        // GET api/values/5
        [HttpPost]
        [Authorize("create:student")]
        public IActionResult Post([FromBody]DtoStudent _dtoStudent)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid data");
            return Ok(studentCRUDService.AddItem(_dtoStudent));
        }
        /// <summary>
        /// This function edit student information
        /// </summary>
        /// <param name="_dtoStudent"></param>
        /// <returns></returns>
        /// <remarks>This is a unsecure api which do not need any type of authentication or authorization</remarks>
        [HttpPut]
        public IActionResult Put([FromBody]DtoStudent _dtoStudent)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid data");
            return Ok("");
        }
    }
}