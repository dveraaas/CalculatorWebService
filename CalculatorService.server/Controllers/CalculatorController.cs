using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CalculatorService.server.Models;
using Newtonsoft.Json;
using NLog;

namespace CalculatorService.server.Controllers
{
    public class CalculatorController : Controller
    {
		private static Logger logger = LogManager.GetCurrentClassLogger();
		
		// GET: Calculator
		#region /calculator2/add
		[HttpPost]
		[ActionName("add")]
		public string Add(Request request)
		{
			logger.Trace("----- Method Add -----");

			Response response = new Response();
			response.Total = 0;

			// If on the List are more than 1 parameter
			if (request.nums.Count > 1)
			{
				foreach (int item in request.nums)
				{
					response.Total += item;
					response.operation += item + " + ";
				}

				// Delete the last '+' on the response.operation
				response.operation = response.operation.Substring(0, response.operation.Length - 3);
			}else
			{
				response.Total = int.Parse(response.operation);
			}

			logger.Trace(response.operation+" = "+response.Total);

			string Json = JsonConvert.SerializeObject(response);
			return Json;
		}
		#endregion
		#region /calculator2/sub
		[HttpPost]
		[ActionName("sub")]
		public string Substract(Request request)
		{
			logger.Trace("----- Method Subtract -----");

			Response response = new Response();
			int total = 0;
			response.Total = request.nums[0];

			// If on the List are more than 1 parameter
			if (request.nums.Count > 1)
			{
				// Sums all the parameters
				for (int i = 0; i < request.nums.Count; i++)
				{
					total += request.nums[i];
					response.operation += request.nums[i] + " - ";
				}

				// Subtracts the first number and delete the last '-'
				total -= request.nums[0];
				response.Total -= total;
				response.operation = response.operation.Substring(0, response.operation.Length - 3);
			}

			logger.Trace(response.operation + " = " + response.Total);

			string Json = JsonConvert.SerializeObject(response);
			return Json;
		}
		#endregion
		#region /calculator2/mult
		[HttpPost]
		[ActionName("mult")]
		public string Multiply(Request request)
		{
			logger.Trace("----- Method Multiply -----");

			Response response = new Response();
			response.Total = 1;

			// Multiplys the numbers of the request
			foreach (int item in request.nums)
			{
				response.Total *= item;
				response.operation += item + " * ";
			}
			// Delete the last '*'
			response.operation = response.operation.Substring(0, response.operation.Length - 3);

			logger.Trace(response.operation + " = " + response.Total);

			string Json = JsonConvert.SerializeObject(response);
			return Json;
		}
		#endregion
		#region /calculator2/div
		[HttpPost]
		[ActionName("div")]
		public string Divide(DivRequest request)
		{
			logger.Trace("----- Method Divide -----");

			// Divides the two numbers and print the rest of the divide inside of response.operation
			Response response = new Response();
			response.Total = 0;
			response.Total = request.num1 / request.num2;
			response.operation = $"{request.num1} / {request.num2} (Rest: {request.num1%request.num2})";

			logger.Trace(response.operation + " = " + response.Total);

			string Json = JsonConvert.SerializeObject(response);
			return Json;
		}
		#endregion
		#region /calculator2/sqr
		[HttpPost]
		[ActionName("sqr")]
		public string Square(SqrRequest request)
		{
			logger.Trace("Method Square");

			Response response = new Response();
			response.Total = 0;
			response.Total = request.num * request.num;
			response.operation = $"({request.num})e2";

			logger.Trace(response.operation + " = " + response.Total);

			string Json = JsonConvert.SerializeObject(response);
			return Json;
		}
		#endregion
	}
}