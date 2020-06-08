using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.SmartApartment.Controllers
{
    /// <summary>
    /// This is API Base Controller
    /// All API Controllers will extend this class
    /// </summary>
    /// <remarks>This class contains the generic helper functions which are used by Get,Put,Post and Delete actions in 
    /// api controller.</remarks>
    [Route("api/[controller]")]
    [ApiController]
    public class APIBaseController : ControllerBase
    {
        /// <typeparam name="T">T is ViewModel that will convert Json which is received with request</typeparam>
        /// <param name="json">it is json </param>
        public T ConvertJsonToModel<T>(string json)
        {
            T obj = JsonConvert.DeserializeObject<T>(json);
            return obj;
        }
    }
}