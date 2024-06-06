using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerManagerDAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop existing tables if they exist using raw SQL
            migrationBuilder.Sql("IF OBJECT_ID('dbo.Requests', 'U') IS NOT NULL DROP TABLE dbo.Requests;");
            migrationBuilder.Sql("IF OBJECT_ID('dbo.Sessions', 'U') IS NOT NULL DROP TABLE dbo.Sessions;");
            migrationBuilder.Sql("IF OBJECT_ID('dbo.Servers', 'U') IS NOT NULL DROP TABLE dbo.Servers;");
            migrationBuilder.Sql("IF OBJECT_ID('dbo.SessionServer', 'U') IS NOT NULL DROP TABLE dbo.SessionServer;");

            // Create the Requests table
            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 10000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                });

            // Create the Servers table
            migrationBuilder.CreateTable(
                name: "Servers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 10000, nullable: false),
                    GameName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Ip = table.Column<string>(type: "nvarchar(39)", maxLength: 39, nullable: false),
                    Port = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servers", x => x.Id);
                });

            // Create the Sessions table
            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 10000, nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                });

            // Create the linking table for many-to-many relationship between Sessions and Servers
            migrationBuilder.CreateTable(
                name: "SessionServer",
                columns: table => new
                {
                    SessionId = table.Column<int>(type: "int", nullable: false),
                    ServerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionServer", x => new { x.SessionId, x.ServerId });
                    table.ForeignKey(
                        name: "FK_SessionServer_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SessionServer_Servers_ServerId",
                        column: x => x.ServerId,
                        principalTable: "Servers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Adding indexes on foreign keys to improve lookup performance
            migrationBuilder.CreateIndex(
                name: "IX_SessionServer_ServerId",
                table: "SessionServer",
                column: "ServerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "SessionServer");

            migrationBuilder.DropTable(
                name: "Servers");

            migrationBuilder.DropTable(
                name: "Sessions");
        }
    }
}
