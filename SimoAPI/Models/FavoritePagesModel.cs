using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace allAPIs.SimoAPI.Models
{
    public class FavoritePagesModel
    {
        public int Id { get; set; }
        public long UserId { get; set; } // نوع داده مطابق با user.id
        public int PageId { get; set; }
        public user User { get; set; }
        public PagesModel Pages  { get; set; }
    }
}