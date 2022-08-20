using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TechTieraTechnicalAssignment.Interfaces;
using TechTieraTechnicalAssignment.Models;

namespace TechTieraTechnicalAssignment.Controllers
{
	[ApiVersion("1.0")]
	[Route("api/file/v{version:apiVersion}")]
	[ApiController]
	public class FileUploaderController : ControllerBase
	{

		private readonly ILogger<FileUploaderController> _logger;
		private readonly IFileService _fileService;
		private readonly IDataService _dataService;
		public FileUploaderController(ILogger<FileUploaderController> logger, IFileService fileService, IDataService dataService )
		{
			_logger = logger;
			_fileService = fileService;
			_dataService = dataService;
		}
		[HttpPost]
		[Route("uploadfile")]
		public async Task<IActionResult> UploadFile(IFormFile formFile)
		{
			string filename = "";
			try
			{
				Response validation = Helpers.Helpers.Validate(formFile);
				if (!validation.success)
				{
					_logger.LogInformation(validation.message);
					return BadRequest(validation.message);
				}
				filename = await _fileService.SaveFile(formFile);
				List<TransactionData> transactionDatas = await _fileService.ProcessFile(filename);
				Response reponse = _dataService.ProcessData(transactionDatas);
				if (!reponse.success)
					return BadRequest("Invalid Data");
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
