using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechTieraTechnicalAssignment.Models
{
	public class TransactionData
	{
		public int Id { get; set; }
		public string TransactionId { get; set; }
		public decimal? Amount { get; set; }
		public string CurrencyCode { get; set; }
		public DateTime?  TransactionDate { get; set; }
		public string status { get; set; }
	}
}
