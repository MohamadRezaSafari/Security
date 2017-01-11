using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace MohammadReza
{
    public class Security
    {
        private string HtmlTag = "<.*?>";

        public string RemoveTag(string str)
        {
            return Regex.Replace(str, HtmlTag, string.Empty); ;
        }
    }
}