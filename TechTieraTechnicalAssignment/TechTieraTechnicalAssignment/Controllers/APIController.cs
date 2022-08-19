using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTieraTechnicalAssignment.Interfaces;
using TechTieraTechnicalAssignment.Models;
using TechTieraTechnicalAssignment.Models.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechTieraTechnicalAssignment.Controllers
{
	[ApiVersion("1.0")]
	[Route("api/transaction/v{version:apiVersion}")]
	[ApiController]
	public class APIController : ControllerBase
	{
		private readonly ILogger<APIController> _logger;
		private IGetByCurrency _getByCurrency;
		private IGetByStatus _getByStatus;
		private IGetByDateRange _getByDateRange;

		public APIController(ILogger<APIController> logger,IGetByCurrency getByCurrency, IGetByStatus getByStatus, IGetByDateRange getByDateRange )
		{
			_logger = logger;
			_getByCurrency = getByCurrency;
			_getByStatus = getByStatus;
			_getByDateRange = getByDateRange;
		}

		[HttpGet]
		[Route("GetByDateRange")]
		public async Task<IActionResult> Get([FromQuery] GetByDateRangeRequest request)
		{
			try
			{
				request.StartDate = DateTime.Parse(request.StartDate).ToShortDateString();
				request.EndDate = DateTime.Parse(request.EndDate).AddDays(1).AddMinutes(-1).ToString();
				var response = await _getByDateRange.GetData(request);
				return Ok(response);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		[HttpGet]
		[Route("GetByStatus")]
		public async Task<IActionResult> Get([FromQuery] GetByStatusRequest request)
		{
			try
			{
				var response = await _getByStatus.GetData(request);
				return Ok(response);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		[HttpGet]
		[Route("GetByCurrencyCode")]
		public async Task<IActionResult> Get([FromQuery] GetByCurrencyRequest request)
		{
			try
			{
				var response = await _getByCurrency.GetData(request);
				return Ok(response);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}
