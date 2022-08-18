using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TechTieraTechnicalAssignment.Models;

namespace TechTieraTechnicalAssignment.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ApiConfig _apiConfig;
		public HomeController(ILogger<HomeController> logger, ApiConfig apiConfig)
		{
			_logger = logger;
			_apiConfig = apiConfig;
		}

		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Index(IFormFile formFile)
		{
			try
			{
				if (formFile == null)
				{
					ModelState.Clear();
					return View();
				}
				HttpResponseMessage response = null;

				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(_apiConfig.apiURL);
					using (var content = new MultipartFormDataContent())
					{
						content.Add(new StreamContent(formFile.OpenReadStream())
						{
							Headers =
									{
										ContentLength = formFile.Length,
										ContentType = new MediaTypeHeaderValue(formFile.ContentType)
									}
						}, "formFile", formFile.FileName);
						 response = await client.PostAsync(_apiConfig.apiURL + "uploadfile", content);
					}
				}
				if (response.IsSuccessStatusCode)
				{
					ViewBag.Message = "Record Inserted successfully";
					ModelState.Clear();
					return View();
				}
				else if(response.StatusCode == HttpStatusCode.BadRequest)
				{
					ViewBag.Message = await response.Content.ReadAsStringAsync();
					ModelState.Clear();
					return View();
				}
				return View();
			}
			catch (Exception e)
			{
				ViewBag.Message = e.Message;
				return View();
			}

		}
		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
