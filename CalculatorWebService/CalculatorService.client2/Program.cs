using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorService.client2
{
	public class Program
	{
		// Class for testing with defined parameters
		public static TestCalculator fullTest = new TestCalculator();
		// Class for testing with user parameters
		public static ConexionController test = new ConexionController();


		public static string IdStoreOperation() {
			string Id_Tracking = "";
			Console.WriteLine("Do you want to save the operations?(Y/N)");
			string decision = Console.ReadLine();
			switch (decision.ToLowerInvariant().Trim())
			{
				case "y":
				case "yes":
					do
					{
						Console.WriteLine("Enter your Id Tracking");
						Id_Tracking = Console.ReadLine();
					} while (Id_Tracking == "");
					break;
			}
			return Id_Tracking;
		}

		public static void PrintMenu()
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("Welcome to the Menu.Write an option to continue or 'exit' to go out.");
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.WriteLine("1. Add");
			Console.WriteLine("2. Subtract");
			Console.WriteLine("3. Multiply");
			Console.WriteLine("4. Divide");
			Console.WriteLine("5. History");
			Console.WriteLine("6. Test");
			Console.WriteLine("7. Exit");
		}

		public static void History(string Id_Tracking){
			

			if (Id_Tracking != "")
			{
				test.Journal(Id_Tracking);
			}
			else
			{
				fullTest.GetHistory("http://localhost:53459/calculator/history");
			}
		}

		public static void CallFunction(string Id_Tracking)
		{

			string selection = Console.ReadLine();

			switch (selection.ToLowerInvariant().Trim())
			{
				case "add":
				case "1":
					test.Add(Id_Tracking);
					break;
				case "subtract":
				case "2":
					test.Sub(Id_Tracking);
					break;
				case "multiply":
				case "3":
					test.Mult(Id_Tracking);
					break;
				case "divide":
				case "4":
					test.Div(Id_Tracking);
					break;
				case "5":
				case "history":
					History(Id_Tracking);
					break;
				case "test":
				case "6":
					fullTest.test();
					break;
				case "exit":
				case "7":
					// Close Console Windows
					Environment.Exit(0);
					break;
				default:
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Word not valid.Try again!");
					Console.ForegroundColor = ConsoleColor.Gray;
					break;
			}
			Console.WriteLine("---------------------------------------------------");
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("Thanks for try it! Press any key for show the Menu.");
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.ReadLine();
		}

		static void Main(string[] args)
		{
			// Id_Tracking if you want save on the journal the operations
					string Id_Tracking = IdStoreOperation();
					PrintMenu();
					CallFunction(Id_Tracking);
		}
	}
}