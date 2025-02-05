
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace allAPIs.SimoAPI.Models.campaignView
{
    public class AddPriceToCampView
    {
        [Required(ErrorMessage = "ای دی کمپین را وارد کنید")]
        public int campaignId {get; set;}
        public string pricePageIds {get; set;}
        public string PageID {get; set;}
    }
}