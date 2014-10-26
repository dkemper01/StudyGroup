using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess.Domain;

namespace AsyncControllerActions.Models
{
    public class RateViewModel
    {
        public IEnumerable<SelectListItem> Timestamps { get; set; }
        public IEnumerable<ExchangeRate> Rates { get; set; } 
    }
}