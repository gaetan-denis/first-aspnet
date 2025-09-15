using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Domains",
                columns: table => new
                {
                    DOMA_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DOMA_name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Domains", x => x.DOMA_id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    USER_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USER_Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    USER_Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    USER_Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    USER_IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    USER_CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    USER_UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.USER_Id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    POST_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    POST_userId = table.Column<int>(type: "int", nullable: false),
                    POST_title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    POST_content = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    POST_createddAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    POST_updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.POST_id);
                    table.ForeignKey(
                        name: "FK_Posts_Users_POST_userId",
                        column: x => x.POST_userId,
                        principalTable: "Users",
                        principalColumn: "USER_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostDomains",
                columns: table => new
                {
                    POSTDOM_postId = table.Column<int>(type: "int", nullable: false),
                    DomainId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostDomains", x => new { x.POSTDOM_postId, x.DomainId });
                    table.ForeignKey(
                        name: "FK_PostDomains_Domains_DomainId",
                        column: x => x.DomainId,
                        principalTable: "Domains",
                        principalColumn: "DOMA_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostDomains_Posts_POSTDOM_postId",
                        column: x => x.POSTDOM_postId,
                        principalTable: "Posts",
                        principalColumn: "POST_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostDomains_DomainId",
                table: "PostDomains",
                column: "DomainId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_POST_userId",
                table: "Posts",
                column: "POST_userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostDomains");

            migrationBuilder.DropTable(
                name: "Domains");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
