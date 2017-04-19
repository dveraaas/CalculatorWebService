using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorService.client2
{
	class Program
	{
		static void Main(string[] args)
		{
				// Class for testing with defined parameters
				TestCalculator fullTest = new TestCalculator();

				// Class for testing with user parameters
				ConexionController test = new ConexionController();

				// Id_Tracking if you want save on the journal the operations
				string selection,Id_Tracking="";
				Console.WriteLine("Do you want to save the operations?(Y/N)");
				string decision = Console.ReadLine();
				switch (decision.ToLowerInvariant().Trim())
				{
					case "y":
					case "yes":
					do {
						Console.WriteLine("Enter your Id Tracking");
						Id_Tracking = Console.ReadLine();
					} while (Id_Tracking=="");
						break;
				}

				//Console menu
				do
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
					selection = Console.ReadLine();
					
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
							if (Id_Tracking != "")
							{
								test.Journal(Id_Tracking);
							}else
							{
								fullTest.GetHistory("http://localhost:53459/calculator/history");
							}
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
				} while (
						(selection.ToLowerInvariant().Trim() != "add" || selection.ToLowerInvariant().Trim() != "1") 
						&& (selection.ToLowerInvariant().Trim() != "subtract" || selection.ToLowerInvariant().Trim() != "2")
						&& (selection.ToLowerInvariant().Trim() != "multiply" || selection.ToLowerInvariant().Trim() != "3")
						&& (selection.ToLowerInvariant().Trim() != "divide" || selection.ToLowerInvariant().Trim() != "4")
						&& (selection.ToLowerInvariant().Trim() != "history" || selection.ToLowerInvariant().Trim() != "5")
						&& (selection.ToLowerInvariant().Trim() != "test" || selection.ToLowerInvariant().Trim() != "6")
						&& (selection.ToLowerInvariant().Trim() != "exit" || selection.ToLowerInvariant().Trim() != "7"));
		}
	}
}