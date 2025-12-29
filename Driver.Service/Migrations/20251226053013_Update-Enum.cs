using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Driver.Service.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsMultipleChoice",
                table: "Questions",
                newName: "QuestionType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QuestionType",
                table: "Questions",
                newName: "IsMultipleChoice");
        }
    }
}
