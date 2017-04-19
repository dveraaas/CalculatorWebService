using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace CalculatorService.server2.Models
{
	public class Journal
	{
		private string file= "C:\\Users\\Diego\\Documents\\Visual Studio 2015\\Projects\\CalculatorService.client\\CalculatorService.server2\\Store.txt";
		public void StoreOperation(Operations op)
		{
			string journal = GetJournal();
			
			using (StreamWriter sw = new StreamWriter(file))
			{
				sw.WriteLine(journal);
				sw.WriteLine(op.operation+" => "+op.Calculation+" || "+op.Key+" || "+op.Date);
				sw.Close();
			}

		}

		public string GetJournal()
		{
			string journal = "";

			// Create the file if not exists.
			if (!(File.Exists(file)))
			{
				using (StreamWriter sw = File.CreateText(file))
				{
					sw.WriteLine("* * * * History of Operations * * * *");
					sw.Close();
				}
			}

			using (StreamReader sr = new StreamReader(file))
			{
				journal = sr.ReadToEnd();
			}
			return journal.TrimEnd();
		}
	}
}