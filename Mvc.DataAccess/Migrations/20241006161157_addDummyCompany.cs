using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mvc.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addDummyCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "City", "CurrentAddress", "Name", "PhoneNumber", "PostalCode", "State" },
                values: new object[,]
                {
                    { 1, "Delhi", null, "HASBC", "212332323", "123456", "Delhi" },
                    { 2, "Bangalore", null, "YTFD", "123456774", "765432", "Karnaataka" },
                    { 3, "Mumbai", null, "ETEED", "234567", "867654", "Maharashtra" },
                    { 4, "Indore", null, "LTRD", "2345654323", "234432", "MadhyaPradesh" },
                    { 5, "Kolkata", null, "UJRD", "7787234342", "345433", "WestBengal" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
