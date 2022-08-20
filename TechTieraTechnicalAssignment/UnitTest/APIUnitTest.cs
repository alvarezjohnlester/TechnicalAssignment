using AutoMapper;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTieraTechnicalAssignment.Interfaces;
using TechTieraTechnicalAssignment.Mapper;
using TechTieraTechnicalAssignment.Models;
using TechTieraTechnicalAssignment.Models.DTO;
using TechTieraTechnicalAssignment.Services;

namespace UnitTest
{
	public class APIUnitTest
	{

		public IGetByCurrency _getByCurrency;
		public IGetByStatus _getByStatus;
		public IGetByDateRange _getByDateRange;
		public IMapper _mapper;
		public DBConnection _dBConnection;
		[SetUp]
		public void Setup()
		{
			_mapper = new Mapper(new MapperConfiguration(configure => { configure.AddProfile<TransactionProfile>(); }));
			_dBConnection = new DBConnection() { DefaultConnection = "Server=127.0.0.1;Database=TechTiera;User Id=localadmin; Password=localdbadmin; Trusted_Connection=false;MultipleActiveResultSets=true" };

			_getByCurrency = new GetByCurrencyService(_dBConnection, _mapper);
			_getByStatus = new GetByStatusService(_dBConnection, _mapper);
			_getByDateRange = new GetByDateRangeService(_dBConnection, _mapper);

		}
		[Test]
		public void GetByCurrencyCode_Pass_ShouldReturnValue()
		{
			GetByCurrencyRequest request = new GetByCurrencyRequest()
			{
				Currency = "USD"
			};
			IList<TransactionDTO> response = _getByCurrency.GetData(request).Result;
			Assert.IsNotNull(response);
		}
		[Test]
		public void GetByStatus_Pass_ShouldReturnValue()
		{
			GetByStatusRequest request = new GetByStatusRequest()
			{
				Status = "Approved"
			};
			IList<TransactionDTO> response = _getByStatus.GetData(request).Result;
			Assert.IsNotNull(response);
		}
		[Test]
		public void GetByDateRange_Pass_ShouldReturnValue()
		{
			GetByDateRangeRequest request = new GetByDateRangeRequest()
			{
				StartDate = "20-02-2019",
				EndDate = "20-02-2019"
			};
			IList<TransactionDTO> response = _getByDateRange.GetData(request).Result;
			Assert.IsNotNull(response);
		}
	}
}
