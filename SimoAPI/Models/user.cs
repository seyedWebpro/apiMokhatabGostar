using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Intrinsics.X86;
using allAPIs.SimoAPI.Models;

public class user : entityBase{
    public string username { get; set; } = "";
    public string password { get; set; } = "";
    [NotMapped]
    public string password2 { get; set; } = "";
    public string? roles { get; set; } = "";
    public string pic { get; set; } = "";
    public string displayName { get; set; } = "";   
    public int? CampaignId {get; set;}
    public string? PhoneNumber {get ; set;}

    public ICollection<FavoriteTelegramChannelModel> FavoriteTelegramChannels { get; set; }

    public ICollection<FavoriteMusicSiteModel> FavoriteMusicSites { get; set; }
    public ICollection<FavoritePagesModel> FavoritePages { get; set; }



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
        if (string.IsNullOrEmpty(this.username))
        {
            new errorProp() { 
                error = "نام کاربری را وارد کنید", 
                propname = nameof(this.username) 
            }.init(ref result);           
        }

        if (string.IsNullOrEmpty(this.password))
        {
            new errorProp() { 
                error = "کلمه عبور را وارد کنید", 
                propname = nameof(this.password) 
            }.init(ref result);           
        }
    }

    //delete validation
    public override void deleteValidation(ref serviceresult result, apiContext context)
    {

    }
}