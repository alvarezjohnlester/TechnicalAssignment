using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TechTieraTechnicalAssignment.Helpers
{
	public static class Helpers
	{
		public static bool ValidateFileType(string file)
		{
			string extension = Path.GetExtension(file);
			if (extension != "csv" || extension != "xml")
			{
				return false;
			}
			else
			{
				return true;
			}
		}
	}
}
