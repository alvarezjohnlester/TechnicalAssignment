using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTieraTechnicalAssignment.Models;

namespace TechTieraTechnicalAssignment.Interfaces
{
	public interface IFileReader
	{
		public Task<List<TransactionData>> ProcessFile(string file);
	}
}
