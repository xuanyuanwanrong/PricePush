using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PricePush.Migrations
{
    public partial class addtgbotid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BotID",
                table: "TgBot",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BotID",
                table: "TgBot");
        }
    }
}
