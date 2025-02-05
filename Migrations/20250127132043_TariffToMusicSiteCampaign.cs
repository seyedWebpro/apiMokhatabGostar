using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    public partial class TariffToMusicSiteCampaign : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TariffId",
                table: "campaignMusicSites",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TariffId",
                table: "campaignChannels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_campaignMusicSites_TariffId",
                table: "campaignMusicSites",
                column: "TariffId");

            migrationBuilder.CreateIndex(
                name: "IX_campaignChannels_TariffId",
                table: "campaignChannels",
                column: "TariffId");

            migrationBuilder.AddForeignKey(
                name: "FK_campaignChannels_tarefeModels_TariffId",
                table: "campaignChannels",
                column: "TariffId",
                principalTable: "tarefeModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_campaignMusicSites_tarefeModels_TariffId",
                table: "campaignMusicSites",
                column: "TariffId",
                principalTable: "tarefeModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_campaignChannels_tarefeModels_TariffId",
                table: "campaignChannels");

            migrationBuilder.DropForeignKey(
                name: "FK_campaignMusicSites_tarefeModels_TariffId",
                table: "campaignMusicSites");

            migrationBuilder.DropIndex(
                name: "IX_campaignMusicSites_TariffId",
                table: "campaignMusicSites");

            migrationBuilder.DropIndex(
                name: "IX_campaignChannels_TariffId",
                table: "campaignChannels");

            migrationBuilder.DropColumn(
                name: "TariffId",
                table: "campaignMusicSites");

            migrationBuilder.DropColumn(
                name: "TariffId",
                table: "campaignChannels");
        }
    }
}
