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
	public class GetByStatusService : IGetByStatus
	{
		public readonly DBConnection _DBConnection;
		public IMapper _mapper;
		public GetByStatusService(DBConnection DBConnection, IMapper mapper)
		{
			_DBConnection = DBConnection;
			_mapper = mapper;
		}
		public async Task<IList<TransactionDTO>> GetData(GetByStatusRequest request)
		{
			string query = $"select * from TransactionData where Status = @status";
			List<TransactionDTO> data = new List<TransactionDTO>();
			using (var connection = new SqlConnection(_DBConnection.DefaultConnection))
			{
				connection.Open();
				DynamicParameters dynamicParameters = new DynamicParameters();
				dynamicParameters.Add("@status", request.Status, DbType.String, ParameterDirection.Input);
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
