using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountingDB.Migrations
{
    /// <inheritdoc />
    public partial class fixComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MetaType",
                table: "TransactionMetas",
                type: "int",
                nullable: false,
                comment: "Description: 1, Category: 2, Note: 3, Tag: 4, ExtDescription: 5",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Description: 1\r\nCategory: 2\r\nNote: 3\r\nTag: 4\r\nExtDescription: 5");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MetaType",
                table: "TransactionMetas",
                type: "int",
                nullable: false,
                comment: "Description: 1\r\nCategory: 2\r\nNote: 3\r\nTag: 4\r\nExtDescription: 5",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Description: 1, Category: 2, Note: 3, Tag: 4, ExtDescription: 5");
        }
    }
}
