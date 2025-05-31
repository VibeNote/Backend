using Common.Enums;
using Common.Extentions;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class update_tags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
                        
            migrationBuilder.InsertData(
                table: "TagTable",
                columns: new[] { "Id", "EnumValue", "EngName", "RuName" },
                values: new object[,]
                {
                    { new Guid("a8d5707f-2880-4d22-9561-7d13061b9930"), (int)TagsEnum.Joy, TagsEnum.Joy.ToEngName(), TagsEnum.Joy.ToRuName() }
                });
            
            migrationBuilder.InsertData(
                table: "TagTable",
                columns: new[] { "Id", "EnumValue", "EngName", "RuName" },
                values: new object[,]
                {
                    { new Guid("583b8346-1237-4de6-b014-afaf1812c061"), (int)TagsEnum.Calmness, TagsEnum.Calmness.ToEngName(), TagsEnum.Calmness.ToRuName() }
                });
            
            migrationBuilder.InsertData(
                table: "TagTable",
                columns: new[] { "Id", "EnumValue", "EngName", "RuName" },
                values: new object[,]
                {
                    { new Guid("f7b844fd-9d26-4603-971f-674bafabac2f"), (int)TagsEnum.Anger, TagsEnum.Anger.ToEngName(), TagsEnum.Anger.ToRuName() }
                });
            
            migrationBuilder.InsertData(
                table: "TagTable",
                columns: new[] { "Id", "EnumValue", "EngName", "RuName" },
                values: new object[,]
                {
                    { new Guid("10de9f43-9383-4549-85fd-a152f67981bf"), (int)TagsEnum.Sadness, TagsEnum.Sadness.ToEngName(), TagsEnum.Sadness.ToRuName() }
                });
            
            migrationBuilder.InsertData(
                table: "TagTable",
                columns: new[] { "Id", "EnumValue", "EngName", "RuName" },
                values: new object[,]
                {
                    { new Guid("73cd3672-328b-48c1-a507-449823a264dc"), (int)TagsEnum.Anxiety, TagsEnum.Anxiety.ToEngName(), TagsEnum.Anxiety.ToRuName() }
                });
            
            migrationBuilder.InsertData(
                table: "TagTable",
                columns: new[] { "Id", "EnumValue", "EngName", "RuName" },
                values: new object[,]
                {
                    { new Guid("309a9bb6-2b54-409c-995d-693e755ce519"), (int)TagsEnum.Confusion, TagsEnum.Confusion.ToEngName(), TagsEnum.Confusion.ToRuName() }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
