using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "campaigns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    PricePageId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Platform = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "Instagram")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_campaigns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "charts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vertical = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Horezntial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CampaignId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_charts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "chatContacts",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phonenumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isblock = table.Column<bool>(type: "bit", nullable: false),
                    createTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    editeTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdBy = table.Column<long>(type: "bigint", nullable: true),
                    editedBy = table.Column<long>(type: "bigint", nullable: true),
                    rempoteIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    setion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    allowId = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chatContacts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "chats",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    phonenumber1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phonenumber2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    datetime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    editeTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdBy = table.Column<long>(type: "bigint", nullable: true),
                    editedBy = table.Column<long>(type: "bigint", nullable: true),
                    rempoteIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    setion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    allowId = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chats", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "CombinedTariffModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TariffNames = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CombinedTariffModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "companys",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    companyname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    companyAdmins = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    editeTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdBy = table.Column<long>(type: "bigint", nullable: true),
                    editedBy = table.Column<long>(type: "bigint", nullable: true),
                    rempoteIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    setion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    allowId = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_companys", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "musicSiteModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiteName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SiteTopic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SiteAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SiteIcon = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_musicSiteModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "otps",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    phone_number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    verify_code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    createTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    editeTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdBy = table.Column<long>(type: "bigint", nullable: true),
                    editedBy = table.Column<long>(type: "bigint", nullable: true),
                    rempoteIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    setion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    allowId = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_otps", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pageTypeCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pageTypeCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "pageTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pageTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "pricePages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Normalprice = table.Column<int>(type: "int", nullable: true),
                    HamkarPrice = table.Column<int>(type: "int", nullable: true),
                    SpecialPrice = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pricePages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "pricePageTitles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pricePageTitles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tarefeModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tarefeModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "telegramChannels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Topic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChannelId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManagerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubscribersCount = table.Column<int>(type: "int", nullable: false),
                    PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_telegramChannels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tickets",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ticketnumber = table.Column<int>(type: "int", nullable: true),
                    dateticket = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    state = table.Column<int>(type: "int", nullable: false),
                    creatorname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phonenumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    editeTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdBy = table.Column<long>(type: "bigint", nullable: true),
                    editedBy = table.Column<long>(type: "bigint", nullable: true),
                    rempoteIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    setion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    allowId = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tickets", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    roles = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    pic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    displayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CampaignId = table.Column<int>(type: "int", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    editeTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdBy = table.Column<long>(type: "bigint", nullable: true),
                    editedBy = table.Column<long>(type: "bigint", nullable: true),
                    rempoteIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    setion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    allowId = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "messages",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    messagechat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    authorname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dateisnew = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    attachchat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    attachchat_Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    attachchat_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    chatid = table.Column<long>(type: "bigint", nullable: true),
                    createTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    editeTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdBy = table.Column<long>(type: "bigint", nullable: true),
                    editedBy = table.Column<long>(type: "bigint", nullable: true),
                    rempoteIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    setion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    allowId = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_messages", x => x.id);
                    table.ForeignKey(
                        name: "FK_messages_chats_chatid",
                        column: x => x.chatid,
                        principalTable: "chats",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "campaignMusicSites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CampaignId = table.Column<int>(type: "int", nullable: false),
                    MusicSiteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_campaignMusicSites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_campaignMusicSites_campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_campaignMusicSites_musicSiteModels_MusicSiteId",
                        column: x => x.MusicSiteId,
                        principalTable: "musicSiteModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PageId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelegramID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhatsappNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sex = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShowName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersianName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Followesrs = table.Column<int>(type: "int", nullable: true),
                    Following = table.Column<int>(type: "int", nullable: true),
                    ImgUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostViews = table.Column<int>(type: "int", nullable: true),
                    PostLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostLikes = table.Column<int>(type: "int", nullable: true),
                    postComments = table.Column<int>(type: "int", nullable: true),
                    PostImpertion = table.Column<int>(type: "int", nullable: true),
                    StoryViews = table.Column<int>(type: "int", nullable: true),
                    StoryImpertion = table.Column<int>(type: "int", nullable: true),
                    Engagement = table.Column<double>(type: "float", nullable: true),
                    PageTypeCategoryId = table.Column<int>(type: "int", nullable: true),
                    PageTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pages_pageTypeCategories_PageTypeCategoryId",
                        column: x => x.PageTypeCategoryId,
                        principalTable: "pageTypeCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pages_pageTypes_PageTypeId",
                        column: x => x.PageTypeId,
                        principalTable: "pageTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "musicSiteTarefeModels",
                columns: table => new
                {
                    MusicSiteId = table.Column<int>(type: "int", nullable: false),
                    TariffId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_musicSiteTarefeModels", x => new { x.MusicSiteId, x.TariffId });
                    table.ForeignKey(
                        name: "FK_musicSiteTarefeModels_musicSiteModels_MusicSiteId",
                        column: x => x.MusicSiteId,
                        principalTable: "musicSiteModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_musicSiteTarefeModels_tarefeModels_TariffId",
                        column: x => x.TariffId,
                        principalTable: "tarefeModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "campaignChannels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CampaignId = table.Column<int>(type: "int", nullable: false),
                    ChannelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_campaignChannels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_campaignChannels_campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_campaignChannels_telegramChannels_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "telegramChannels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TelegramChannelCombinedTariffModels",
                columns: table => new
                {
                    TelegramChannelId = table.Column<int>(type: "int", nullable: false),
                    CombinedTariffId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramChannelCombinedTariffModels", x => new { x.TelegramChannelId, x.CombinedTariffId });
                    table.ForeignKey(
                        name: "FK_TelegramChannelCombinedTariffModels_CombinedTariffModels_CombinedTariffId",
                        column: x => x.CombinedTariffId,
                        principalTable: "CombinedTariffModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramChannelCombinedTariffModels_telegramChannels_TelegramChannelId",
                        column: x => x.TelegramChannelId,
                        principalTable: "telegramChannels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "telegramChannelTariffModels",
                columns: table => new
                {
                    TelegramChannelId = table.Column<int>(type: "int", nullable: false),
                    TariffId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_telegramChannelTariffModels", x => new { x.TelegramChannelId, x.TariffId });
                    table.ForeignKey(
                        name: "FK_telegramChannelTariffModels_tarefeModels_TariffId",
                        column: x => x.TariffId,
                        principalTable: "tarefeModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_telegramChannelTariffModels_telegramChannels_TelegramChannelId",
                        column: x => x.TelegramChannelId,
                        principalTable: "telegramChannels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ticketdetails",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    messageticket = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    authorname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dateisnew = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    attachticket = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    attachticket_Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    attachticket_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    seen = table.Column<bool>(type: "bit", nullable: true),
                    tiketid = table.Column<long>(type: "bigint", nullable: true),
                    createTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    editeTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdBy = table.Column<long>(type: "bigint", nullable: true),
                    editedBy = table.Column<long>(type: "bigint", nullable: true),
                    rempoteIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    setion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    allowId = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ticketdetails", x => x.id);
                    table.ForeignKey(
                        name: "FK_ticketdetails_tickets_tiketid",
                        column: x => x.tiketid,
                        principalTable: "tickets",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "FavoriteMusicSiteModels",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    MusicSiteId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteMusicSiteModels", x => new { x.UserId, x.MusicSiteId });
                    table.ForeignKey(
                        name: "FK_FavoriteMusicSiteModels_musicSiteModels_MusicSiteId",
                        column: x => x.MusicSiteId,
                        principalTable: "musicSiteModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoriteMusicSiteModels_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteTelegramChannels",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    TelegramChannelId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteTelegramChannels", x => new { x.UserId, x.TelegramChannelId });
                    table.ForeignKey(
                        name: "FK_FavoriteTelegramChannels_telegramChannels_TelegramChannelId",
                        column: x => x.TelegramChannelId,
                        principalTable: "telegramChannels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoriteTelegramChannels_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "templates",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fullname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nationalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    myuserId = table.Column<long>(type: "bigint", nullable: true),
                    createTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    editeTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdBy = table.Column<long>(type: "bigint", nullable: true),
                    editedBy = table.Column<long>(type: "bigint", nullable: true),
                    rempoteIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    setion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    allowId = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_templates", x => x.id);
                    table.ForeignKey(
                        name: "FK_templates_users_myuserId",
                        column: x => x.myuserId,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "CampaignPages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CampaignId = table.Column<int>(type: "int", nullable: false),
                    PageId = table.Column<int>(type: "int", nullable: false),
                    TariffId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignPages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CampaignPages_campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CampaignPages_Pages_PageId",
                        column: x => x.PageId,
                        principalTable: "Pages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CampaignPages_tarefeModels_TariffId",
                        column: x => x.TariffId,
                        principalTable: "tarefeModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FavoritePagesModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    PageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoritePagesModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoritePagesModels_Pages_PageId",
                        column: x => x.PageId,
                        principalTable: "Pages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoritePagesModels_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PageTags",
                columns: table => new
                {
                    PageId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageTags", x => new { x.PageId, x.TagId });
                    table.ForeignKey(
                        name: "FK_PageTags_Pages_PageId",
                        column: x => x.PageId,
                        principalTable: "Pages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PageTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PageTariffModels",
                columns: table => new
                {
                    PageId = table.Column<int>(type: "int", nullable: false),
                    TariffId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageTariffModels", x => new { x.PageId, x.TariffId });
                    table.ForeignKey(
                        name: "FK_PageTariffModels_Pages_PageId",
                        column: x => x.PageId,
                        principalTable: "Pages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PageTariffModels_tarefeModels_TariffId",
                        column: x => x.TariffId,
                        principalTable: "tarefeModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pageVersions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PageId = table.Column<int>(type: "int", nullable: false),
                    sex = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShowName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersianName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CampaignId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostLikes = table.Column<int>(type: "int", nullable: true),
                    PostLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    postComments = table.Column<int>(type: "int", nullable: true),
                    PostViews = table.Column<int>(type: "int", nullable: true),
                    PostImpertion = table.Column<int>(type: "int", nullable: true),
                    StoryViews = table.Column<int>(type: "int", nullable: true),
                    StoryImpertion = table.Column<int>(type: "int", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsAccepted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pageVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_pageVersions_campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "campaigns",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_pageVersions_Pages_PageId",
                        column: x => x.PageId,
                        principalTable: "Pages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "screenshotModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScreenshotUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CampaignId = table.Column<int>(type: "int", nullable: false),
                    ChannelId = table.Column<int>(type: "int", nullable: true),
                    MusicSiteId = table.Column<int>(type: "int", nullable: true),
                    InstagramPageId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_screenshotModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_screenshotModels_campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_screenshotModels_musicSiteModels_MusicSiteId",
                        column: x => x.MusicSiteId,
                        principalTable: "musicSiteModels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_screenshotModels_Pages_InstagramPageId",
                        column: x => x.InstagramPageId,
                        principalTable: "Pages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_screenshotModels_telegramChannels_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "telegramChannels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_screenshotModels_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_campaignChannels_CampaignId",
                table: "campaignChannels",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_campaignChannels_ChannelId",
                table: "campaignChannels",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_campaignMusicSites_CampaignId",
                table: "campaignMusicSites",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_campaignMusicSites_MusicSiteId",
                table: "campaignMusicSites",
                column: "MusicSiteId");

            migrationBuilder.CreateIndex(
                name: "IX_CampaignPages_CampaignId",
                table: "CampaignPages",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_CampaignPages_PageId",
                table: "CampaignPages",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_CampaignPages_TariffId",
                table: "CampaignPages",
                column: "TariffId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteMusicSiteModels_MusicSiteId",
                table: "FavoriteMusicSiteModels",
                column: "MusicSiteId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoritePagesModels_PageId",
                table: "FavoritePagesModels",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoritePagesModels_UserId",
                table: "FavoritePagesModels",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteTelegramChannels_TelegramChannelId",
                table: "FavoriteTelegramChannels",
                column: "TelegramChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_messages_chatid",
                table: "messages",
                column: "chatid");

            migrationBuilder.CreateIndex(
                name: "IX_musicSiteTarefeModels_TariffId",
                table: "musicSiteTarefeModels",
                column: "TariffId");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_PageTypeCategoryId",
                table: "Pages",
                column: "PageTypeCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_PageTypeId",
                table: "Pages",
                column: "PageTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PageTags_TagId",
                table: "PageTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_PageTariffModels_TariffId",
                table: "PageTariffModels",
                column: "TariffId");

            migrationBuilder.CreateIndex(
                name: "IX_pageVersions_CampaignId",
                table: "pageVersions",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_pageVersions_PageId",
                table: "pageVersions",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_screenshotModels_CampaignId",
                table: "screenshotModels",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_screenshotModels_ChannelId",
                table: "screenshotModels",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_screenshotModels_InstagramPageId",
                table: "screenshotModels",
                column: "InstagramPageId");

            migrationBuilder.CreateIndex(
                name: "IX_screenshotModels_MusicSiteId",
                table: "screenshotModels",
                column: "MusicSiteId");

            migrationBuilder.CreateIndex(
                name: "IX_screenshotModels_UserId",
                table: "screenshotModels",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramChannelCombinedTariffModels_CombinedTariffId",
                table: "TelegramChannelCombinedTariffModels",
                column: "CombinedTariffId");

            migrationBuilder.CreateIndex(
                name: "IX_telegramChannelTariffModels_TariffId",
                table: "telegramChannelTariffModels",
                column: "TariffId");

            migrationBuilder.CreateIndex(
                name: "IX_templates_myuserId",
                table: "templates",
                column: "myuserId");

            migrationBuilder.CreateIndex(
                name: "IX_ticketdetails_tiketid",
                table: "ticketdetails",
                column: "tiketid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "campaignChannels");

            migrationBuilder.DropTable(
                name: "campaignMusicSites");

            migrationBuilder.DropTable(
                name: "CampaignPages");

            migrationBuilder.DropTable(
                name: "charts");

            migrationBuilder.DropTable(
                name: "chatContacts");

            migrationBuilder.DropTable(
                name: "companys");

            migrationBuilder.DropTable(
                name: "FavoriteMusicSiteModels");

            migrationBuilder.DropTable(
                name: "FavoritePagesModels");

            migrationBuilder.DropTable(
                name: "FavoriteTelegramChannels");

            migrationBuilder.DropTable(
                name: "messages");

            migrationBuilder.DropTable(
                name: "musicSiteTarefeModels");

            migrationBuilder.DropTable(
                name: "otps");

            migrationBuilder.DropTable(
                name: "PageTags");

            migrationBuilder.DropTable(
                name: "PageTariffModels");

            migrationBuilder.DropTable(
                name: "pageVersions");

            migrationBuilder.DropTable(
                name: "pricePages");

            migrationBuilder.DropTable(
                name: "pricePageTitles");

            migrationBuilder.DropTable(
                name: "screenshotModels");

            migrationBuilder.DropTable(
                name: "TelegramChannelCombinedTariffModels");

            migrationBuilder.DropTable(
                name: "telegramChannelTariffModels");

            migrationBuilder.DropTable(
                name: "templates");

            migrationBuilder.DropTable(
                name: "ticketdetails");

            migrationBuilder.DropTable(
                name: "chats");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "campaigns");

            migrationBuilder.DropTable(
                name: "musicSiteModels");

            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropTable(
                name: "CombinedTariffModels");

            migrationBuilder.DropTable(
                name: "tarefeModels");

            migrationBuilder.DropTable(
                name: "telegramChannels");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "tickets");

            migrationBuilder.DropTable(
                name: "pageTypeCategories");

            migrationBuilder.DropTable(
                name: "pageTypes");
        }
    }
}
