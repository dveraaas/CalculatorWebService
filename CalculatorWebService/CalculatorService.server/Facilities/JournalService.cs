using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace CalculatorService.server2
{
	public class JournalService
	{
		// GET: Journal
		private const string STORE_PATH = "C:\\Users\\Diego\\.ssh\\CalculatorWebService\\CalculatorWebService\\CalculatorService.server\\Store.txt";

		public static void StoreOperation(Models.Operations op)
		{
			string journal = GetJournal();

			using (StreamWriter sw = new StreamWriter(STORE_PATH))
			{
				sw.WriteLine(journal);
				sw.WriteLine($"{ op.operation} => {op.Calculation} || {op.Key} || { op.Date}");
				sw.Close();
			}

		}

		public static string GetJournal()
		{
			string journal = "";

			// Create the file if not exists.
			if (!(File.Exists(STORE_PATH)))
			{
				using (StreamWriter sw = File.CreateText(STORE_PATH))
				{
					sw.WriteLine("* * * * History of Operations * * * *");
					sw.Close();
				}
			}

			using (StreamReader sr = new StreamReader(STORE_PATH))
			{
				journal = sr.ReadToEnd();
			}
			return journal.TrimEnd();
		}
	}
}