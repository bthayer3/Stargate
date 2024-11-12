using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StargateAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddRequestLogTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RequestLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Endpoint = table.Column<string>(type: "TEXT", nullable: false),
                    HttpMethod = table.Column<string>(type: "TEXT", nullable: false),
                    StatusCode = table.Column<int>(type: "INTEGER", nullable: false),
                    RequestTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ResponseTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ExceptionMessage = table.Column<string>(type: "TEXT", nullable: true),
                    IsSuccess = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestLog", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestLog");
        }
    }
}
