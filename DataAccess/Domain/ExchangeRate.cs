using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Domain
{
    public class ExchangeRate
    {
        [Key]
        public DateTime Timestamp { get; set; }

        [Key]
        public string CurrencyId { get; set; }

        public string CurrencyName { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.#########}")]
        public decimal Rate { get; set; }
    }
}