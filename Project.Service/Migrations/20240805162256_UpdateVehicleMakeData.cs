using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Service.Migrations
{
    /// <inheritdoc />
    public partial class UpdateVehicleMakeData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "VehicleModels",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "VehicleMakes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Bayerische Motoren Werke");

            migrationBuilder.UpdateData(
                table: "VehicleModels",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Abrv", "Name" },
                values: new object[] { "X3", "BMW X3" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "VehicleMakes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "BMW");

            migrationBuilder.UpdateData(
                table: "VehicleModels",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Abrv", "Name" },
                values: new object[] { "128", "128" });

            migrationBuilder.InsertData(
                table: "VehicleModels",
                columns: new[] { "Id", "Abrv", "MakeId", "Name" },
                values: new object[] { 2, "325", 1, "325" });
        }
    }
}
