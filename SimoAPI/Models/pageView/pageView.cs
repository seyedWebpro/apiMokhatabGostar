using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace allAPIs.SimoAPI.Models.pageView
{
    public class pageView
    {
        [Required(ErrorMessage = "لطفا آی دی پیج مورد نظر را وارد کنید .")]
        public string PageId { get; set; }
        [Required(ErrorMessage = "لطفا جنسیت پیج مورد نظر را وارد کنید.")]

        public string Sex { get; set; }
        [Required(ErrorMessage = "لطفا توضیحات پیج مورد نظر را وارد کنید.")]

        public string Description {get; set;}
        [Required(ErrorMessage = "لطفا آی دی نوع پیج مورد نظر را انتخاب کنید.")]

        public int PageTypeId { get; set; }
        [Required(ErrorMessage = "لطفا آی دی دسته بندی نوع پیج مورد نظر را انتخاب کنید.")]

        public int PageCategoryId { get; set; }           

        public string? PersianName { get; set; }
        public string? TelegramID { get; set; }
        public string? WhatsappNumber { get; set; }

        public int? CampaignId {get; set;}


    }
}