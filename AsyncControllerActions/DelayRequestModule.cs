using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AsyncControllerActions
{
    public class DelayRequestModule: IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += OnBeginRequestHandler;
        }

        private void OnBeginRequestHandler(object sender, EventArgs eventArgs)
        {
            var application = (HttpApplication) sender;
            string value = application.Request.QueryString["delayRequest"];
            if (!string.IsNullOrEmpty(value))
            {
                int delay;
                if (int.TryParse(value, out delay))
                {
                    Task.Delay(delay).Wait();
                }
            }
        }
        public void Dispose() { }
    }
}