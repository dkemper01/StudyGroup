using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AsyncControllerActions.Infrastructure
{
    public class MeasureElapsedAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            filterContext.Controller.ViewBag.Stopwatch = Stopwatch.StartNew();
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            filterContext.Controller.ViewBag.Stopwatch.Stop();
        }
    }
}