using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Intrinsics.X86;

public class chatcontact : entityBase
{

    [Description("نام")]
    public string fname { get; set; } = "";

    [Description("نام خانوادگی")]
    public string lname { get; set; } = "";

    [Description("شماره تماس")]
    public string phonenumber { get; set; }

    [Description("انسداد مخاطب")]
    public bool isblock { get; set; }

    //unique validation
    public override void uniqueValidation(ref serviceresult result, apiContext context)
    {
        // if (context.chatcontacts.Where(x => x.chatcontactname == this.chatcontactname && x.id != this.id ).FirstOrDefault() != null)
        // {
        //     new errorProp() { 
        //         error = "نام کاربری تکراری است", 
        //         propname = nameof(this.chatcontactname) 
        //     }.init(ref result);   
        // }
    }

    //required validation
    public override void requiredValidation(ref serviceresult result)
    {
        if (string.IsNullOrEmpty(this.phonenumber))
        {
            new errorProp()
            {
                error = "شماره تلفن را وارد کنید",
                propname = nameof(this.phonenumber)
            }.init(ref result);
        }

    }

    //delete validation
    public override void deleteValidation(ref serviceresult result, apiContext context)
    {

    }
}