using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace allAPIs.SimoAPI.Models.InstagramView
{
    public class AssignTariffInstagramPageDto
    {
         public int InstagramPageId { get; set; }
        public int TariffId { get; set; }
        public decimal Price { get; set; }
    }
}