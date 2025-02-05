using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    public partial class EditCampaignPageAndCampaignTel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdminId",
                table: "CampaignPages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "CampaignPages",
                type: "decimal(18,2)",
                nullable: true);

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
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "campaignChannels",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SiteAddress",
                table: "campaignChannels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SiteIcon",
                table: "campaignChannels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SiteName",
                table: "campaignChannels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SiteTopic",
                table: "campaignChannels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "CampaignPages");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "CampaignPages");

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

            migrationBuilder.DropColumn(
                name: "Price",
                table: "campaignChannels");

            migrationBuilder.DropColumn(
                name: "SiteAddress",
                table: "campaignChannels");

            migrationBuilder.DropColumn(
                name: "SiteIcon",
                table: "campaignChannels");

            migrationBuilder.DropColumn(
                name: "SiteName",
                table: "campaignChannels");

            migrationBuilder.DropColumn(
                name: "SiteTopic",
                table: "campaignChannels");
        }
    }
}
