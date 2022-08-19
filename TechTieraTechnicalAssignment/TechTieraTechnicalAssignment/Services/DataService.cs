using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using TechTieraTechnicalAssignment.Interfaces;
using TechTieraTechnicalAssignment.Models;

namespace TechTieraTechnicalAssignment.Services
{
	public class DataService : IDataService
	{
		public readonly DBConnection _DBConnection;
		public DataService(DBConnection DBConnection)
		{
			_DBConnection = DBConnection;
		}
		public Response ProcessData(List<TransactionData> transactionDatas)
		{
			var sql = "insert into TransactionData (TransactionId,Amount,CurrencyCode,TransactionDate,Status) values (@TransactionId,@Amount,@CurrencyCode,@TransactionDate,@Status)";
			using (var connection = new SqlConnection(_DBConnection.DefaultConnection))
			{
				connection.Open();

				using (var transaction = connection.BeginTransaction())
				{
					try
					{
						bool isNull = true;
						foreach (var item in transactionDatas)
						{
							isNull = item.GetType().GetProperties().Any(p => p.GetValue(item) == null);
							if (!isNull)
							{
								DynamicParameters dynamicParameters = new DynamicParameters();
								dynamicParameters.Add("@TransactionId", item.TransactionId, DbType.String, ParameterDirection.Input);
								dynamicParameters.Add("@Amount", item.Amount, DbType.Decimal, ParameterDirection.Input);
								dynamicParameters.Add("@CurrencyCode", item.CurrencyCode, DbType.String, ParameterDirection.Input);
								dynamicParameters.Add("@TransactionDate", item.TransactionDate, DbType.DateTime, ParameterDirection.Input);
								dynamicParameters.Add("@Status", item.status, DbType.String, ParameterDirection.Input);
								connection.Execute(sql, dynamicParameters, transaction: transaction);
							}
						}
						if (isNull)
						{
							transaction.Rollback();
							return new Response() { message = "Invalid Data", success = false };
						}
						else
						{
							transaction.Commit();
							return new Response() { message = "data imported", success = true };
						}
						
					}
					catch (Exception e)
					{
						transaction.Rollback();
						return new Response() { message =e.Message, success = false };
					}	
				}
			}
		}
	}
}
