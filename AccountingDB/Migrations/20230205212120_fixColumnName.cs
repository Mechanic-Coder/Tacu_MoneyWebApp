using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountingDB.Migrations
{
    /// <inheritdoc />
    public partial class fixColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_TransactionMerchants_MerchantLocationId",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "MerchantLocationId",
                table: "Transactions",
                newName: "TransactionMerchantId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_MerchantLocationId",
                table: "Transactions",
                newName: "IX_Transactions_TransactionMerchantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_TransactionMerchants_TransactionMerchantId",
                table: "Transactions",
                column: "TransactionMerchantId",
                principalTable: "TransactionMerchants",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_TransactionMerchants_TransactionMerchantId",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "TransactionMerchantId",
                table: "Transactions",
                newName: "MerchantLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_TransactionMerchantId",
                table: "Transactions",
                newName: "IX_Transactions_MerchantLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_TransactionMerchants_MerchantLocationId",
                table: "Transactions",
                column: "MerchantLocationId",
                principalTable: "TransactionMerchants",
                principalColumn: "Id");
        }
    }
}
