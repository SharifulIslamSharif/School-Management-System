using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace School_Management_System.Custom_helper
{
    public class Custom_helper_class
    {
        public static IHtmlString Label(string content)
        {
            string value = String.Format("{0}", content);
            return new HtmlString(value);
        }
    }
}