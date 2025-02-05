using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    public partial class AddFieldsToCampaignMusicSite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdminId",
                table: "campaignMusicSites",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SiteAddress",
                table: "campaignMusicSites",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SiteIcon",
                table: "campaignMusicSites",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SiteName",
                table: "campaignMusicSites",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SiteTopic",
                table: "campaignMusicSites",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "campaignMusicSites");

            migrationBuilder.DropColumn(
                name: "SiteAddress",
                table: "campaignMusicSites");

            migrationBuilder.DropColumn(
                name: "SiteIcon",
                table: "campaignMusicSites");

            migrationBuilder.DropColumn(
                name: "SiteName",
                table: "campaignMusicSites");

            migrationBuilder.DropColumn(
                name: "SiteTopic",
                table: "campaignMusicSites");
        }
    }
}
