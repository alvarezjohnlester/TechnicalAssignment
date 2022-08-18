using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTieraTechnicalAssignment.Interfaces;
using TechTieraTechnicalAssignment.Models;

namespace TechTieraTechnicalAssignment.Helpers
{
	public class FileReader
	{
		private readonly IFileReader _fileReader;
		public FileReader(IFileReader fileReader)
		{
			_fileReader = fileReader;
		}
		public Task<List<TransactionData>> ProcessFile(string file)
		{
			return _fileReader.ProcessFile(file);
		}
	}
}
