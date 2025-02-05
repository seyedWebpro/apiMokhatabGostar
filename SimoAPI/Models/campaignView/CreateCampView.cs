using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace allAPIs.SimoAPI.Models.campaignView
{
    public class CreateCampView
    {
        public string? Name {get; set;}= "" ;
        public DateTime? StartDate {get; set;}

        public long? UserId {get; set;}
        public string? PricePageId {get; set;} = "";

        public string? PageId {get; set;}
    }
}