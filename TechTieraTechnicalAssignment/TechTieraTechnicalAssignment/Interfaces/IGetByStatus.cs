using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTieraTechnicalAssignment.Models;
using TechTieraTechnicalAssignment.Models.DTO;

namespace TechTieraTechnicalAssignment.Interfaces
{
	public interface IGetByStatus
	{
		Task<IList<TransactionDTO>> GetData(GetByStatusRequest getByStatusRequest);
	}
}
