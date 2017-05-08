using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ImpactWebsite.Data.Migrations
{
    public partial class EntityUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "NewsletterRequired",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewsletterRequired",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
