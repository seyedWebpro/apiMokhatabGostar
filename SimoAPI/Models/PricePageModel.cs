using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace allAPIs.SimoAPI.Models
{
    public class PricePageModel
    {

        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "نام تعرفه را وارد کنید !")]
        public string Name { get; set; }
        public int? Normalprice { get; set; }
        public int? HamkarPrice { get; set; }
        public int? SpecialPrice { get; set; }

    }
}