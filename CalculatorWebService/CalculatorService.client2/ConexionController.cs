using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using CalculatorService.client2.Models;
using System.IO;
using Newtonsoft.Json;
using NLog;

namespace CalculatorService.client2
{
	public class ConexionController
	{
		private const string url= "http://localhost:53459/calculator/";
		private string op;
		private char separator;
		private AddRequest Reqadd = new AddRequest();
		private SubRequest Reqsub = new SubRequest();
		private MultRequest Reqmult = new MultRequest();
		private DivRequest Reqdiv = new DivRequest();
		private static Logger logger = LogManager.GetCurrentClassLogger();

		// Methods for user parameters depending selection 
		#region Add
		public void Add(string Id_Tracking)
		{
			Reqadd.Addends = new int[] { };
			Console.WriteLine("------- Operation Add -------");
			Console.WriteLine("Write the operation( Ej: 1+2+3 )");
			separator = '+';
			try
			{
					Console.WriteLine("Operation:");
					op = Console.ReadLine();

					string[] numbers = op.Trim().Split(separator);
					Reqadd.Addends = numbers.Select(x => int.Parse(x)).ToArray();

				HttpWebRequest req = (HttpWebRequest)WebRequest.Create($"{url}add");
				req.Method = "POST";
				req.ContentType = "application/json";

				if (Id_Tracking != "")
				{
					req.Headers.Add("X_Evi_Tracking_Id", Id_Tracking);
				}

				// Opens a stream,converts the request to Json and send it 
				using (StreamWriter sw = new StreamWriter(req.GetRequestStream()))
				{
					string jsonRequest = JsonConvert.SerializeObject(Reqadd);
					sw.Write(jsonRequest);
					sw.Close();
				}

				AddResponse response = new AddResponse();
				HttpWebResponse Response = (HttpWebResponse)req.GetResponse();
				using (StreamReader sr = new StreamReader(Response.GetResponseStream(), Encoding.UTF8))
				{
					Console.WriteLine("The server responds: ");
					response = JsonConvert.DeserializeObject<AddResponse>(sr.ReadToEnd());
					Console.WriteLine(response.Sum);
					sr.Close();
					Response.Close();
				}				
			}
			catch (Exception e)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine(e.Message);
				Console.ForegroundColor = ConsoleColor.Gray;
				logger.Error(e.Message);
			}

		}
		#endregion

		#region Sub
		public void Sub(string Id_Tracking)
		{
			Reqsub.Nums = new int[] { };
			Console.WriteLine("------- Operation Subtract -------");
			Console.WriteLine("Write the operation( Ej: 1-2-3 )");
			separator = '-';
			try
			{
					Console.WriteLine("Operation:");
					op = Console.ReadLine();

					string[] numbers = op.Trim().Split(separator);
					Reqsub.Nums = numbers.Select(x => int.Parse(x)).ToArray();

				HttpWebRequest req = (HttpWebRequest)WebRequest.Create($"{url}sub");
				req.Method = "POST";
				req.ContentType = "application/json";

				if (Id_Tracking != "")
				{
					req.Headers.Add("X_Evi_Tracking_Id", Id_Tracking);
				}

				// Opens a stream,converts the request to Json and send it 
				using (StreamWriter sw = new StreamWriter(req.GetRequestStream()))
				{
					string jsonRequest = JsonConvert.SerializeObject(Reqsub);
					sw.Write(jsonRequest);
					sw.Close();
				}

				SubResponse response = new SubResponse();
				HttpWebResponse Response = (HttpWebResponse)req.GetResponse();
				using (StreamReader sr = new StreamReader(Response.GetResponseStream(), Encoding.UTF8))
				{
					Console.WriteLine("The server responds: ");
						response = JsonConvert.DeserializeObject<SubResponse>(sr.ReadToEnd());
						Console.WriteLine($"{response.Difference}");
					sr.Close();
					Response.Close();
				}
			}
			catch (Exception e)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine(e.Message);
				Console.ForegroundColor = ConsoleColor.Gray;
				logger.Error(e.Message);
			}
		}
		#endregion

		#region Mult
		public void Mult(string Id_Tracking)
		{

			Reqmult.Factors = new int[] { };
			Console.WriteLine("------- Operation Multiply -------");
			Console.WriteLine("Write the operation( Ej: 1*2*3 )");
			separator = '*';
			try
			{
					Console.WriteLine("Operation:");
					op = Console.ReadLine();

					string[] numbers = op.Trim().Split(separator);
					Reqmult.Factors = numbers.Select(x => int.Parse(x)).ToArray();

				HttpWebRequest req = (HttpWebRequest)WebRequest.Create($"{url}mult");
				req.Method = "POST";
				req.ContentType = "application/json";

				if (Id_Tracking != "")
				{
					req.Headers.Add("X_Evi_Tracking_Id", Id_Tracking);
				}

				// Opens a stream,converts the request to Json and send it 
				using (StreamWriter sw = new StreamWriter(req.GetRequestStream()))
				{
					string jsonRequest = JsonConvert.SerializeObject(Reqmult);
					sw.Write(jsonRequest);
					sw.Close();
				}

				MultResponse response = new MultResponse();
				HttpWebResponse Response = (HttpWebResponse)req.GetResponse();
				using (StreamReader sr = new StreamReader(Response.GetResponseStream(), Encoding.UTF8))
				{
					Console.WriteLine("The server responds: ");
						response = JsonConvert.DeserializeObject<MultResponse>(sr.ReadToEnd());
						Console.WriteLine($"{response.Product}");
					sr.Close();
					Response.Close();
				}
				
			}
			catch (Exception e)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine(e.Message);
				Console.ForegroundColor = ConsoleColor.Gray;
				logger.Error(e.Message);
			}
		}
		#endregion

		#region Div
		public void Div(string Id_Tracking)
		{
			Console.WriteLine("------- Operation Divide -------");
			separator = '/';
			try
			{
					Console.WriteLine("Dividend:");
					op = Console.ReadLine();
					Reqdiv.Dividend = int.Parse(op);

					Console.WriteLine("Divisor:");
					op = Console.ReadLine();
					Reqdiv.Divisor = int.Parse(op);

				HttpWebRequest req = (HttpWebRequest)WebRequest.Create($"{url}div");
				req.Method = "POST";
				req.ContentType = "application/json";

				if (Id_Tracking != "")
				{
					req.Headers.Add("X_Evi_Tracking_Id", Id_Tracking);
				}

				// Opens a stream,converts the request to Json and send it 
				using (StreamWriter sw = new StreamWriter(req.GetRequestStream()))
				{
					string jsonRequest = JsonConvert.SerializeObject(Reqdiv);
					sw.Write(jsonRequest);
					sw.Close();
				}

				DivResponse response = new DivResponse();
				HttpWebResponse Response = (HttpWebResponse)req.GetResponse();
				using (StreamReader sr = new StreamReader(Response.GetResponseStream(), Encoding.UTF8))
				{
					Console.WriteLine("The server responds: ");
						response = JsonConvert.DeserializeObject<DivResponse>(sr.ReadToEnd());
						Console.WriteLine($"Quotient: {response.Quotient}, Remainder: {response.Remainder}");
					sr.Close();
					Response.Close();
				}
			}
			catch (Exception e)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine(e.Message);
				Console.ForegroundColor = ConsoleColor.Gray;
				logger.Error(e.Message);
			}
		}
		#endregion

		#region Journal
		public void Journal(string Id_Tracking)
		{
			logger.Info(url);

			// Connects to the server and sends the request
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create($"{url}history");
			req.Method = "GET";
			req.ContentType = "application/json";
			req.Headers.Add("X_Evi_Tracking_Id", Id_Tracking);

			string history="",line="";
			char[] sep =new char[] {'|'};

			HttpWebResponse Response = (HttpWebResponse)req.GetResponse();

			using (StreamReader sr = new StreamReader(Response.GetResponseStream(), Encoding.UTF8))
			{
				line = sr.ReadLine();
				if (Id_Tracking != "")
				{
					while (line != null)
					{
					
						line = sr.ReadLine();
						if (line != "* * * * History of Operations * * * *" && line != null)
						{
							string id = line.Split(sep)[2].Trim();
							if (id == Id_Tracking)
							{
								history += line + "\n";
							}
						}
					}
				}else{
					Console.WriteLine("Not Tracking Id");
				}

				//history.Select(x => x.ToString().Split('>')[0]);
				sr.Close();
				Response.Close();
			}
			Console.WriteLine("* * * * History of Operations * * * *");
			Console.WriteLine(history);
		}
		#endregion
	}
}