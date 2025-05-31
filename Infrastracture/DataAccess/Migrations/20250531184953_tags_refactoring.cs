using Common.Enums;
using Common.Extentions;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class tags_refactoring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropColumn(
                name: "Value",
                table: "TagTable");
            
            migrationBuilder.AddColumn<int>(
                name: "EnumValue",
                table: "TagTable",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "EngName",
                table: "TagTable",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RuName",
                table: "TagTable",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.DropColumn(
                name: "EngName",
                table: "TagTable");

            migrationBuilder.DropColumn(
                name: "RuName",
                table: "TagTable");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "TagTable",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
            
                        
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
    }
}
