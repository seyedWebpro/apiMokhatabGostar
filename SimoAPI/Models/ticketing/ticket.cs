using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Intrinsics.X86;

public class ticket : entityBase{

    [Description("موضوع")]
    public string subject { get; set; } = "";

    [Description("شماره تیکت")]
    public int? ticketnumber { get; set; } = 0;

    [Description("تاریخ تیکت")]
    public string? dateticket { get; set; } = "";

    [Description("وضعیت")]
    public int state { get; set; } = 0 ;

    [Description("ایجاد کننده")]
    public string? creatorname { get; set; }

    [Description("شماره تماس")]
    public string? phonenumber { get; set; }

    //unique validation
    public override void uniqueValidation(ref serviceresult result, apiContext context)
    {
        // if (context.tickets.Where(x => x.ticketname == this.ticketname && x.id != this.id ).FirstOrDefault() != null)
        // {
        //     new errorProp() { 
        //         error = "نام کاربری تکراری است", 
        //         propname = nameof(this.ticketname) 
        //     }.init(ref result);   
        // }
    }

    //required validation
    public override void requiredValidation(ref serviceresult result)
    {
        if (string.IsNullOrEmpty(this.subject))
        {
            new errorProp() { 
                error = "موضوع را وارد کنید", 
                propname = nameof(this.subject) 
            }.init(ref result);           
        }

        if (this.ticketnumber  == 0)
        {
            new errorProp() { 
                error = "شماره تیکت را وارد کنید", 
                propname = nameof(this.ticketnumber) 
            }.init(ref result);           
        }

        
        if (string.IsNullOrEmpty(this.dateticket))
        {
            new errorProp() { 
                error = "تاریخ تیکت را وارد کنید", 
                propname = nameof(this.dateticket) 
            }.init(ref result);           
        }
    }

    //delete validation
    public override void deleteValidation(ref serviceresult result, apiContext context)
    {

    }
}