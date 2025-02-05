using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class company : entityBase
{

    [Description("نام شرکت")]
    public string companyname { get; set; }

    [Description("مدیران شرکت")]
    public string? companyAdmins { get; set; }

    public override void uniqueValidation(ref serviceresult result, apiContext context)
    {

    }

    public override void requiredValidation(ref serviceresult result)
    {

    }

    public override void deleteValidation(ref serviceresult result, apiContext context)
    {

    }

}
