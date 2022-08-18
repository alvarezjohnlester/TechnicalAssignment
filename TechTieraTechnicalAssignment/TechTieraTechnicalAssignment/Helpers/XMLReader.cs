using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTieraTechnicalAssignment.Interfaces;
using TechTieraTechnicalAssignment.Models;

namespace TechTieraTechnicalAssignment.Helpers
{
	public class XMLReader : IFileReader
	{
		public Task<List<TransactionData>> ProcessFile(string file)
		{
			throw new NotImplementedException();
		}
	}
}
