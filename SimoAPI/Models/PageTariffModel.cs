using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace allAPIs.SimoAPI.Models
{
    public class PageTariffModel
    {
         public int PageId { get; set; }
        public PagesModel Pages { get; set; }

        public int TariffId { get; set; }
        public TarefeModel Tariff { get; set; }

        public decimal Price { get; set; } 
    }
}