using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Domain
{
    public class ExchangeRate
    {
        public Guid Id { get; set; }

        public DateTime Timestamp { get; set; }

        [MaxLength(3)]
        public string CurrencyId { get; set; }

        public string CurrencyName { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.#########}")]
        public decimal Rate { get; set; }
    }
}