using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Intrinsics.X86;

public class ticketdetail : entityBase
{
    [Description("پیام")]
    public string? messageticket { get; set; } = "";

    [Description("ایجاد کننده")]
    public string? authorname { get; set; } = "";

    [Description("تاریخ ایجاد")]
    public string? dateisnew { get; set; } = "";

    [Description("فایل پیوست")]
    public string? attachticket { get; set; } = "";

    [Description("نوع پیوست")]
    public string? attachticket_Type { get; set; } = "";

    [Description("نام پیوست")]
    public string? attachticket_Name { get; set; } = "";

    [Description("سین کردن")]
    public bool? seen { get; set; } 

    [ForeignKey("ticket")]
    public long? tiketid { get; set; }
    public virtual ticket? ticket { get; set; }

    //unique validation
    public override void uniqueValidation(ref serviceresult result, apiContext context)
    {
        // if (context.ticketdetails.Where(x => x.ticketdetailname == this.ticketdetailname && x.id != this.id ).FirstOrDefault() != null)
        // {
        //     new errorProp() { 
        //         error = "نام کاربری تکراری است", 
        //         propname = nameof(this.ticketdetailname) 
        //     }.init(ref result);   
        // }
    }

    //required validation
    public override void requiredValidation(ref serviceresult result)
    {
        if (string.IsNullOrEmpty(this.messageticket) && string.IsNullOrEmpty(this.attachticket))
        {
            new errorProp()
            {
                error = "پیام را وارد کنید",
                propname = nameof(this.messageticket)
            }.init(ref result);
        }


        if (string.IsNullOrEmpty(this.dateisnew))
        {
            new errorProp()
            {
                error = "تاریخ تیکت را وارد کنید",
                propname = nameof(this.dateisnew)
            }.init(ref result);
        }
    }

    //delete validation
    public override void deleteValidation(ref serviceresult result, apiContext context)
    {

    }
}