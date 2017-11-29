using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Mvc;
using System.Web.Routing;
using MohamadReza;

namespace Providers.Attributes
{
    public class LoginFirewall : System.Web.Mvc.ActionFilterAttribute
    {
        private readonly Cookie _cookie = new Cookie();
        private readonly Encryption _encryption = new Encryption();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (_cookie.ExistCookie("login"))
            {
                bool _valid =  Validate(_cookie.GetCookie("login"), filterContext.HttpContext.Request.UserHostAddress);
                if (String.IsNullOrEmpty(_cookie.GetCookie("login")) || _valid == false)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Error" }));
                }
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Error" }));
            }

            base.OnActionExecuting(filterContext);
        }


        private bool Validate(string cookie, string ip)
        {
            try
            {
                var _enc = _encryption.Decrypt(cookie);
                return (_enc == ip) ? true : false;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
