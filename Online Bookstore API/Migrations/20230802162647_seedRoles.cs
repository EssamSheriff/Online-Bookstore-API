using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Online_Bookstore_API.Migrations
{
    /// <inheritdoc />
    public partial class seedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new [] { "Id" ,"Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { Guid.NewGuid().ToString(), Constants.AdminRoleString, Constants.AdminRoleString.ToUpper(), Guid.NewGuid().ToString(), }
            );
            migrationBuilder.InsertData(
    table: "AspNetRoles",
    columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
    values: new object[] { Guid.NewGuid().ToString(), Constants.UserRoleString, Constants.UserRoleString.ToUpper(), Guid.NewGuid().ToString(), }
);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from [AspNetRoles]");
        }
    }
}
