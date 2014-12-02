using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AsyncControllerActions
{
    /// <summary>
    /// Summary description for HelloWorld
    /// </summary>
    public class HelloWorld : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}