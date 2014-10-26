using System.Collections.Generic;

namespace OpenExchangeRates
{
    public class FxRates
    {
        public string Disclaimer { get; set; }
        public string License { get; set; }
        public string Base { get; set; }
        public long Timestamp { get; set; }
        public Dictionary<string, decimal> Rates { get; set; }
    }
}