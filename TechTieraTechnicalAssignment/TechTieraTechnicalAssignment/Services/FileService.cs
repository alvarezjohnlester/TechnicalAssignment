using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TechTieraTechnicalAssignment.Interfaces;

namespace TechTieraTechnicalAssignment
{
	public class FileService  : IFileService
	{
		public FileService()
		{

		}

		public async Task<string> SaveFile(IFormFile formFile)
		{
			string filePath = Path.Combine(Directory.GetCurrentDirectory(), $"AppDataFiles\\{DateTime.UtcNow.ToString("yyyyMMdd'T'HHmmssfff'Z'")}_{formFile.FileName}" );
			
			using (Stream fileStream = new FileStream(filePath, FileMode.Create))
			{
				await formFile.CopyToAsync(fileStream);
			}
			return Path.GetFileName(filePath);
		}
	}
}
