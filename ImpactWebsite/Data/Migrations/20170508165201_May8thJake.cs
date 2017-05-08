using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ImpactWebsite.Data.Migrations
{
    public partial class May8thJake : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Investment_OrderHeader_OrderHeaderId",
                table: "Investment");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderHeader_AspNetUsers_ApplicationUserId",
                table: "OrderHeader");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UnitPrice",
                table: "UnitPrice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Promotion",
                table: "Promotion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderLine",
                table: "OrderLine");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderHeader",
                table: "OrderHeader");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewsLetterUser",
                table: "NewsLetterUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Module",
                table: "Module");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Investment",
                table: "Investment");

            migrationBuilder.RenameTable(
                name: "UnitPrice",
                newName: "UnitPrices");

            migrationBuilder.RenameTable(
                name: "Promotion",
                newName: "Promotions");

            migrationBuilder.RenameTable(
                name: "OrderLine",
                newName: "OrderLines");

            migrationBuilder.RenameTable(
                name: "OrderHeader",
                newName: "OrderHeaders");

            migrationBuilder.RenameTable(
                name: "NewsLetterUser",
                newName: "NewsLetterUsers");

            migrationBuilder.RenameTable(
                name: "Module",
                newName: "Modules");

            migrationBuilder.RenameTable(
                name: "Investment",
                newName: "Investments");

            migrationBuilder.RenameIndex(
                name: "IX_OrderHeader_ApplicationUserId",
                table: "OrderHeaders",
                newName: "IX_OrderHeaders_ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Investment_OrderHeaderId",
                table: "Investments",
                newName: "IX_Investments_OrderHeaderId");

            migrationBuilder.AddColumn<string>(
                name: "ModuleName",
                table: "Modules",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModuleUrl",
                table: "Modules",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UnitPrices",
                table: "UnitPrices",
                column: "UnitPriceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Promotions",
                table: "Promotions",
                column: "PromotionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderLines",
                table: "OrderLines",
                column: "OrderLineId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderHeaders",
                table: "OrderHeaders",
                column: "OrderHeaderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewsLetterUsers",
                table: "NewsLetterUsers",
                column: "NewsLetterUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Modules",
                table: "Modules",
                column: "ModuleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Investments",
                table: "Investments",
                column: "InvestmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Investments_OrderHeaders_OrderHeaderId",
                table: "Investments",
                column: "OrderHeaderId",
                principalTable: "OrderHeaders",
                principalColumn: "OrderHeaderId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHeaders_AspNetUsers_ApplicationUserId",
                table: "OrderHeaders",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Investments_OrderHeaders_OrderHeaderId",
                table: "Investments");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderHeaders_AspNetUsers_ApplicationUserId",
                table: "OrderHeaders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UnitPrices",
                table: "UnitPrices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Promotions",
                table: "Promotions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderLines",
                table: "OrderLines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderHeaders",
                table: "OrderHeaders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewsLetterUsers",
                table: "NewsLetterUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Modules",
                table: "Modules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Investments",
                table: "Investments");

            migrationBuilder.DropColumn(
                name: "ModuleName",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "ModuleUrl",
                table: "Modules");

            migrationBuilder.RenameTable(
                name: "UnitPrices",
                newName: "UnitPrice");

            migrationBuilder.RenameTable(
                name: "Promotions",
                newName: "Promotion");

            migrationBuilder.RenameTable(
                name: "OrderLines",
                newName: "OrderLine");

            migrationBuilder.RenameTable(
                name: "OrderHeaders",
                newName: "OrderHeader");

            migrationBuilder.RenameTable(
                name: "NewsLetterUsers",
                newName: "NewsLetterUser");

            migrationBuilder.RenameTable(
                name: "Modules",
                newName: "Module");

            migrationBuilder.RenameTable(
                name: "Investments",
                newName: "Investment");

            migrationBuilder.RenameIndex(
                name: "IX_OrderHeaders_ApplicationUserId",
                table: "OrderHeader",
                newName: "IX_OrderHeader_ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Investments_OrderHeaderId",
                table: "Investment",
                newName: "IX_Investment_OrderHeaderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UnitPrice",
                table: "UnitPrice",
                column: "UnitPriceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Promotion",
                table: "Promotion",
                column: "PromotionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderLine",
                table: "OrderLine",
                column: "OrderLineId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderHeader",
                table: "OrderHeader",
                column: "OrderHeaderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewsLetterUser",
                table: "NewsLetterUser",
                column: "NewsLetterUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Module",
                table: "Module",
                column: "ModuleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Investment",
                table: "Investment",
                column: "InvestmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Investment_OrderHeader_OrderHeaderId",
                table: "Investment",
                column: "OrderHeaderId",
                principalTable: "OrderHeader",
                principalColumn: "OrderHeaderId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHeader_AspNetUsers_ApplicationUserId",
                table: "OrderHeader",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
