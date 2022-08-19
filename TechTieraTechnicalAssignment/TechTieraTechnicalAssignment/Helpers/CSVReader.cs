using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TechTieraTechnicalAssignment.Interfaces;
using TechTieraTechnicalAssignment.Models;

namespace TechTieraTechnicalAssignment.Helpers
{
	public class CSVReader : IFileReader
	{
		public async Task<List<TransactionData>> ProcessFile(string file)
		{
			List<TransactionData> TransactionDatas = new List<TransactionData>();
			string filePath = Path.Combine(Directory.GetCurrentDirectory(), $"AppDataFiles\\{file}");
			using (TextFieldParser csvParser = new TextFieldParser(filePath))
			{
				csvParser.CommentTokens = new string[] { "#" };
				csvParser.SetDelimiters(new string[] { "," });
				csvParser.HasFieldsEnclosedInQuotes = true;
				//remove header
				csvParser.ReadLine();
				while (!csvParser.EndOfData)
				{
					// Read current line fields, pointer moves to the next line.
					string[] fields = csvParser.ReadFields();
					DateTime? d; DateTime dt;
					d = DateTime.TryParseExact(fields[3], "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt) ? dt : (DateTime?)null;
					decimal? amt; decimal a;
					amt = decimal.TryParse(fields[1], out a) ? a : (decimal?)null;

					TransactionData transactionData = new TransactionData();
					transactionData.TransactionId = fields[0];
					transactionData.Amount = amt;
					transactionData.CurrencyCode = fields[2]; 
					transactionData.TransactionDate = d;
					transactionData.status = fields[4];
					TransactionDatas.Add(transactionData);
				}
			}
			return TransactionDatas;
		}
	}
}
