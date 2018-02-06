using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RaportareOTR.Data.Migrations
{
    public partial class addEstimatesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estimate",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AppliancesQuantity = table.Column<double>(type: "float", nullable: false),
                    AutomatedDistributors = table.Column<double>(type: "float", nullable: false),
                    BagsQuantity = table.Column<double>(type: "float", nullable: false),
                    BatteryQuantity = table.Column<double>(type: "float", nullable: false),
                    BigHouseHoldAppliances = table.Column<double>(type: "float", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DangerousSubstances = table.Column<double>(type: "float", nullable: false),
                    ElectricalTools = table.Column<double>(type: "float", nullable: false),
                    ElectricalToys = table.Column<double>(type: "float", nullable: false),
                    ElectronicEquipment = table.Column<double>(type: "float", nullable: false),
                    Glass = table.Column<double>(type: "float", nullable: false),
                    InformaticEquipment = table.Column<double>(type: "float", nullable: false),
                    LightingEquipment = table.Column<double>(type: "float", nullable: false),
                    MedicalDevices = table.Column<double>(type: "float", nullable: false),
                    Metal = table.Column<double>(type: "float", nullable: false),
                    MetalAl = table.Column<double>(type: "float", nullable: false),
                    MetalSteel = table.Column<double>(type: "float", nullable: false),
                    MonitoringInstruments = table.Column<double>(type: "float", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    OilQuantity = table.Column<double>(type: "float", nullable: false),
                    PaperCarton = table.Column<double>(type: "float", nullable: false),
                    Plastic = table.Column<double>(type: "float", nullable: false),
                    PlasticOthers = table.Column<double>(type: "float", nullable: false),
                    PlasticPE = table.Column<double>(type: "float", nullable: false),
                    PlasticPET = table.Column<double>(type: "float", nullable: false),
                    PlasticPP = table.Column<double>(type: "float", nullable: false),
                    PlasticPS = table.Column<double>(type: "float", nullable: false),
                    PlasticPVC = table.Column<double>(type: "float", nullable: false),
                    SmallHouseHoldAppliances = table.Column<double>(type: "float", nullable: false),
                    StickerPaperCarton = table.Column<double>(type: "float", nullable: false),
                    TetraPakPaperCarton = table.Column<double>(type: "float", nullable: false),
                    Total = table.Column<double>(type: "float", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    WheelsQuantity = table.Column<double>(type: "float", nullable: false),
                    Wood = table.Column<double>(type: "float", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estimate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Estimate_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Estimate_UserId",
                table: "Estimate",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Estimate");
        }
    }
}
