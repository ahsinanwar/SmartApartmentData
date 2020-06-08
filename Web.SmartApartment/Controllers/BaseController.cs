﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using Web.SmartApartment.Models;

namespace Web.SmartApartment.Controllers
{
	/// <summary>
	/// This is BaseController
	/// </summary>
	/// <remarks>This controller can be extended by all controller which used common operations in their class, We have below
	/// functions in this class
	/// -->Generic Get Rest API call by using RestClient
	/// -->Generic Post Rest API call 
	/// -->Extract access_token, id_token, refresh token after Auth0 Authentication and store this information in session 
	/// or cookies</remarks>
	///////////***************TODO*************
	///-->Save Auth0 user details into asp.net core session
	///--> Logging of api requests which are created by web application users 
	public class BaseController : Controller
	{
		public VMUserInfoSession vmUserInfoSession = new VMUserInfoSession();
		public RestClient restClient = new RestClient();
		private IRestResponse response;
		/// <summary>
		/// This is async function which send Get api request to server
		/// </summary>
		/// <returns>It returns response of rest api</returns>
		/// <remarks>This function add JWT token which is generated by Auth0 into RestRequest Header with key authorzation
		/// </remarks>
		///////////***************TODO*************
		///-->If token authentication failed than send api request with refresh token
		///--> Exception handling Get API request generate any exception.
		public async Task<IRestResponse> SendGetAPIRequest(string url)
		{
			try
			{
				await SetupAuthorizationHeader();
				var client = new RestClient(url);
				var request = new RestRequest(Method.GET);
				request.AddHeader("authorization", "Bearer " + vmUserInfoSession.Accesskey);
				response = client.Execute(request);
			}
			catch (Exception ex)
			{
				throw new Exception("Exception while sending Get API request", ex);
			}
			return response;
		}
		/// <summary>
		/// This is async function which send Post api request to server
		/// </summary>
		/// <returns>It returns response of rest api</returns>
		/// <remarks>This function add JWT token which is generated by Auth0 into RestRequest Header with key authorzation
		/// </remarks>
		public async Task<IRestResponse> SendPostAPIRequest(string url,string jsonData)
		{
			try
			{
				await SetupAuthorizationHeader();
				var client = new RestClient(url);
				var request = new RestRequest();
				request.Method = Method.POST;
				request.AddHeader("Content-Type", "application/json");
				request.RequestFormat = DataFormat.Json;
				request.AddHeader("authorization", "Bearer " + vmUserInfoSession.Accesskey);
				request.AddJsonBody(jsonData);
				response = client.Execute(request);
			}
			catch (Exception ex)
			{
				throw new Exception("Exception while sending Post API request", ex);
			}
			return response;
		}
		
		/// <summary>
		/// This function will extract logged in user informaion from Auth0
		/// </summary>
		/// <returns></returns>
		/// <remarks>After extracting this information we assign this information to VMUserInfoSession viewmodel and it can be 
		/// accessible in all controllers which has extended BaseController
		/// </remarks>
		public async Task SetupAuthorizationHeader()
		{
			try
			{
				if (User.Identity.IsAuthenticated)
				{
					vmUserInfoSession.Accesskey = await HttpContext.GetTokenAsync("access_token");

					// if you need to check the Access Token expiration time, use this value
					// provided on the authorization response and stored.
					// do not attempt to inspect/decode the access token
					DateTime accessTokenExpiresAt = DateTime.Parse(
						await HttpContext.GetTokenAsync("expires_at"),
						CultureInfo.InvariantCulture,
						DateTimeStyles.RoundtripKind);

					string idToken = await HttpContext.GetTokenAsync("id_token");
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Exception at Setup Authorization Header",ex);
			}
		}
	}
}