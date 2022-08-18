using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TechTieraTechnicalAssignment.Helpers;
using TechTieraTechnicalAssignment.Interfaces;
using TechTieraTechnicalAssignment.Models;

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
		public async Task<List<TransactionData>> ProcessFile(string filename)
		{
			string extension = Path.GetExtension(filename);
			FileReader _fileReader = null;
			if (extension == ".csv")
			{
				_fileReader = new FileReader(new CSVReader());
			}
			else if(extension == ".xml")
			{
				_fileReader = new FileReader(new XMLReader());
			}
			return await _fileReader.ProcessFile(filename);
		}
	}
}
