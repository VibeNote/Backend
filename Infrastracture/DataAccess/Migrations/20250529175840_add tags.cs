using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addtags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "UserTable",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BlockedTill",
                table: "UserTable",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "EntryTable",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "EntryTable",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AnalysisTable",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
            
            migrationBuilder.InsertData(
                table: "TagTable",
                columns: new[] { "Id", "Value" },
                values: new object[,]
                {
                    { new Guid("a8d5707f-2880-4d22-9561-7d13061b9930"), "Радость" }
                });
            
            migrationBuilder.InsertData(
                table: "TagTable",
                columns: new[] { "Id", "Value" },
                values: new object[,]
                {
                    { new Guid("583b8346-1237-4de6-b014-afaf1812c061"), "Спокойствие" }
                });
            
            migrationBuilder.InsertData(
                table: "TagTable",
                columns: new[] { "Id", "Value" },
                values: new object[,]
                {
                    { new Guid("f7b844fd-9d26-4603-971f-674bafabac2f"), "Злость" }
                });
            
            migrationBuilder.InsertData(
                table: "TagTable",
                columns: new[] { "Id", "Value" },
                values: new object[,]
                {
                    { new Guid("10de9f43-9383-4549-85fd-a152f67981bf"), "Печаль" }
                });
            
            migrationBuilder.InsertData(
                table: "TagTable",
                columns: new[] { "Id", "Value" },
                values: new object[,]
                {
                    { new Guid("73cd3672-328b-48c1-a507-449823a264dc"), "Тревога" }
                });
            
            migrationBuilder.InsertData(
                table: "TagTable",
                columns: new[] { "Id", "Value" },
                values: new object[,]
                {
                    { new Guid("309a9bb6-2b54-409c-995d-693e755ce519"), "Растерянность" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "UserTable",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BlockedTill",
                table: "UserTable",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "EntryTable",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "EntryTable",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AnalysisTable",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");
            
            migrationBuilder.DeleteData(
                table: "TagTable",
                keyColumn: "Id",
                keyColumnType: "uuid",
                keyValue: new Guid("a8d5707f-2880-4d22-9561-7d13061b9930"));
            migrationBuilder.DeleteData(
                table: "TagTable",
                keyColumn: "Id",
                keyColumnType: "uuid",
                keyValue: new Guid("583b8346-1237-4de6-b014-afaf1812c061"));
            migrationBuilder.DeleteData(
                table: "TagTable",
                keyColumn: "Id",
                keyColumnType: "uuid",
                keyValue: new Guid("f7b844fd-9d26-4603-971f-674bafabac2f"));
            migrationBuilder.DeleteData(
                table: "TagTable",
                keyColumn: "Id",
                keyColumnType: "uuid",
                keyValue: new Guid("10de9f43-9383-4549-85fd-a152f67981bf"));
            migrationBuilder.DeleteData(
                table: "TagTable",
                keyColumn: "Id",
                keyColumnType: "uuid",
                keyValue: new Guid("73cd3672-328b-48c1-a507-449823a264dc"));
            migrationBuilder.DeleteData(
                table: "TagTable",
                keyColumn: "Id",
                keyColumnType: "uuid",
                keyValue: new Guid("309a9bb6-2b54-409c-995d-693e755ce519"));
        }
    }
}
