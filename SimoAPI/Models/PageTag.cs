using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace allAPIs.SimoAPI.Models
{
    public class PageTag
    {
        public int PageId { get; set; }
        public PagesModel Page { get; set; }

        public int TagId { get; set; }
        public TagModel Tag { get; set; }
    }
}