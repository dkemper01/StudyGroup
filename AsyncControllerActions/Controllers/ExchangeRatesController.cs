using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AsyncControllerActions.Models;
using DataAccess;
using DataAccess.Domain;
using OpenExchangeRates;

namespace AsyncControllerActions.Controllers
{
    public class ExchangeRatesController : Controller
    {
        public async Task<ActionResult> Import()
        {
            using (var client = new OpenExchangeRateClient())
            {
                Task<FxRates> fxRates = client.GetLatest();
                Task<ReadOnlyDictionary<string, string>> currencies = client.GetCurrencies();
                await Task.WhenAll(fxRates, currencies);

                using (var db = new DatabaseContext())
                {
                    //var timestamp = fxRates.Timestamp;
                    var timestamp = DateTime.UtcNow;

                    var rates = fxRates.Result.Rates.Select(pair => new ExchangeRate
                    {
                        Timestamp = timestamp,
                        CurrencyId = pair.Key,
                        CurrencyName = currencies.Result[pair.Key],
                        Rate = pair.Value
                    }).ToList();

                    db.ExchangeRates.AddRange(rates);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult Index(DateTime? timestamp = null)
        {
            using (var db = new DatabaseContext())
            {
                var timestamps = db.ExchangeRates
                    .Select(rate => rate.Timestamp)
                    .Distinct()
                    .OrderByDescending(ts => ts)
                    .ToList();

                if (!timestamps.Any())
                {
                    return RedirectToAction("Import");
                }


                timestamp = timestamp ?? timestamps.First();

                var rates = db.ExchangeRates.Where(rate => rate.Timestamp == timestamp.Value).ToList();
                var vm = new RateViewModel
                {
                    Timestamps = timestamps.Select(ts => new SelectListItem
                    {
                        Selected = ts == timestamp,
                        Text = ts.ToLocalTime().ToString(),
                        Value = ts.ToString("yyyy-MM-dd HH:mm:ss.fff")
                    }),
                    Rates = rates
                };

                return View(vm);
            }
        }

        public ActionResult Detail(string id)
        {
            using (var db = new DatabaseContext())
            {
                var rates = db.ExchangeRates
                    .Where(rate => rate.CurrencyId == id)
                    .OrderByDescending(rate => rate.Timestamp)
                    .ToList();

                return View(rates);
            }
        }

        public void Watch(string id)
        {
            Session["CurrencyId"] = id;           
        }

        public PartialViewResult WatchSummary(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return PartialView("_WatchSummary", null);
            }

            using (var db = new DatabaseContext())
            {
                var rate = db.ExchangeRates
                    .Where(fx => fx.CurrencyId == id)
                    .OrderByDescending(fx => fx.Timestamp)
                    .FirstOrDefault();

                return PartialView("_WatchSummary", rate);
            }            
        }
    }
}