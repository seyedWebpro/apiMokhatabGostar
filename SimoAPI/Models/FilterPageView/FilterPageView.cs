using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace allAPIs.SimoAPI.Models.FilterPageView
{
    public class FilterPageView
    {  
        public int? PricePageName { get; set; }
        public string? normalPrice { get; set; } 
        public string? hamkarPrice { get; set; }
        public string? specialPrice {get ; set;}
        public int? pageTypeID {get ; set;}
        public int? PageCategoryId {get ; set;}
    
    } 
}
