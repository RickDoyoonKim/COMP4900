using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ImpactWebsite.Data.Migrations
{
    public partial class EditOrderHeader : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "OrderHeader",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "TotalAmount",
                table: "OrderHeader",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "OrderHeader");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "OrderHeader",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
