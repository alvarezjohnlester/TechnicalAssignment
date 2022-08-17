using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TechTieraTechnicalAssignment.Models;

namespace TechTieraTechnicalAssignment.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly string _apiUrl;
		public HomeController(ILogger<HomeController> logger, string apiUrl)
		{
			_logger = logger;
			_apiUrl = apiUrl;
		}

		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> UploadFile( IFormFile formFile)
		{
			var formData = new Dictionary<string, IFormFile>();
			formData.Add("formFile", formFile);

			var content = new FormUrlEncodedContent(formData);

			using (var httpClient = new HttpClient())
			{
				var httpResponse = await httpClient.PostAsync(theurl, content);

				var responseString = await httpResponse.Content.ReadAsStringAsync();
			}
				return View();
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
