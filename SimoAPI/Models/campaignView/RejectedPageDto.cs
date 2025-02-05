using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace allAPIs.SimoAPI.Models.campaignView
{
    public class RejectedPageDto
    {
        public int Id { get; set; }
        public string PageId { get; set; }
        public bool IsAccepted { get; set; }
    }
}