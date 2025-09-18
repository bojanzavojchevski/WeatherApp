using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherApp.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddBaseEntityModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlertRules_Locations_LocationId",
                table: "AlertRules");

            migrationBuilder.DropForeignKey(
                name: "FK_WeatherSnapshots_Locations_LocationId",
                table: "WeatherSnapshots");

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "WeatherSnapshots",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Locations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AlertRules",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "AlertRules",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_AlertRules_Locations_LocationId",
                table: "AlertRules",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WeatherSnapshots_Locations_LocationId",
                table: "WeatherSnapshots",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlertRules_Locations_LocationId",
                table: "AlertRules");

            migrationBuilder.DropForeignKey(
                name: "FK_WeatherSnapshots_Locations_LocationId",
                table: "WeatherSnapshots");

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "WeatherSnapshots",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AlertRules",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "AlertRules",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AlertRules_Locations_LocationId",
                table: "AlertRules",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WeatherSnapshots_Locations_LocationId",
                table: "WeatherSnapshots",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
