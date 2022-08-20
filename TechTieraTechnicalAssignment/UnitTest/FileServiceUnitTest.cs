using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using TechTieraTechnicalAssignment;
using TechTieraTechnicalAssignment.Helpers;
using TechTieraTechnicalAssignment.Interfaces;
using TechTieraTechnicalAssignment.Models;

namespace UnitTest
{
	public class FileServiceUnitTest
	{
		public IFileService _fileService;
		[SetUp]
		public void Setup()
		{
			_fileService = new FileService();
		}

		[Test]
		public void FileService_Pass_WhenFileIsCSV()
		{
			string filePath = Path.Combine(Directory.GetCurrentDirectory(), $"AppDataFiles\\TestTransactionData.csv");
			bool result = Helpers.ValidateFileType(filePath);
			Assert.IsTrue(result);
		}
		[Test]
		public void FileService_Pass_WhenFileIsXML()
		{
			string filePath = Path.Combine(Directory.GetCurrentDirectory(), $"AppDataFiles\\TransactionData.xml");
			bool result = Helpers.ValidateFileType(filePath);
			Assert.IsTrue(result);
		}
		[Test]
		public void FileService_Fail_WhenFileIsNotValid()
		{
			string filePath = Path.Combine(Directory.GetCurrentDirectory(), $"AppDataFiles\\TransactionData.txt");
			bool result = Helpers.ValidateFileType(filePath);
			Assert.IsFalse(result);
		}

		[Test]
		public void FileService_Pass_ProcessCSV()
		{
			List<TransactionData> transactionDatas = _fileService.ProcessFile("TestTransactionData.csv").Result;
			Assert.IsTrue(transactionDatas.Count > 0);
		}
		[Test]
		public void FileService_Pass_ProcessXML()
		{
			List<TransactionData> transactionDatas = _fileService.ProcessFile("TransactionData.xml").Result;
			Assert.IsTrue(transactionDatas.Count > 0);
		}

	}
}