using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookStore.web.Filters
{
    public class CheckSessionAttribute : ActionFilterAttribute
    {
        private readonly string _sessionKey;

        public CheckSessionAttribute(string sessionKey)
        {
            _sessionKey = sessionKey;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var sessionValue = context.HttpContext.Session.GetString(_sessionKey);
            if (string.IsNullOrEmpty(sessionValue)) 
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    Controller = "Account",
                    Action = "Login"
                }));
            }
            base.OnActionExecuting(context);

        }
    }
}
