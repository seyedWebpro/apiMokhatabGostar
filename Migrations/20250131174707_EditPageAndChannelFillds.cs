using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    public partial class EditPageAndChannelFillds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SiteAddress",
                table: "CampaignPages");

            migrationBuilder.DropColumn(
                name: "SiteIcon",
                table: "CampaignPages");

            migrationBuilder.DropColumn(
                name: "SiteName",
                table: "CampaignPages");

            migrationBuilder.DropColumn(
                name: "SiteTopic",
                table: "CampaignPages");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "campaignChannels");

            migrationBuilder.RenameColumn(
                name: "AdminId",
                table: "CampaignPages",
                newName: "sex");

            migrationBuilder.RenameColumn(
                name: "SiteTopic",
                table: "campaignChannels",
                newName: "Topic");

            migrationBuilder.RenameColumn(
                name: "SiteName",
                table: "campaignChannels",
                newName: "PhotoPath");

            migrationBuilder.RenameColumn(
                name: "SiteIcon",
                table: "campaignChannels",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "SiteAddress",
                table: "campaignChannels",
                newName: "ManagerId");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CampaignPages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Engagement",
                table: "CampaignPages",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Followesrs",
                table: "CampaignPages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Following",
                table: "CampaignPages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImgUrl",
                table: "CampaignPages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersianName",
                table: "CampaignPages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PostImpertion",
                table: "CampaignPages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PostLikes",
                table: "CampaignPages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostLink",
                table: "CampaignPages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PostViews",
                table: "CampaignPages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShowName",
                table: "CampaignPages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoryImpertion",
                table: "CampaignPages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoryViews",
                table: "CampaignPages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TelegramID",
                table: "CampaignPages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WhatsappNumber",
                table: "CampaignPages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "postComments",
                table: "CampaignPages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubscribersCount",
                table: "campaignChannels",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "CampaignPages");

            migrationBuilder.DropColumn(
                name: "Engagement",
                table: "CampaignPages");

            migrationBuilder.DropColumn(
                name: "Followesrs",
                table: "CampaignPages");

            migrationBuilder.DropColumn(
                name: "Following",
                table: "CampaignPages");

            migrationBuilder.DropColumn(
                name: "ImgUrl",
                table: "CampaignPages");

            migrationBuilder.DropColumn(
                name: "PersianName",
                table: "CampaignPages");

            migrationBuilder.DropColumn(
                name: "PostImpertion",
                table: "CampaignPages");

            migrationBuilder.DropColumn(
                name: "PostLikes",
                table: "CampaignPages");

            migrationBuilder.DropColumn(
                name: "PostLink",
                table: "CampaignPages");

            migrationBuilder.DropColumn(
                name: "PostViews",
                table: "CampaignPages");

            migrationBuilder.DropColumn(
                name: "ShowName",
                table: "CampaignPages");

            migrationBuilder.DropColumn(
                name: "StoryImpertion",
                table: "CampaignPages");

            migrationBuilder.DropColumn(
                name: "StoryViews",
                table: "CampaignPages");

            migrationBuilder.DropColumn(
                name: "TelegramID",
                table: "CampaignPages");

            migrationBuilder.DropColumn(
                name: "WhatsappNumber",
                table: "CampaignPages");

            migrationBuilder.DropColumn(
                name: "postComments",
                table: "CampaignPages");

            migrationBuilder.DropColumn(
                name: "SubscribersCount",
                table: "campaignChannels");

            migrationBuilder.RenameColumn(
                name: "sex",
                table: "CampaignPages",
                newName: "AdminId");

            migrationBuilder.RenameColumn(
                name: "Topic",
                table: "campaignChannels",
                newName: "SiteTopic");

            migrationBuilder.RenameColumn(
                name: "PhotoPath",
                table: "campaignChannels",
                newName: "SiteName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "campaignChannels",
                newName: "SiteIcon");

            migrationBuilder.RenameColumn(
                name: "ManagerId",
                table: "campaignChannels",
                newName: "SiteAddress");

            migrationBuilder.AddColumn<string>(
                name: "SiteAddress",
                table: "CampaignPages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SiteIcon",
                table: "CampaignPages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SiteName",
                table: "CampaignPages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SiteTopic",
                table: "CampaignPages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AdminId",
                table: "campaignChannels",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
