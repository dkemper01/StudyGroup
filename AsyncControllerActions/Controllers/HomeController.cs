using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using AsyncControllerActions.Infrastructure;
using AsyncControllerActions.Models;

namespace AsyncControllerActions.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [MeasureElapsed]
        public ActionResult Sync()
        {
            var client = new HttpClient();
            var model = new List<RequestStatistics>()
            {
                RequestStatistics.Create(client.GetAsync("http://www.example.com/asdf").Result),
                RequestStatistics.Create(client.GetAsync("http://www.microsoft.com").Result),
                RequestStatistics.Create(client.GetAsync("http://www.yahoo.com").Result),
                RequestStatistics.Create(client.GetAsync("http://www.google.com").Result),
                RequestStatistics.Create(client.GetAsync("http://www.msn.com").Result),
                RequestStatistics.Create(client.GetAsync("http://www.thomsonreuters.com").Result),
                RequestStatistics.Create(client.GetAsync("http://www.serengetilaw.com").Result)
            };

            ViewBag.SyncOrAsync = "Synchronous";

            return View("CommonRequestStatistics", model);
        }

        [MeasureElapsed]
        public async Task<ActionResult> Async()
        {
            var client = new HttpClient();

            var responses = await Task.WhenAll(
                client.GetAsync("http://www.example.com/asdf"),
                client.GetAsync("http://www.microsoft.com"),
                client.GetAsync("http://www.yahoo.com"),
                client.GetAsync("http://www.google.com"),
                client.GetAsync("http://www.msn.com")
            );
            var model = responses.Select(RequestStatistics.Create).ToList();

            ViewBag.SyncOrAsync = "Asynchronous";

            return View("CommonRequestStatistics", model);
        }
    }
}