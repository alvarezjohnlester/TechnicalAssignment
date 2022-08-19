using AutoMapper;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TechTieraTechnicalAssignment.Interfaces;
using TechTieraTechnicalAssignment.Models;
using TechTieraTechnicalAssignment.Models.DTO;

namespace TechTieraTechnicalAssignment.Services
{
	public class GetByDateRangeService : IGetByDateRange
	{
		public readonly DBConnection _DBConnection;
		public IMapper _mapper;
		public GetByDateRangeService(DBConnection DBConnection, IMapper mapper)
		{
			_DBConnection = DBConnection;
			_mapper = mapper;
		}
		public async Task<IList<TransactionDTO>> GetData(GetByDateRangeRequest request)
		{
			string query = $"select * from TransactionData where TransactionDate BETWEEN  @startdate and @enddate";
			List<TransactionDTO> data = new List<TransactionDTO>();
			using (var connection = new SqlConnection(_DBConnection.DefaultConnection))
			{
				connection.Open();
				DynamicParameters dynamicParameters = new DynamicParameters();
				dynamicParameters.Add("@startdate", request.StartDate, DbType.DateTime, ParameterDirection.Input);
				dynamicParameters.Add("@enddate", request.EndDate, DbType.DateTime, ParameterDirection.Input);
				var response = connection.Query<TransactionData>(query, dynamicParameters);
				foreach (var item in response)
				{
					data.Add(_mapper.Map<TransactionDTO>(item));
				}
				connection.Close();
			}
			return data;
		}
	}
}
