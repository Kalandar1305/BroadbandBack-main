using System;
using BroadBandBillingPaymentSystem.Data;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BroadbandBillingPaymentSystem.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    admin_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.admin_id);
                });

            migrationBuilder.CreateTable(
                name: "TarrifPlan",
                columns: table => new
                {
                    tarrif_plan_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    amount = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    admin_id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TarrifPlan", x => x.tarrif_plan_id);
                    table.ForeignKey(
                        name: "FK_TarrifPlan_Admin_admin_id",
                        column: x => x.admin_id,
                        principalTable: "Admin",
                        principalColumn: "admin_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    account_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tarrif_plan_id = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.account_id);
                    table.ForeignKey(
                        name: "FK_Account_TarrifPlan_tarrif_plan_id",
                        column: x => x.tarrif_plan_id,
                        principalTable: "TarrifPlan",
                        principalColumn: "tarrif_plan_id");
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    customer_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    f_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    l_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    account_id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.customer_id);
                    table.ForeignKey(
                        name: "FK_Customer_Account_account_id",
                        column: x => x.account_id,
                        principalTable: "Account",
                        principalColumn: "account_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bill",
                columns: table => new
                {
                    bill_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    amount = table.Column<int>(type: "int", nullable: false),
                    due_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    bill_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    payment_mode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    payment_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    customer_id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    account_id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bill", x => x.bill_id);
                    table.ForeignKey(
                        name: "FK_Bill_Account_account_id",
                        column: x => x.account_id,
                        principalTable: "Account",
                        principalColumn: "account_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bill_Customer_customer_id",
                        column: x => x.customer_id,
                        principalTable: "Customer",
                        principalColumn: "customer_id");
                });

            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    feedback_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    review = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    rating = table.Column<int>(type: "int", nullable: false),
                    reply = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    reply_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    customer_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    admin_id = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedback", x => x.feedback_id);
                    table.ForeignKey(
                        name: "FK_Feedback_Admin_admin_id",
                        column: x => x.admin_id,
                        principalTable: "Admin",
                        principalColumn: "admin_id");
                    table.ForeignKey(
                        name: "FK_Feedback_Customer_customer_id",
                        column: x => x.customer_id,
                        principalTable: "Customer",
                        principalColumn: "customer_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_tarrif_plan_id",
                table: "Account",
                column: "tarrif_plan_id");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_account_id",
                table: "Bill",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_customer_id",
                table: "Bill",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_account_id",
                table: "Customer",
                column: "account_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_admin_id",
                table: "Feedback",
                column: "admin_id");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_customer_id",
                table: "Feedback",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_TarrifPlan_admin_id",
                table: "TarrifPlan",
                column: "admin_id");
            AppDbContext.CreateCustomElements(migrationBuilder);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bill");

            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "TarrifPlan");

            migrationBuilder.DropTable(
                name: "Admin");

            AppDbContext.DropCustomElements(migrationBuilder);
        }
    }
}
