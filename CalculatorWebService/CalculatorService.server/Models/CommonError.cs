using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalculatorService.server2.Models
{
	public class CommonError
	{
		public string ErrorCode { get; set; }
		public int ErrorStatus { get; set; }
		public string ErrorMessage { get; set; }
	}
}