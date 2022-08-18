using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTieraTechnicalAssignment.Models;

namespace TechTieraTechnicalAssignment.Interfaces
{
	public interface IDataService
	{
		Reponse ProcessData(List<TransactionData> transactionDatas);
	}
}
