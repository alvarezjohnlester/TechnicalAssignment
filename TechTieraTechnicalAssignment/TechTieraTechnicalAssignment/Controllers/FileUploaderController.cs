using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TechTieraTechnicalAssignment.Controllers
{
	[ApiVersion("1.0")]
	[Route("api/file/v{version:apiVersion}")]
	[ApiController]
	public class FileUploaderController : ControllerBase
	{

		private readonly ILogger<FileUploaderController> _logger;
		public FileUploaderController(ILogger<FileUploaderController> logger)
		{
			_logger = logger;
		}
		[HttpPost]
		[Route("uploadfile")]
		public IActionResult  UploadFile(IFormFile formFile)
		{
			try
			{
				if (!Helpers.Helpers.ValidateFileType(formFile.FileName))
				{
					_logger.LogInformation("Unknown format");
					return BadRequest("Unknown format");
				}

			}
			catch (Exception e)
			{
				_logger.LogInformation(e.Message);
				return BadRequest(e.Message);
			}
			return Ok();
		}
	}
}
