using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FileTrackingSystem.DAL.Migrations.ApplicationDb
{
    public partial class AppDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "idProof",
                table: "Client",
                type: "varbinary(4000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Gender",
                table: "Client",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "idProof",
                table: "Client",
                type: "text",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(4000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Client",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
