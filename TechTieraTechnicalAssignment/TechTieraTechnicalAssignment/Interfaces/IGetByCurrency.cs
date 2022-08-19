using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTieraTechnicalAssignment.Models;
using TechTieraTechnicalAssignment.Models.DTO;

namespace TechTieraTechnicalAssignment.Interfaces
{
	public interface IGetByCurrency
	{
		Task<IList<TransactionDTO>> GetData(GetByCurrencyRequest getByCurrencyRequest);
	}
}
