using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using TechTieraTechnicalAssignment.Interfaces;
using TechTieraTechnicalAssignment.Models;

namespace TechTieraTechnicalAssignment.Helpers
{
	public class XMLReader : IFileReader
	{
		public async Task<List<TransactionData>> ProcessFile(string file)
		{
			List<TransactionData> TransactionDatas = new List<TransactionData>();
			string filePath = Path.Combine(Directory.GetCurrentDirectory(), $"AppDataFiles\\{file}");
			try
			{
				XmlDocument doc = new XmlDocument();
				doc.Load(filePath);
				XmlNode node = doc.SelectSingleNode("Transactions");
				XmlNodeList prop = node.SelectNodes("Transaction");
				foreach (XmlNode item in prop)
				{
					XmlNode paymentDetails = item.SelectSingleNode("PaymentDetails");
					DateTime? d; DateTime dt;
					d = DateTime.TryParseExact(item["TransactionDate"].InnerText, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt) ? dt : (DateTime?)null;
					decimal? amt; decimal a;
					amt = decimal.TryParse(paymentDetails["Amount"].InnerText, out a) ? a : (decimal?)null;

					TransactionData transactionData = new TransactionData();
					transactionData.TransactionId = item.Attributes["id"].Value;
					transactionData.Amount = amt;
					transactionData.CurrencyCode = paymentDetails["CurrencyCode"].InnerText;
					transactionData.TransactionDate = d;
					transactionData.status = item["Status"].InnerText;
					TransactionDatas.Add(transactionData);
				}
				return TransactionDatas;
			}
			catch (Exception)
			{
				return TransactionDatas;
			}
		}
	}
}
