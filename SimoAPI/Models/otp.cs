using System.ComponentModel.DataAnnotations;
using System.Runtime.Intrinsics.X86;
using System.Text.RegularExpressions;

public class otp : entityBase
{

    public string phone_number { get; set; }

    public string verify_code { get; set; }

    public int status { get; set; }


    public override void uniqueValidation(ref serviceresult result, apiContext context)
    {

    }

    public override void requiredValidation(ref serviceresult result)
    {

        //string fild valdetion
        if (string.IsNullOrEmpty(this.phone_number))
        {
            new errorProp() { 
                error = "شماره همراه خود را وارد کنید", 
                propname = nameof(this.phone_number) 
            }.init(ref result);           
        }
        
        Regex PhoneNumberRegex = new Regex(@"^(\+98?)?{?(0?9[0-9]{9,9}}?)$");
        if (!PhoneNumberRegex.IsMatch(this.phone_number))
        {
            new errorProp() { 
                error = "شماره همراه وارد شده قابل قبول نیست", 
                propname = nameof(this.phone_number) 
            }.init(ref result);
        }
    }

    public override void deleteValidation(ref serviceresult result, apiContext context)
    {

    }

}