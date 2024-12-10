using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyAbp.DynamicForm.Blazor.Server.Host.Migrations
{
    public partial class AddedFormItemInfoTextProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InfoText",
                table: "EasyAbpDynamicFormFormItems",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InfoText",
                table: "EasyAbpDynamicFormFormItems");
        }
    }
}
