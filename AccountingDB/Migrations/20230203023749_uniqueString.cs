using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountingDB.Migrations
{
    /// <inheritdoc />
    public partial class uniqueString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Metas_Content",
                table: "Metas",
                column: "Content",
                unique: true,
                filter: "[Content] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Metas_Content",
                table: "Metas");
        }
    }
}
