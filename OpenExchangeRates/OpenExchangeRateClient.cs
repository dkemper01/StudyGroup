using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OpenExchangeRates
{
    public class OpenExchangeRateClient : IDisposable
    {
        private readonly string _appId;
        private HttpClient _client;

        private const string ApiKeyRequiredMessage = "An API key is required to use the Open Exchange Rates API. You can signup for a free API key at https://openexchangerates.org/signup.";

        public OpenExchangeRateClient() : this(null) { }

        public OpenExchangeRateClient(string appId)
        {
            _appId = appId ?? ConfigurationManager.AppSettings["OpenExchangeRatesApiKey"];
            if (string.IsNullOrEmpty(_appId))
            {
                throw new ArgumentException(ApiKeyRequiredMessage, "appId");
            }

            _client = new HttpClient()
            {
                BaseAddress = new Uri("http://openexchangerates.org/api")
            };
        }

        public async Task<ReadOnlyDictionary<string, string>> GetCurrencies()
        {
            var response = await _client.GetAsync("currencies.json");
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();

            var obj = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            return new ReadOnlyDictionary<string, string>(obj);
        }

        public async Task<FxRates> GetLatest()
        {
            var url = "latest.json?app_id={0}";
            var response = await _client.GetAsync(string.Format(url, _appId));
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<FxRates>(json);

            return obj;
        }

        public async Task<FxRates> GetHistorical(DateTime date)
        {

            var url = string.Format("historical/{0}.json?app_id={1}", date.ToString("yyyy-MM-dd"), _appId);
            var response = await _client.GetAsync(string.Format(url, _appId));
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<FxRates>(json);

            return obj;
        }

        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            try
            {
                if (disposing)
                {
                    _client.Dispose();
                    _client = null;
                }
            }
            finally
            {
                _disposed = true;
            }
        }
    }
}
