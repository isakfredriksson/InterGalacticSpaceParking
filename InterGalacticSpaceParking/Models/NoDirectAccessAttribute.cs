using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Routing;
//Av Isak Fredriksson
namespace InterGalacticSpaceParking.Controllers
{
	/// <summary>
	/// Custom action filter that creates an attribute that restricts the user from directly accessing a view from browser.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public class NoDirectAccessAttribute : ActionFilterAttribute
	{
		/// <summary>
		/// Action filter that executes if the action "ThankYou" or "Payment" gets executed. 
		/// </summary>
		/// <param name="filterContext"></param>
		public override void OnActionExecuted(ActionExecutedContext filterContext) 
		{
			var canAccess = false; 
			var referer = filterContext.HttpContext.Request.Headers["Referer"].ToString(); 
			if (!string.IsNullOrEmpty(referer))											   
			{
				var rUri = new System.UriBuilder(referer).Uri;
				var req = filterContext.HttpContext.Request;
				if(req.Host.Host == rUri.Host && req.Host.Port == rUri.Port && req.Scheme == rUri.Scheme) 
				{
					canAccess = true; //Tillåt åtkomst till sidan.
				}
			}
			if (!canAccess)
			{
				filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Parking", action = "Index", area = "" })); 
			}
		}
	}
}