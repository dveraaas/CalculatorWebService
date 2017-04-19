using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using NLog;
using CalculatorService.server2.Models;

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
				if (Reqadd.Addends == null || Reqadd == null)
				{
					throw new Exception("Null Request");
				}

				foreach (int item in Reqadd.Addends)
				{
					response.Sum += item;
					operation += item + " + ";
				}

				operation = operation.Substring(0, operation.Length - 3);

				//Json = JsonConvert.SerializeObject(response.Operation+" = "+response.Total);
				//Json = response.Operation + " = " + response.Total;
				logger.Trace($"{operation} = {response.Sum }");

				if (Request.Headers != null)
						{
							string key = Request.Headers.GetValues("X_Evi_Tracking_Id").First().ToString();

								Journal journal = new Journal();
								Operations Operation = new Operations();
								Operation.operation = "Sum";
								Operation.Calculation = $"{operation} = {response.Sum}";
								Operation.Date = new DateTime();
								Operation.Key = key;

								journal.StoreOperation(Operation);
						}
				string json = JsonConvert.SerializeObject(response);
				return json;	
			}
			catch (Exception e)
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
			response.Difference = Reqsub.Nums[0];
			logger.Trace("----- Method Subtract -----");
			try
			{
				if (Reqsub.Nums == null || Reqsub == null)
				{
					throw new Exception("Null Request");
				}

				for (int i = 0; i < Reqsub.Nums.Length; i++)
				{
					total += Reqsub.Nums[i];
					operation += Reqsub.Nums[i] + " - ";
				}

				// Subtracts the first number and delete the last '-'
				total -= Reqsub.Nums[0];
				response.Difference -= total;

				operation = operation.Substring(0, operation.Length - 3);

				//Json = JsonConvert.SerializeObject(response.Operation+" = "+response.Total);
				//Json = response.Operation + " = " + response.Total;
				logger.Trace($"{operation} = {response.Difference}");

				if (Request.Headers != null)
				{
					string key = Request.Headers.GetValues("X_Evi_Tracking_Id").First().ToString();

						Journal journal = new Journal();
						Operations Operation = new Operations();
						Operation.operation = "Dif";
						Operation.Calculation = $"{operation} = {response.Difference}";
						Operation.Date = new DateTime();
						Operation.Key = key;

					journal.StoreOperation(Operation);
				}
				string json = JsonConvert.SerializeObject(response);
				return json;
			}
			catch (Exception e)
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
				if (Reqmult.Factors == null || Reqmult == null)
				{
					throw new Exception("Null Request");
				}
				response.Product = 1;
				foreach (int item in Reqmult.Factors)
				{
					response.Product *= item;
					operation += item + " * ";
				}

				operation = operation.Substring(0, operation.Length - 3);

				//Json = JsonConvert.SerializeObject(response.Operation+" = "+response.Total);
				//Json = response.Operation + " = " + response.Total;
				logger.Trace($"{operation} = {response.Product}");

				if (Request.Headers != null)
				{
					string key = Request.Headers.GetValues("X_Evi_Tracking_Id").First().ToString();

					Journal journal = new Journal();
					Operations Operation = new Operations();
					Operation.operation = "Mult";
					Operation.Calculation = $"{operation} = {response.Product}";
					Operation.Date = new DateTime();
					Operation.Key = key;

					journal.StoreOperation(Operation);
				}
				string json = JsonConvert.SerializeObject(response);
				return json;
			}
			catch (Exception e)
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
				if ((Reqdiv.Dividend == null || Reqdiv.Divisor == null) || Reqdiv == null)
				{
					throw new Exception("Null Request");
				}
				if (Reqdiv.Divisor == 0)
				{
					throw new Exception("The operation result is Infinite");
				}else
				{
					response.Quotient = Reqdiv.Dividend / Reqdiv.Divisor;
					response.Remainder = Reqdiv.Dividend % Reqdiv.Divisor;
					
				}
				operation = Reqdiv.Dividend + " / " + Reqdiv.Divisor;

				//Json = JsonConvert.SerializeObject(response.Operation+" = "+response.Total);
				//Json = response.Operation + " = " + response.Total;
				logger.Trace($"{operation} = {response.Quotient}(Reaminder: {response.Remainder})");

				if (Request.Headers != null)
				{
					string key = Request.Headers.GetValues("X_Evi_Tracking_Id").First().ToString();

					Journal journal = new Journal();
					Operations Operation = new Operations();
					Operation.operation = "Div";
					Operation.Calculation = $"{operation} = {response.Quotient}(Remainder: {response.Remainder})";
					Operation.Date = new DateTime();
					Operation.Key = key;

					journal.StoreOperation(Operation);
				}
				string json = JsonConvert.SerializeObject(response);
				return json;
			}
			catch (Exception e)
			{
				logger.Error(e.Message);
				return e.Message;
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
				string key = Request.Headers.GetValues("X_Evi_Tracking_Id").First().ToString();
				Journal journal = new Journal();
				history = journal.GetJournal();
				return history;
			}
			catch (Exception e)
			{
				logger.Error(e.Message);
				return e.Message;
			}
		}
		#endregion
	}
}