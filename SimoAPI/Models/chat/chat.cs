using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Intrinsics.X86;

public class chat : entityBase
{

    [Description("کاربر شروع کننده")]
    public string phonenumber1 { get; set; } = "";

    [Description("مخاطب")]
    public string phonenumber2 { get; set; } = "";

    [Description("تاریخ گفتگو")]
    public string datetime { get; set; } = "";

    //unique validation
    public override void uniqueValidation(ref serviceresult result, apiContext context)
    {
        // if (context.chats.Where(x => x.chatname == this.chatname && x.id != this.id ).FirstOrDefault() != null)
        // {
        //     new errorProp() { 
        //         error = "نام کاربری تکراری است", 
        //         propname = nameof(this.chatname) 
        //     }.init(ref result);   
        // }
    }

    //required validation
    public override void requiredValidation(ref serviceresult result)
    {

    }

    //delete validation
    public override void deleteValidation(ref serviceresult result, apiContext context)
    {

    }
}