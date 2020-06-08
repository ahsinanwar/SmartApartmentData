using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using Web.SmartApartment.Controllers;
using Web.SmartApartment.Models;

namespace Web.SmartApartment.Areas.Student.Controllers
{
	/// <summary>
	/// This is a student Controller class which is extended by BaseController
	/// </summary>
	/// <remarks>This controller received user input from web browser and send Rest API requests to the API server e.g 
	/// Get, Put, Post, Delete for student information. All these mehods add authorization header in their request. 
	/// </remarks>
	/////////////********To-Do***********
	///--> Generic Eception handling with exception action filters
	///--> Logging of user operations
	[Authorize]
	[Area("StudentInfo")]
	public class StudentController : BaseController
	{
		private IRestResponse response;
		public IActionResult Index()
		{
			ViewBag.StatusCode = "";
			ViewBag.Data = "";
			return View();
		}
		/// <summary>
		/// This function will call studentapi to get all student information
		/// </summary>
		/// <returns>It returns its response to view</returns>
		/// <remarks>This function use PerformGetAPI method from baseController which use RestClient to call api
		/// </remarks>
		///////////////*************TODO**********
		///Receive response into a view and pass that viewmodel to cshtml view.
		public async Task<IActionResult> GetAllValues()
		{
			response = await SendGetAPIRequest("https://localhost:44305/api/studentapi");
			ViewBag.StatusCode = response.StatusCode.ToString();
			ViewBag.Data = response.Content.ToString();
			return View(nameof(Index));
		}
		/// <summary>
		/// This function will call studentapi to get single student information
		/// </summary>
		/// <returns>It returns its response to view</returns>
		/// <remarks>This function use PerformGetAPI method from baseController which use RestClient to call api
		/// </remarks>
		public async Task<IActionResult> GetById()
		{
			response = await SendGetAPIRequest("https://localhost:44305/api/studentapi/2");
			ViewBag.StatusCode = response.StatusCode.ToString();
			ViewBag.Data = response.Content.ToString();
			//var result = await response.Content.ReadAsStringAsync();

			return View(nameof(Index));
		}
		/// <summary>
		/// This function will call studentapi to create student information
		/// </summary>
		/// <returns>It returns its response to view which is new created Student</returns>
		/// <remarks>This function use PerformPostAPI method from baseController which use RestClient to call api
		/// </remarks>
		public async Task<IActionResult> CreateObject()
		{
			string _jsonConvertedVM = JsonConvert.SerializeObject(GetSampleStudentData());
			response = await SendPostAPIRequest("https://localhost:44305/api/studentapi/",_jsonConvertedVM);
			ViewBag.StatusCode = response.StatusCode.ToString();
			ViewBag.Data = response.Content.ToString();
			return View(nameof(Index));
		}
		private ViewModelStudent GetSampleStudentData()
		{
			return new ViewModelStudent
			{
				StudentID = 9,
				StudentName = "Ahsin",
				Grade="School"
			};
		}

	}
}