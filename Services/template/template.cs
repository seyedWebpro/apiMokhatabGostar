using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

//props
public partial class template : entityBase
{

    [Description("نام و نام خانوادگی")]
    public string? fullname { get; set; }

    [Description("کد ملی")]
    public string? nationalCode { get; set; }

    [Description("کاربر مرتبط")]
    public long? myuserId { get; set; } 
    public virtual user? myuser { get; set; }

}

//valdations
public partial class template
{

    //required Validation
    public override void requiredValidation(ref serviceresult result)
    {

        //string fild valdetion
        if (string.IsNullOrEmpty(this.fullname))
        {
            new errorProp() { 
                error = "نام را وارد کنید", 
                propname = nameof(this.fullname) 
            }.init(ref result);           
        }

        //string fild valdetion
        if (string.IsNullOrEmpty(this.nationalCode))
        {
            new errorProp() { 
                error = "کد ملی را وارد کنید", 
                propname = nameof(this.nationalCode) 
            }.init(ref result);           
        }

        
    }

    //unique validation
    public override void uniqueValidation(ref serviceresult result, apiContext context)
    {
    
        if (context.templates.Where(x => x.nationalCode == this.nationalCode && x.id != this.id ).FirstOrDefault() != null)
        {
            new errorProp() { 
                error = "کد ملی تکراری است", 
                propname = nameof(this.nationalCode) 
            }.init(ref result);   
        }


    }

    //delete validation
    public override void deleteValidation(ref serviceresult result, apiContext context)
    {
    

    }

}

//context
public partial class apiContext
{
    public DbSet<template> templates { get; set; }

}