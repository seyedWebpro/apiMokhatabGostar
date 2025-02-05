using allAPIs.SimoAPI.Models;
using Dapper;
using EFCore.AutomaticMigrations;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Migrations;


public partial class apiContext : DbContext
{

    #region dbsets
    //Simo
    public DbSet<user> users { get; set; }
    public DbSet<ticket> tickets { get; set; }
    public DbSet<ticketdetail> ticketdetails { get; set; }
    public DbSet<chatcontact> chatContacts { get; set; }
    public DbSet<chat> chats { get; set; }
    public DbSet<message> messages { get; set; }
    public DbSet<company> companys { get; set; }
    public DbSet<otp> otps { get; set; }
    public DbSet<PageTypeModel> pageTypes { get; set; }
    public DbSet<PageTypeCategoryModel> pageTypeCategories { get; set; }
    public DbSet<CampaignModel> campaigns { get; set; }
    public DbSet<PricePageModel> pricePages { get; set; }
    public DbSet<ChartModel> charts { get; set; }
    public DbSet<CampaignPage> CampaignPages { get; set; }
    public DbSet<PageVersion> pageVersions { get; set; }
    public DbSet<TagModel> Tags { get; set; }
    public DbSet<PageTag> PageTags { get; set; }
    public DbSet<PricePageTitleModel> pricePageTitles { get; set; }
    public DbSet<TelegramChannelModel> telegramChannels { get; set; }
    public DbSet<MusicSiteModel> musicSiteModels { get; set; }
    public DbSet<MusicSiteTarefeModel> musicSiteTarefeModels { get; set; }
    public DbSet<TarefeModel> tarefeModels { get; set; }
    public DbSet<TelegramChannelTariffModel> telegramChannelTariffModels { get; set; }
    public DbSet<FavoriteTelegramChannelModel> FavoriteTelegramChannels { get; set; }
    public DbSet<FavoriteMusicSiteModel> FavoriteMusicSiteModels { get; set; }
    public DbSet<CampaignChannel> campaignChannels { get; set; }
    public DbSet<CampaignMusicSite> campaignMusicSites { get; set; }
    public DbSet<ScreenshotModel> screenshotModels { get; set; }
    public DbSet<PagesModel> Pages { get; set; }
    public DbSet<PageTariffModel> PageTariffModels { get; set; }
    public DbSet<FavoritePagesModel> FavoritePagesModels { get; set; }
    public DbSet<CombinedTariffModel> CombinedTariffModels { get; set; }
    public DbSet<TelegramChannelCombinedTariffModel> TelegramChannelCombinedTariffModels { get; set; }

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // رابطه بین PagesModel و PageTypeCategoryModel
    modelBuilder.Entity<PagesModel>()
        .HasOne(p => p.PageTypeCategory)
        .WithMany(c => c.Pages)
        .HasForeignKey(p => p.PageTypeCategoryId)
        .OnDelete(DeleteBehavior.Restrict); // یا Cascade بسته به نیاز شما

    // رابطه بین PagesModel و PageTypeModel
    modelBuilder.Entity<PagesModel>()
        .HasOne(p => p.PageType)
        .WithMany(t => t.Pages)
        .HasForeignKey(p => p.PageTypeId)
        .OnDelete(DeleteBehavior.Restrict); // یا Cascade بسته به نیاز شما

    // تعریف کلید ترکیبی برای PageTariffModel
    modelBuilder.Entity<PageTariffModel>()
        .HasKey(pt => new { pt.PageId, pt.TariffId }); // کلید ترکیبی از PageId و TariffId

    // روابط بین PagesModel و PageTariffModel
    modelBuilder.Entity<PagesModel>()
        .HasMany(p => p.PageTariffModels)
        .WithOne(pt => pt.Pages)
        .HasForeignKey(pt => pt.PageId)
        .OnDelete(DeleteBehavior.Cascade);

    // روابط بین PagesModel و FavoritePagesModel
    modelBuilder.Entity<PagesModel>()
        .HasMany(p => p.FavoritePagesModels)
        .WithOne(fp => fp.Pages)
        .HasForeignKey(fp => fp.PageId)
        .OnDelete(DeleteBehavior.Cascade);

    // روابط بین TarefeModel و PageTariffModel
    modelBuilder.Entity<TarefeModel>()
        .HasMany(t => t.PageTariffModels)
        .WithOne(pt => pt.Tariff)
        .HasForeignKey(pt => pt.TariffId)
        .OnDelete(DeleteBehavior.Cascade);

    // روابط بین user و FavoritePagesModel
    modelBuilder.Entity<user>()
        .HasMany(u => u.FavoritePages)
        .WithOne(fp => fp.User)
        .HasForeignKey(fp => fp.UserId)
        .OnDelete(DeleteBehavior.Cascade);

    // تعریف کلید ترکیبی برای PageTag
    modelBuilder.Entity<PageTag>()
        .HasKey(pt => new { pt.PageId, pt.TagId });

    // روابط بین PageTag و PagesModel
    modelBuilder.Entity<PageTag>()
        .HasOne(pt => pt.Page)
        .WithMany(p => p.PageTags)
        .HasForeignKey(pt => pt.PageId);

    // روابط بین PageTag و TagModel
    modelBuilder.Entity<PageTag>()
        .HasOne(pt => pt.Tag)
        .WithMany(t => t.PageTags)
        .HasForeignKey(pt => pt.TagId);

    // تعریف کلید ترکیبی برای MusicSiteTarefeModel
    modelBuilder.Entity<MusicSiteTarefeModel>()
        .HasKey(mst => new { mst.MusicSiteId, mst.TariffId });

    // روابط بین MusicSiteTarefeModel و MusicSiteModel
    modelBuilder.Entity<MusicSiteTarefeModel>()
        .HasOne(mst => mst.MusicSite)
        .WithMany(ms => ms.MusicSiteTarefeModel)
        .HasForeignKey(mst => mst.MusicSiteId)
        .OnDelete(DeleteBehavior.Cascade); // حذف مرتبط: اگر سایت موزیک حذف شد، تعرفه‌های آن نیز حذف شوند.

    // روابط بین MusicSiteTarefeModel و TarefeModel
    modelBuilder.Entity<MusicSiteTarefeModel>()
        .HasOne(mst => mst.Tariff)
        .WithMany(t => t.MusicSiteTarefeModel)
        .HasForeignKey(mst => mst.TariffId)
        .OnDelete(DeleteBehavior.Restrict); // حذف غیرمرتبط: اگر تعرفه حذف شود، ارتباط‌ها باقی بمانند.

    // تنظیم مقدار پیش‌فرض برای Price در MusicSiteTarefeModel
    modelBuilder.Entity<MusicSiteTarefeModel>()
        .Property(mst => mst.Price)
        .HasDefaultValue(0m); // مقدار پیش‌فرض قیمت را صفر تعیین کنید.

    // تعریف کلید ترکیبی برای TelegramChannelTariffModel
    modelBuilder.Entity<TelegramChannelTariffModel>()
        .HasKey(tct => new { tct.TelegramChannelId, tct.TariffId });

    // روابط بین TelegramChannelTariffModel و TelegramChannelModel
    modelBuilder.Entity<TelegramChannelTariffModel>()
        .HasOne(tct => tct.TelegramChannel)
        .WithMany(tc => tc.TelegramChannelTariffModels)
        .HasForeignKey(tct => tct.TelegramChannelId)
        .OnDelete(DeleteBehavior.Cascade);

    // روابط بین TelegramChannelTariffModel و TarefeModel
    modelBuilder.Entity<TelegramChannelTariffModel>()
        .HasOne(tct => tct.Tariff)
        .WithMany(t => t.TelegramChannelTariffModels)
        .HasForeignKey(tct => tct.TariffId)
        .OnDelete(DeleteBehavior.Restrict);

    // تعریف کلید ترکیبی برای FavoriteTelegramChannelModel
    modelBuilder.Entity<FavoriteTelegramChannelModel>()
        .HasKey(f => new { f.UserId, f.TelegramChannelId }); // کلید ترکیبی

    // روابط بین FavoriteTelegramChannelModel و user
    modelBuilder.Entity<FavoriteTelegramChannelModel>()
        .HasOne(f => f.User)
        .WithMany(u => u.FavoriteTelegramChannels)
        .HasForeignKey(f => f.UserId)
        .OnDelete(DeleteBehavior.Cascade);

    // روابط بین FavoriteTelegramChannelModel و TelegramChannelModel
    modelBuilder.Entity<FavoriteTelegramChannelModel>()
        .HasOne(f => f.TelegramChannel)
        .WithMany(tc => tc.FavoriteTelegramChannels)
        .HasForeignKey(f => f.TelegramChannelId)
        .OnDelete(DeleteBehavior.Cascade); // حذف علاقه‌مندی‌ها در صورت حذف کانال

    // تعریف کلید ترکیبی برای FavoriteMusicSiteModel
    modelBuilder.Entity<FavoriteMusicSiteModel>()
        .HasKey(f => new { f.UserId, f.MusicSiteId });

    // روابط بین FavoriteMusicSiteModel و user
    modelBuilder.Entity<FavoriteMusicSiteModel>()
        .HasOne(f => f.User)
        .WithMany(u => u.FavoriteMusicSites)
        .HasForeignKey(f => f.UserId)
        .OnDelete(DeleteBehavior.Cascade);

    // روابط بین FavoriteMusicSiteModel و MusicSiteModel
    modelBuilder.Entity<FavoriteMusicSiteModel>()
        .HasOne(f => f.MusicSite)
        .WithMany(ms => ms.FavoriteMusicSites)
        .HasForeignKey(f => f.MusicSiteId)
        .OnDelete(DeleteBehavior.Cascade);

    // روابط بین CampaignModel و CampaignPage
    modelBuilder.Entity<CampaignModel>()
        .HasMany(c => c.CampaignPages)
        .WithOne(cp => cp.Campaign)
        .HasForeignKey(cp => cp.CampaignId);

    // روابط بین CampaignModel و CampaignChannel
    modelBuilder.Entity<CampaignModel>()
        .HasMany(c => c.CampaignChannels)
        .WithOne(cc => cc.Campaign)
        .HasForeignKey(cc => cc.CampaignId)
        .OnDelete(DeleteBehavior.Cascade);

    // روابط بین TelegramChannelModel و CampaignChannel
    modelBuilder.Entity<TelegramChannelModel>()
        .HasMany(tc => tc.CampaignChannels)
        .WithOne(cc => cc.Channel)
        .HasForeignKey(cc => cc.ChannelId)
        .OnDelete(DeleteBehavior.Cascade);

    // مقدار پیش‌فرض برای Platform در CampaignModel
    modelBuilder.Entity<CampaignModel>()
        .Property(c => c.Platform)
        .HasDefaultValue("Instagram");

    // روابط بین CampaignModel و CampaignMusicSites
    modelBuilder.Entity<CampaignModel>()
        .HasMany(c => c.CampaignMusicSites)
        .WithOne(cm => cm.Campaign)
        .HasForeignKey(cm => cm.CampaignId)
        .OnDelete(DeleteBehavior.Cascade);

    // روابط بین MusicSiteModel و CampaignMusicSites
    modelBuilder.Entity<MusicSiteModel>()
        .HasMany(ms => ms.CampaignMusicSites)
        .WithOne(cm => cm.MusicSite)
        .HasForeignKey(cm => cm.MusicSiteId)
        .OnDelete(DeleteBehavior.Cascade);

    // روابط بین ScreenshotModel و user
    modelBuilder.Entity<ScreenshotModel>()
        .HasOne(s => s.User)
        .WithMany()
        .HasForeignKey(s => s.UserId)
        .OnDelete(DeleteBehavior.Cascade); // حذف اسکرین‌شات‌ها در صورت حذف کاربر

    // تعریف کلید ترکیبی برای TelegramChannelCombinedTariffModel
    modelBuilder.Entity<TelegramChannelCombinedTariffModel>()
        .HasKey(tcct => new { tcct.TelegramChannelId, tcct.CombinedTariffId });

    // روابط بین TelegramChannelCombinedTariffModel و TelegramChannelModel
    modelBuilder.Entity<TelegramChannelCombinedTariffModel>()
        .HasOne(tcct => tcct.TelegramChannel)
        .WithMany(tc => tc.TelegramChannelCombinedTariffModels)
        .HasForeignKey(tcct => tcct.TelegramChannelId)
        .OnDelete(DeleteBehavior.Cascade);

    // روابط بین TelegramChannelCombinedTariffModel و CombinedTariffModel
    modelBuilder.Entity<TelegramChannelCombinedTariffModel>()
        .HasOne(tcct => tcct.CombinedTariff)
        .WithMany(ct => ct.TelegramChannelCombinedTariffModels)
        .HasForeignKey(tcct => tcct.CombinedTariffId)
        .OnDelete(DeleteBehavior.Restrict);
}
    #endregion

    #region ctors
    public apiContext(DbContextOptions options, IConfiguration config) : base(options)
    {
        Config = config;

        // Reset database schema
        // MigrateDatabaseToLatestVersion.Execute(this, new DbMigrationsOptions { ResetDatabaseSchema = false , AutomaticMigrationsEnabled = true });
        // this.Database.Migrate();

    }

    public IConfiguration Config { get; }
    public HttpContext httpContext { get; set; }
    public System.Data.Common.DbConnection Connection { get => this.Database.GetDbConnection(); }

    #endregion

    #region events

    public override int SaveChanges()
    {
        foreach (var entityEntry in ChangeTracker.Entries()) // Iterate all made changes
        {
            if (entityEntry.Entity is entityParent entity)
            {
                //remoteIp
                entity.rempoteIp = httpContext.Connection.RemoteIpAddress.ToString();

                //insert
                if (entityEntry.State == EntityState.Added) // If you want to update TenantId when Order is added
                {

                    //allow custom id
                    if (entity.allowId == 1)
                    {
                        if (this.Connection.State == System.Data.ConnectionState.Closed)
                            this.Connection.Open();

                        this.Connection.Query(@"SET IDENTITY_INSERT " + entity.GetType().Name + "s ON");
                    }

                    //set creator
                    entity.setCreator(httpContext);

                    var rs = base.SaveChanges();

                    if (entity.allowId == 1)
                        this.Connection.Query(@"SET IDENTITY_INSERT " + entity.GetType().Name + "s OFF");

                    return rs;

                }
                //update 
                else if (entityEntry.State == EntityState.Modified) // If you want to update TenantId when Order is modified
                {
                    entity.setEditor(httpContext);
                }
                //delet 
                else if (entityEntry.State == EntityState.Deleted) // If you want to update TenantId when Order is modified
                {

                }

            }
        }
        return base.SaveChanges();
    }

    #endregion


}

