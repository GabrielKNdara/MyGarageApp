﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyGarageApp.Data.Migrations
{
    public partial class AddShoppingcartAndServiceHeaderAndServiceDetailToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServiceHeader",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Miles = table.Column<double>(type: "float", nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceHeader", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceHeader_Car_CarId",
                        column: x => x.CarId,
                        principalTable: "Car",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceShoppingCart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    ServiceTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceShoppingCart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceShoppingCart_Car_CarId",
                        column: x => x.CarId,
                        principalTable: "Car",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceShoppingCart_ServiceType_ServiceTypeId",
                        column: x => x.ServiceTypeId,
                        principalTable: "ServiceType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceHeaderId = table.Column<int>(type: "int", nullable: false),
                    ServiceTypeId = table.Column<int>(type: "int", nullable: false),
                    ServicePrice = table.Column<double>(type: "float", nullable: false),
                    ServiceName = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceDetails_ServiceHeader_ServiceHeaderId",
                        column: x => x.ServiceHeaderId,
                        principalTable: "ServiceHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceDetails_ServiceType_ServiceTypeId",
                        column: x => x.ServiceTypeId,
                        principalTable: "ServiceType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceDetails_ServiceHeaderId",
                table: "ServiceDetails",
                column: "ServiceHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceDetails_ServiceTypeId",
                table: "ServiceDetails",
                column: "ServiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceHeader_CarId",
                table: "ServiceHeader",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceShoppingCart_CarId",
                table: "ServiceShoppingCart",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceShoppingCart_ServiceTypeId",
                table: "ServiceShoppingCart",
                column: "ServiceTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceDetails");

            migrationBuilder.DropTable(
                name: "ServiceShoppingCart");

            migrationBuilder.DropTable(
                name: "ServiceHeader");
        }
    }
}
