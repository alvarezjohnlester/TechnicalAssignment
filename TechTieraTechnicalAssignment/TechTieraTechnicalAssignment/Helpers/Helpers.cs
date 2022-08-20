using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TechTieraTechnicalAssignment.Models;

namespace TechTieraTechnicalAssignment.Helpers
{
	public static class Helpers
	{
		public static bool ValidateFileType(string file)
		{
			string extension = Path.GetExtension(file);
			if (extension.ToLower() == ".csv" || extension.ToLower() == ".xml")
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		
		public static Response Validate(IFormFile formFile)
		{
			Response response = new Response();
			response.success = true;
			if (formFile.Length > 1000000)
			{
				response.message = "File is larger than 1mb";
				response.success = false;
			}
			if (!ValidateFileType(formFile.FileName))
			{
				response.message = "Unknown format";
				response.success = false;
			}
			return response;
		}
	}
}
