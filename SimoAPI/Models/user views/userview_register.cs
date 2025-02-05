    public class userview_register : entityBase
    {
        
    public string Username { get; set; }
    public string Password { get; set; }
    public string Password2 { get; set; }
    public string Roles { get; set; } 
    public DateTime? createTime { get; set; }

    public DateTime? editeTime { get; set; }

    public long? createdBy { get; set; }

    public long? editedBy { get; set; }

    public string? rempoteIp { get; set; }

    public string? setion { get; set; }

  //unique validation
    public override void uniqueValidation(ref serviceresult result, apiContext context)
    {
        // if (context.users.Where(x => x.username == this.username && x.id != this.id ).FirstOrDefault() != null)
        // {
        //     new errorProp() { 
        //         error = "نام کاربری تکراری است", 
        //         propname = nameof(this.username) 
        //     }.init(ref result);   
        // }
    }

    //required validation
    public override void requiredValidation(ref serviceresult result)
    {
        if (string.IsNullOrEmpty(this.Username))
        {
            new errorProp() { 
                error = "نام کاربری را وارد کنید", 
                propname = nameof(this.Username) 
            }.init(ref result);           
        }

        if (string.IsNullOrEmpty(this.Password))
        {
            new errorProp() { 
                error = "کلمه عبور را وارد کنید", 
                propname = nameof(this.Password) 
            }.init(ref result);           
        }
    }

    //delete validation
    public override void deleteValidation(ref serviceresult result, apiContext context)
    {

    }
}
