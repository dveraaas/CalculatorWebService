using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using NLog;
using CalculatorService.server2;
using CalculatorService.server2.Models;
using CalculatorService.server2.Controllers;

namespace CalculatorService.server2.Controllers
{
	public class CalculatorController : Controller
	{
		// GET: Calculator
		private static Logger logger = LogManager.GetCurrentClassLogger();

		#region /calculator2/add
		[HttpPost]
		[ActionName("add")]
		public string Add(AddRequest Reqadd)
		{
			AddResponse response = new AddResponse();
			string operation = "";
			logger.Trace("----- Method Add -----");
			try
			{
				if (Reqadd == null || Reqadd.Addends == null)
				{
					throw new ArgumentNullException();
				}

				foreach (int item in Reqadd.Addends)
				{
					response.Sum += item;
					operation +=$"{item} + ";
				}

				operation = operation.Substring(0, operation.Length - 3);

				logger.Trace($"{operation} = {response.Sum }");

				if(Request.Headers.GetValues("X_Evi_Tracking_Id") != null && Request.Headers.GetValues("X_Evi_Tracking_Id").Any()) { 

						string key = Request.Headers.GetValues("X_Evi_Tracking_Id").First();

						JournalService.StoreOperation(CreateOperation("Sum", $"{operation} = {response.Sum}", DateTime.Now, key));
				}

				string json = JsonConvert.SerializeObject(response);
				return json;	
			}
			catch (ArgumentNullException e)
			{
				logger.Error(e.Message);
				return e.Message;
			}
		}
		#endregion

		#region /calculator2/sub
		[HttpPost]
		[ActionName("sub")]
		public string Sub(SubRequest Reqsub)
		{
			SubResponse response = new SubResponse();
			string operation = "";
			int total = 0;
			
			logger.Trace("----- Method Subtract -----");
			try
			{
				if (Reqsub == null || Reqsub.Nums == null)
				{
					throw new ArgumentNullException();
				}
				response.Difference = Reqsub.Nums[0];

				for (int i = 1; i < Reqsub.Nums.Length; i++)
				{
					response.Difference -= Reqsub.Nums[i];
					operation += $"{Reqsub.Nums[i]} - ";
				}

				// Subtracts the first number and delete the last '-'
				total -= Reqsub.Nums[0];
				operation = $"{response.Difference} - {operation}";

				operation = operation.Substring(0, operation.Length - 3);

				logger.Trace($"{operation} = {response.Difference}");

				if (Request.Headers.GetValues("X_Evi_Tracking_Id") != null && Request.Headers.GetValues("X_Evi_Tracking_Id").Any())
				{

					string key = Request.Headers.GetValues("X_Evi_Tracking_Id").First();

					JournalService.StoreOperation(CreateOperation("Dif", $"{operation} = {response.Difference}", DateTime.Now, key));
				}

				string json = JsonConvert.SerializeObject(response);
				return json;
			}
			catch (ArgumentNullException e)
			{
				logger.Error(e.Message);
				return e.Message;
			}
		}
		#endregion
		
		#region /calculator2/mult
		[HttpPost]
		[ActionName("mult")]
		public string Mult(MultRequest Reqmult)
		{
			MultResponse response = new MultResponse();
			string operation = "";
			logger.Trace("----- Method Multiply -----");
			try
			{
				if (Reqmult == null || Reqmult.Factors == null)
				{
					throw new ArgumentNullException();
				}
				//response.Product = 1;
				response.Product = Reqmult.Factors.Aggregate(1, (a,b) => a * b);

				foreach (int item in Reqmult.Factors)
				{
					//response.Product *= item;
					operation += $"{item} * ";
				}

				operation = operation.Substring(0, operation.Length - 3);

				logger.Trace($"{operation} = {response.Product}");

				if (Request.Headers.GetValues("X_Evi_Tracking_Id") != null && Request.Headers.GetValues("X_Evi_Tracking_Id").Any())
				{
					string key = Request.Headers.GetValues("X_Evi_Tracking_Id").First();

					JournalService.StoreOperation(CreateOperation("Mul", $"{operation} = {response.Product}", DateTime.Now, key));
				}

				string json = JsonConvert.SerializeObject(response);
				return json;
			}
			catch (ArgumentNullException e)
			{
				logger.Error(e.Message);
				return e.Message;
			}
		}
		#endregion
		
		#region /calculator2/div
		[HttpPost]
		[ActionName("div")]
		public string Div(DivRequest Reqdiv)
		{
			DivResponse response = new DivResponse();
			string operation = "";
			logger.Trace("----- Method Divide -----");
			try
			{
				if (Reqdiv == null || !(Reqdiv.Dividend.HasValue || Reqdiv.Divisor.HasValue))
				{
					throw new ArgumentNullException();
				}
				if (Reqdiv.Divisor == 0)
				{
					throw new DivideByZeroException();
					//throw new Exception("The operation result is Infinite");
				}
				else
				{
					response.Quotient = Reqdiv.Dividend.Value / Reqdiv.Divisor.Value;
					response.Remainder = Reqdiv.Dividend.Value % Reqdiv.Divisor.Value;
				}

				operation = $"{Reqdiv.Dividend} / {Reqdiv.Divisor}";

				logger.Trace($"{operation} = {response.Quotient}(Reaminder: {response.Remainder})");

				if (Request.Headers.GetValues("X_Evi_Tracking_Id") != null && Request.Headers.GetValues("X_Evi_Tracking_Id").Any())
				{
					string key = Request.Headers.GetValues("X_Evi_Tracking_Id").First();

					JournalService.StoreOperation(CreateOperation("Div", $"{operation} = {response.Quotient}(Remainder: {response.Remainder})", DateTime.Now, key));
				}

				string json = JsonConvert.SerializeObject(response);
				return json;
			}
			catch (ArgumentNullException e)
			{
				logger.Error(e.Message);
				return e.Message;
			}
			catch (DivideByZeroException ex)
			{
				logger.Error(ex.Message);
				return ex.Message;
			}
		}
		#endregion

		#region Journal
		[HttpGet]
		[ActionName("history")]
		public string History()
		{
			string history = "";
			try
			{
				string key = Request.Headers.GetValues("X_Evi_Tracking_Id").FirstOrDefault();
				history = JournalService.GetJournal();

				return history;
			}
			catch (Exception e)
			{
				logger.Error(e.Message);
				return e.Message;
			}
		}
		#endregion

		/* Adicional Method */
		#region CreateOperation

		public Operations CreateOperation(string method, string op, DateTime date, string key)
		{
			Operations Operation = new Operations();
			Operation.operation = method;
			Operation.Calculation = op;
			Operation.Date = date;
			Operation.Key = key;

			return Operation;

		}
		#endregion

	}
}