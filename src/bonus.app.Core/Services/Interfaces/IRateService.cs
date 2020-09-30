using System.Collections.Generic;
using System.Threading.Tasks;
using bonus.app.Core.Models;

namespace bonus.app.Core.Services.Interfaces
{
	public interface IRateService
	{
		string PaySuccessUrl
		{
			get;
		}

		string PayErrorUrl
		{
			get;
		}

		Task<List<Rate>> GetRates();

		Task<Rate> GetMyRate();

		Task<bool> ChangeRate(Rate rate);

		Task<string> GetHtmlPayment();
	}
}
