using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Mvc;
using System.Web.Routing;
using Providers;

namespace Providers.Attributes
{
    public class LoginFirewallSet : System.Web.Mvc.ActionFilterAttribute
    {
        public int Minutes { get; set; }

        private readonly Cookie _cookie = new Cookie();
        private readonly Encryption _encryption = new Encryption();
        
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!_cookie.ExistCookie("login"))
            {
                var _enc = _encryption.Encrypt(filterContext.HttpContext.Request.UserHostAddress);
                _cookie.SetCookie("login", _enc, "min", Minutes);
            }
            
            base.OnActionExecuting(filterContext);
        }
    }
}
