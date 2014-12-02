using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using OpenExchangeRates;

namespace AsyncControllerActions
{
    public class CurrencyHandler : HttpTaskAsyncHandler
    {
        public override async Task ProcessRequestAsync(HttpContext context)
        {
            using (var client = new OpenExchangeRateClient())
            {
                var currencies = await client.GetCurrencies();

                string json = JsonConvert.SerializeObject(currencies);

                var response = context.Response;
                response.ContentType = "application/json";
                response.Write(json);
            }
        }
    }
}