using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechTieraTechnicalAssignment.Interfaces
{
	public interface IFileService
	{
		 Task<string> SaveFile(IFormFile formFile);
	}
}
