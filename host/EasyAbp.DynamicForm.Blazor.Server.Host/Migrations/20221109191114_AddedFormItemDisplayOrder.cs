using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyAbp.DynamicForm.Blazor.Server.Host.Migrations
{
    public partial class AddedFormItemDisplayOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "EasyAbpDynamicFormFormItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "EasyAbpDynamicFormFormItems");
        }
    }
}
