using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Driver.Service.Migrations
{
    /// <inheritdoc />
    public partial class AddPic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PicPath",
                table: "Questions",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PicPath",
                table: "Questions");
        }
    }
}
