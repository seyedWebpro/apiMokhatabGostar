using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Intrinsics.X86;

public class message : entityBase
{

     [Description("پیام")]
    public string messagechat { get; set; } = "";

    [Description("ایجاد کننده")]
    public string authorname { get; set; } = "";

    [Description("تاریخ ایجاد")]
    public string dateisnew { get; set; } = "";

    [Description("فایل پیوست")]
    public string attachchat { get; set; } = "";

    [Description("نوع پیوست")]
    public string attachchat_Type { get; set; } = "";

    [Description("نام پیوست")]
    public string attachchat_Name { get; set; } = "";

    [ForeignKey("chat")]
    public long? chatid { get; set; }
    public virtual chat? chat { get; set; }


    //unique validation
    public override void uniqueValidation(ref serviceresult result, apiContext context)
    {
        // if (context.messages.Where(x => x.messagename == this.messagename && x.id != this.id ).FirstOrDefault() != null)
        // {
        //     new errorProp() { 
        //         error = "نام کاربری تکراری است", 
        //         propname = nameof(this.messagename) 
        //     }.init(ref result);   
        // }
    }

    //required validation
    public override void requiredValidation(ref serviceresult result)
    {
        if (string.IsNullOrEmpty(this.messagechat))
        {
            new errorProp()
            {
                error = "پیغام را وارد کنید",
                propname = nameof(this.messagechat)
            }.init(ref result);
        }

    }

    //delete validation
    public override void deleteValidation(ref serviceresult result, apiContext context)
    {

    }
}