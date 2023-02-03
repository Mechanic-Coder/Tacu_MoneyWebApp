using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountingDB.Migrations
{
    /// <inheritdoc />
    public partial class account : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankUsers_BankAccounts_Id",
                table: "BankUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_BankAccounts_BankAccountId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "BankAccounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BankUsers",
                table: "BankUsers");

            migrationBuilder.DropColumn(
                name: "BankId",
                table: "BankUsers");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "BankUsers",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_BankUsers_Id",
                table: "BankUsers",
                newName: "IX_BankUsers_AccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BankUsers",
                table: "BankUsers",
                columns: new[] { "UserId", "AccountId" });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_BankUsers_Accounts_AccountId",
                table: "BankUsers",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_BankAccountId",
                table: "Transactions",
                column: "BankAccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankUsers_Accounts_AccountId",
                table: "BankUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_BankAccountId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BankUsers",
                table: "BankUsers");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "BankUsers",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_BankUsers_AccountId",
                table: "BankUsers",
                newName: "IX_BankUsers_Id");

            migrationBuilder.AddColumn<int>(
                name: "BankId",
                table: "BankUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BankUsers",
                table: "BankUsers",
                columns: new[] { "UserId", "BankId" });

            migrationBuilder.CreateTable(
                name: "BankAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccounts", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_BankUsers_BankAccounts_Id",
                table: "BankUsers",
                column: "Id",
                principalTable: "BankAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_BankAccounts_BankAccountId",
                table: "Transactions",
                column: "BankAccountId",
                principalTable: "BankAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
