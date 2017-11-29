using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Mvc;
using System.Web.Routing;
using Providers;
using System.ComponentModel.DataAnnotations;

namespace Providers.Attributes
{
    public class Xss : ValidationAttribute
    {
        private Security _security = new Security();

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                var Txt = _security.Xss(value.ToString());

                validationContext
                    .ObjectType
                    .GetProperty(validationContext.MemberName)
                    .SetValue(validationContext.ObjectInstance, Txt, null);

                if (String.IsNullOrEmpty(Txt))
                    return new ValidationResult("???");
                else
                    return ValidationResult.Success;
            }
            catch (System.Exception)
            {
                return null;
            }
        }
    }
}
