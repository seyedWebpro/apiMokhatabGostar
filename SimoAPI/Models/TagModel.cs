using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace allAPIs.SimoAPI.Models
{
    public class TagModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "لطفا نام تگ را وارد کنید.")]
        public string Name { get; set; } = "";

        // لیست صفحات مرتبط با این تگ
        public virtual ICollection<PageTag> PageTags { get; set; } = new List<PageTag>();
    }
}