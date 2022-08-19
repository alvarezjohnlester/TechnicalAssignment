using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TechTieraTechnicalAssignment.Models
{
	public class GetByDateRangeRequest
	{
		[Required]
		public string StartDate { get; set; }

		[Required]
		public string EndDate { get; set; }
	}
}
