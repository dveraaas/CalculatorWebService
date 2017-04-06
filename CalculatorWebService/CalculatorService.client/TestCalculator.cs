using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
using CalculatorService.client.Models;
using NLog;

namespace CalculatorService.client
{
	public class TestCalculator
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();

		public void test()
		{
			// Creates Parameter List 
			List<int> numbers = new List<int> { 10, 5, 3 };

			Console.WriteLine("---- We are testing all Methods ----");
			logger.Info("---- We are testing all Methods ----");
			logger.Info("Numbers: "+numbers);

			// Calls to connect with diferents url
			Connect(numbers, 1, "http://localhost:52147/calculator/add");
			Connect(numbers, 1, "http://localhost:52147/calculator/sub");
			Connect(numbers, 1, "http://localhost:52147/calculator/mult");
			Connect(numbers, 2, "http://localhost:52147/calculator/div");
			Connect(numbers, 3, "http://localhost:52147/calculator/sqr");

		}

		public void Connect(List<int> numbers, int op,string url)
		{
			string jsonRequest="";

			logger.Info(url);

			// Connects to the server and sends the request
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
			req.Method = "POST";
			req.ContentType = "application/json";

			using (StreamWriter sw = new StreamWriter(req.GetRequestStream()))
			{
				switch (op)
				{
					case 1:
						Request request = new Request();
						request.nums = numbers;
						jsonRequest = JsonConvert.SerializeObject(request);
						break;
					case 2:
						DivRequest divRequest = new DivRequest();
						divRequest.num1 = numbers[0];
						divRequest.num2 = numbers[1];
						jsonRequest = JsonConvert.SerializeObject(divRequest);
						break;
					case 3:
						SqrRequest sqrRequest = new SqrRequest();
						sqrRequest.num = numbers[0];
						jsonRequest = JsonConvert.SerializeObject(sqrRequest);
						break;
				}

				logger.Info(jsonRequest);

				sw.Write(jsonRequest);
				sw.Close();
			}

			// Gets the response and deseralize the Json to Class response
			Response response = new Response();
			HttpWebResponse Response = (HttpWebResponse)req.GetResponse();
			using (StreamReader sr = new StreamReader(Response.GetResponseStream(), Encoding.UTF8))
			{
				response = JsonConvert.DeserializeObject<Response>(sr.ReadToEnd());
				sr.Close();
				Response.Close();
			}

			Console.WriteLine("The server responds: ");
			Console.WriteLine($"{response.operation} = {response.Total}");

			logger.Info($"The server responds: {response.operation} = {response.Total}");
			logger.Info("-------------------------------------------------------------");
		}
	}
}
