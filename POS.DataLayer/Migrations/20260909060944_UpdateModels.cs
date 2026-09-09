using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_RoleUpdatedBy",
                table: "Users");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "Users",
                type: "int",
                nullable: true);

            

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedOn",
                table: "Roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "Roles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Roles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Users_DeletedBy",
                table: "Users",
                column: "DeletedBy");

            

            migrationBuilder.CreateIndex(
                name: "IX_Roles_DeletedBy",
                table: "Roles",
                column: "DeletedBy");

            

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Users_DeletedBy",
                table: "Roles",
                column: "DeletedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_DeletedBy",
                table: "Users",
                column: "DeletedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_RoleUpdatedBy",
                table: "Users",
                column: "RoleUpdatedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Users_DeletedBy",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_DeletedBy",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_RoleUpdatedBy",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_DeletedBy",
                table: "Users");

            

            migrationBuilder.DropIndex(
                name: "IX_Roles_DeletedBy",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Users");

           

            migrationBuilder.DropColumn(
                name: "AddedOn",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Roles");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_RoleUpdatedBy",
                table: "Users",
                column: "RoleUpdatedBy",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
