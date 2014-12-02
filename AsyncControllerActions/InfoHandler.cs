using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;

namespace AsyncControllerActions
{
    public class InfoHandler: IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            var content = new StringBuilder();

            NameValueCollection variables = context.Request.ServerVariables;
            foreach (var key in variables.AllKeys)
            {
                content.AppendFormat(@"
<tr>
    <td>{0}</td>
    <td>{1}</td>
</tr>
                ", HttpUtility.HtmlEncode(key), HttpUtility.HtmlEncode(variables[key]));
            }

            var response = context.Response;
            response.ContentType = "text/html";
            response.Write(String.Format(@"
<!DOCTYPE html>
<html>
    <body>
        <table>
        <tr>
            <th>Name</th>
            <th>Value</th>
        </tr>
        {0}
        </table>
    </body>
</html>
            ", content));

        }

        public bool IsReusable { get { return false; } }
    }
}