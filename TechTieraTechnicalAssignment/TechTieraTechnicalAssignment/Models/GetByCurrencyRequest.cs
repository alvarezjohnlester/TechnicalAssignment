using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TechTieraTechnicalAssignment.Models
{
	public class GetByCurrencyRequest
	{
		[Required]
		public string Currency { get; set; }
	}
}
