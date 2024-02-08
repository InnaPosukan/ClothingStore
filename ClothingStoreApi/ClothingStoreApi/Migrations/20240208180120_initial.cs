using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothingStoreApi.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    firstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    lastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    phoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    avatar = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    sex = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    dateOfBirth = table.Column<DateTime>(type: "date", nullable: true),
                    accountStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "Advertisement",
                columns: table => new
                {
                    ad_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    advImage = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    price = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    publicationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    seller_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Advertis__CAA4A62799049C04", x => x.ad_id);
                    table.ForeignKey(
                        name: "FK__Advertise__selle__398D8EEE",
                        column: x => x.seller_id,
                        principalTable: "User",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "Advertisement_Attribute",
                columns: table => new
                {
                    category_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ad_id = table.Column<int>(type: "int", nullable: true),
                    size = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    brand = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Advertis__D54EE9B4BB8816B1", x => x.category_id);
                    table.ForeignKey(
                        name: "FK__Advertise__ad_id__3C69FB99",
                        column: x => x.ad_id,
                        principalTable: "Advertisement",
                        principalColumn: "ad_id");
                });

            migrationBuilder.CreateTable(
                name: "Discount",
                columns: table => new
                {
                    discount_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ad_id = table.Column<int>(type: "int", nullable: true),
                    discountPercentage = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    startDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    endDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discount", x => x.discount_id);
                    table.ForeignKey(
                        name: "FK__Discount__ad_id__46E78A0C",
                        column: x => x.ad_id,
                        principalTable: "Advertisement",
                        principalColumn: "ad_id");
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    order_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    ad_id = table.Column<int>(type: "int", nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: true),
                    orderDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.order_id);
                    table.ForeignKey(
                        name: "FK__Order__ad_id__403A8C7D",
                        column: x => x.ad_id,
                        principalTable: "Advertisement",
                        principalColumn: "ad_id");
                    table.ForeignKey(
                        name: "FK__Order__user_id__3F466844",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    rating_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ad_id = table.Column<int>(type: "int", nullable: true),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    ratingValue = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    review = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ratingDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => x.rating_id);
                    table.ForeignKey(
                        name: "FK__Rating__ad_id__4316F928",
                        column: x => x.ad_id,
                        principalTable: "Advertisement",
                        principalColumn: "ad_id");
                    table.ForeignKey(
                        name: "FK__Rating__user_id__440B1D61",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Advertisement_seller_id",
                table: "Advertisement",
                column: "seller_id");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisement_Attribute_ad_id",
                table: "Advertisement_Attribute",
                column: "ad_id");

            migrationBuilder.CreateIndex(
                name: "IX_Discount_ad_id",
                table: "Discount",
                column: "ad_id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ad_id",
                table: "Order",
                column: "ad_id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_user_id",
                table: "Order",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_ad_id",
                table: "Rating",
                column: "ad_id");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_user_id",
                table: "Rating",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Advertisement_Attribute");

            migrationBuilder.DropTable(
                name: "Discount");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropTable(
                name: "Advertisement");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
