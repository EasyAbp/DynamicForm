using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyAbp.DynamicForm.Blazor.Server.Host.Migrations
{
    public partial class AddedGroupProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Group",
                table: "EasyAbpDynamicFormFormItemTemplates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Group",
                table: "EasyAbpDynamicFormFormItems",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Group",
                table: "EasyAbpDynamicFormFormItemTemplates");

            migrationBuilder.DropColumn(
                name: "Group",
                table: "EasyAbpDynamicFormFormItems");
        }
    }
}
