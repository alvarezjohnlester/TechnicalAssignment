using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TechTieraTechnicalAssignment.Interfaces;

namespace TechTieraTechnicalAssignment.Controllers
{
	[ApiVersion("1.0")]
	[Route("api/file/v{version:apiVersion}")]
	[ApiController]
	public class FileUploaderController : ControllerBase
	{

		private readonly ILogger<FileUploaderController> _logger;
		private readonly IFileService _fileService;
		public FileUploaderController(ILogger<FileUploaderController> logger, IFileService fileService)
		{
			_logger = logger;
			_fileService = fileService;
		}
		[HttpPost]
		[Route("uploadfile")]
		public async Task<IActionResult>  UploadFile(IFormFile formFile)
		{
			string filename = "";
			try
			{
				if (!Helpers.Helpers.ValidateFileType(formFile.FileName))
				{
					_logger.LogInformation("Unknown format");
					return BadRequest("Unknown format");
				}
				 filename = await _fileService.SaveFile(formFile);

			}
			catch (Exception e)
			{
				_logger.LogInformation(e.Message);
				return BadRequest(e.Message);
			}
			return Ok(filename);
		}
	}
}
