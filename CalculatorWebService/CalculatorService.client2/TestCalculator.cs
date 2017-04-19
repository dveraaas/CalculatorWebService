using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
using CalculatorService.client2.Models;
using NLog;

namespace CalculatorService.client2
{
	public class TestCalculator
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();

		public void test()
		{
			// Creates Parameter List 
			int[] numbers = new int[] { 10, 5, 3 };

			Console.WriteLine("---- We are testing all Methods ----");
			logger.Info("---- We are testing all Methods ----");

			// Calls to connect with diferents url
			Add(numbers, "http://localhost:53459/calculator/add");
			Add(new int[]{ }, "http://localhost:53459/calculator/add");
			Sub(numbers, "http://localhost:53459/calculator/sub");
			Mult(numbers, "http://localhost:53459/calculator/mult");
			Div(numbers, "http://localhost:53459/calculator/div");
			Div(new int[] {10, 0 }, "http://localhost:53459/calculator/div");
			Div(new int[] { }, "http://localhost:53459/calculator/div");
			GetHistory("http://localhost:53459/calculator/history");
		}
		#region Add
		public void Add(int[] numbers, string url)
		{
			string jsonRequest = "";
			Console.WriteLine($"Operacion: {string.Join("+", numbers)}");

			logger.Info(url);

			// Connects to the server and sends the request
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
			req.Method = "POST";
			req.ContentType = "application/json";
			req.Headers.Add("X_Evi_Tracking_Id", "Test");

			using (StreamWriter sw = new StreamWriter(req.GetRequestStream()))
			{
				AddRequest add = new AddRequest();
				add.Addends = numbers;
				jsonRequest = JsonConvert.SerializeObject(add);
				sw.WriteLine(jsonRequest);
				sw.Close();
			}
			// Get the response
			string resp;
			AddResponse response = new AddResponse();
			HttpWebResponse Response = (HttpWebResponse)req.GetResponse();

			using (StreamReader sr = new StreamReader(Response.GetResponseStream(), Encoding.UTF8))
			{
				//response = JsonConvert.DeserializeObject<AddResponse>(sr.ReadToEnd());
				resp = sr.ReadToEnd();
				sr.Close();
				Response.Close();
			}

				Console.WriteLine("The server responds: ");
				Console.WriteLine(resp);

				logger.Info($"The server responds: {resp}");
				logger.Info("-------------------------------------------------------------");
		}
		#endregion
		#region Sub
		public void Sub(int[] numbers, string url)
		{
			string jsonRequest = "";
			Console.WriteLine($"Operacion: {string.Join("-", numbers)}");

			logger.Info(url);

			// Connects to the server and sends the request
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
			req.Method = "POST";
			req.ContentType = "application/json";
			req.Headers.Add("X_Evi_Tracking_Id", "Test");

			using (StreamWriter sw = new StreamWriter(req.GetRequestStream()))
			{
				SubRequest sub = new SubRequest();
				sub.Nums = numbers;
				jsonRequest = JsonConvert.SerializeObject(sub);
				sw.WriteLine(jsonRequest);
				sw.Close();
			}
			// Get the response
			string resp;
			HttpWebResponse Response = (HttpWebResponse)req.GetResponse();

			using (StreamReader sr = new StreamReader(Response.GetResponseStream(), Encoding.UTF8))
			{
				//response = JsonConvert.DeserializeObject<AddResponse>(sr.ReadToEnd());
				resp = sr.ReadToEnd();
				sr.Close();
				Response.Close();
			}

			Console.WriteLine("The server responds: ");
			Console.WriteLine(resp);
			//Console.WriteLine($"{response.Operation} = {response.Total}");

			logger.Info($"The server responds: {resp}");
			logger.Info("-------------------------------------------------------------");
		}
		#endregion
		#region Mult
		public void Mult(int[] numbers, string url)
		{
			string jsonRequest = "";
			Console.WriteLine($"Operacion: {string.Join("*", numbers)}");
			logger.Info(url);

			// Connects to the server and sends the request
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
			req.Method = "POST";
			req.ContentType = "application/json";
			req.Headers.Add("X_Evi_Tracking_Id", "Test");

			using (StreamWriter sw = new StreamWriter(req.GetRequestStream()))
			{
				MultRequest mult = new MultRequest();
				mult.Factors = numbers;
				jsonRequest = JsonConvert.SerializeObject(mult);
				sw.WriteLine(jsonRequest);
				sw.Close();
			}
			// Get the response
			string resp;
			HttpWebResponse Response = (HttpWebResponse)req.GetResponse();

			using (StreamReader sr = new StreamReader(Response.GetResponseStream(), Encoding.UTF8))
			{
				//response = JsonConvert.DeserializeObject<AddResponse>(sr.ReadToEnd());
				resp = sr.ReadToEnd();
				sr.Close();
				Response.Close();
			}

			Console.WriteLine("The server responds: ");
			Console.WriteLine(resp);
			//Console.WriteLine($"{response.Operation} = {response.Total}");

			logger.Info($"The server responds: {resp}");
			logger.Info("-------------------------------------------------------------");
		}
		#endregion
		#region Div
		public void Div(int[] numbers, string url)
		{
			string jsonRequest = "";
			if (numbers.Length <= 2)
			{
				Console.WriteLine($"Operacion: {string.Join("/", numbers)}");
			}
			else
			{
				Console.WriteLine($"Operacion: {string.Join("/", numbers).Substring(0, string.Join("/", numbers).Length - 2)}");
			}
			logger.Info(url);
			if (numbers.Length == 0)
			{
				Console.WriteLine("The request not have values");
			}
			else
			{
				// Connects to the server and sends the request
				HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
				req.Method = "POST";
				req.ContentType = "application/json";
				req.Headers.Add("X_Evi_Tracking_Id", "Test");

				using (StreamWriter sw = new StreamWriter(req.GetRequestStream()))
				{
					DivRequest div = new DivRequest();
					div.Dividend = numbers[0];
					div.Divisor = numbers[1];
					jsonRequest = JsonConvert.SerializeObject(div);
					sw.WriteLine(jsonRequest);
					sw.Close();
				}
				// Get the response
				string resp;
				HttpWebResponse Response = (HttpWebResponse)req.GetResponse();

				using (StreamReader sr = new StreamReader(Response.GetResponseStream(), Encoding.UTF8))
				{
					//response = JsonConvert.DeserializeObject<AddResponse>(sr.ReadToEnd());
					resp = sr.ReadToEnd();
					sr.Close();
					Response.Close();
				}

				Console.WriteLine("The server responds: ");
				Console.WriteLine(resp);
				//Console.WriteLine($"{response.Operation} = {response.Total}");

				logger.Info($"The server responds: {resp}");
				logger.Info("-------------------------------------------------------------");
			}
		}
		#endregion

		#region History

		public void GetHistory(string url)
		{
			logger.Info(url);

			// Connects to the server and sends the request
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
			req.Method = "GET";
			req.ContentType = "application/json";
			req.Headers.Add("X_Evi_Tracking_Id", "Test");

			string history;
			HttpWebResponse Response = (HttpWebResponse)req.GetResponse();

			using (StreamReader sr = new StreamReader(Response.GetResponseStream(), Encoding.UTF8))
			{
				history = sr.ReadToEnd();
				sr.Close();
				Response.Close();
			}

			Console.WriteLine(history);
		}
		#endregion
	}
}
