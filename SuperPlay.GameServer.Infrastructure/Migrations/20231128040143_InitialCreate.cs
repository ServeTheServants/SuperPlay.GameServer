using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperPlay.GameServer.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    PlayerId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DeviceId = table.Column<string>(type: "TEXT", nullable: false),
                    Coins = table.Column<int>(type: "INTEGER", nullable: false),
                    Rolls = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.PlayerId);
                });

            migrationBuilder.InsertData(
                table: "Player",
                columns: new[] { "PlayerId", "Coins", "DeviceId", "Rolls" },
                values: new object[] { 1, 0, "1", 0 });

            migrationBuilder.InsertData(
                table: "Player",
                columns: new[] { "PlayerId", "Coins", "DeviceId", "Rolls" },
                values: new object[] { 2, 0, "2", 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Player");
        }
    }
}
