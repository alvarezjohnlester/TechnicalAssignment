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
		public readonly string _connString;
		public DataService(string connString)
		{
			_connString = connString;
		}
		public Reponse ProcessData(List<TransactionData> transactionDatas)
		{
			var sql = "insert into TransactionData (TransactionId,Amount,CurencyCode,TransactionDate,Status) values (@TransactionId,@Amount,@CurencyCode,@TransactionDate,@Status)";
			using (var connection = new SqlConnection(_connString))
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
								dynamicParameters.Add("@CurencyCode", item.CurrencyCode, DbType.String, ParameterDirection.Input);
								dynamicParameters.Add("@TransactionDate", item.TransactionDate, DbType.DateTime, ParameterDirection.Input);
								dynamicParameters.Add("@Status", item.status, DbType.String, ParameterDirection.Input);
								connection.Execute(sql, dynamicParameters, transaction: transaction);
							}
						}
						if (isNull)
						{
							transaction.Rollback();
							return new Reponse() { message = "Invalid Data", success = false };
						}
						else
						{
							transaction.Commit();
							return new Reponse() { message = "data imported", success = true };
						}
						
					}
					catch (Exception e)
					{
						transaction.Rollback();
						return new Reponse() { message =e.Message, success = false };
					}	
					
					
				}
			}
		}
	}
}
