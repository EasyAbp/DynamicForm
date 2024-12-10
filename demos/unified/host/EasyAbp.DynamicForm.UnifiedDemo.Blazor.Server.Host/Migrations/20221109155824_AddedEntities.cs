using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyAbp.DynamicForm.Blazor.Server.Host.Migrations
{
    public partial class AddedEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EasyAbpDynamicFormForms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FormDefinitionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FormTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormTemplateName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EasyAbpDynamicFormForms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EasyAbpDynamicFormFormTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FormDefinitionName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomTag = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EasyAbpDynamicFormFormTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EasyAbpDynamicFormFormItems",
                columns: table => new
                {
                    FormId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Optional = table.Column<bool>(type: "bit", nullable: false),
                    Configurations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AvailableValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EasyAbpDynamicFormFormItems", x => new { x.FormId, x.Name });
                    table.ForeignKey(
                        name: "FK_EasyAbpDynamicFormFormItems_EasyAbpDynamicFormForms_FormId",
                        column: x => x.FormId,
                        principalTable: "EasyAbpDynamicFormForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EasyAbpDynamicFormFormItemTemplates",
                columns: table => new
                {
                    FormTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InfoText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Optional = table.Column<bool>(type: "bit", nullable: false),
                    Configurations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AvailableValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EasyAbpDynamicFormFormItemTemplates", x => new { x.FormTemplateId, x.Name });
                    table.ForeignKey(
                        name: "FK_EasyAbpDynamicFormFormItemTemplates_EasyAbpDynamicFormFormTemplates_FormTemplateId",
                        column: x => x.FormTemplateId,
                        principalTable: "EasyAbpDynamicFormFormTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EasyAbpDynamicFormForms_FormTemplateId",
                table: "EasyAbpDynamicFormForms",
                column: "FormTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_EasyAbpDynamicFormFormTemplates_CustomTag",
                table: "EasyAbpDynamicFormFormTemplates",
                column: "CustomTag");

            migrationBuilder.CreateIndex(
                name: "IX_EasyAbpDynamicFormFormTemplates_FormDefinitionName",
                table: "EasyAbpDynamicFormFormTemplates",
                column: "FormDefinitionName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EasyAbpDynamicFormFormItems");

            migrationBuilder.DropTable(
                name: "EasyAbpDynamicFormFormItemTemplates");

            migrationBuilder.DropTable(
                name: "EasyAbpDynamicFormForms");

            migrationBuilder.DropTable(
                name: "EasyAbpDynamicFormFormTemplates");
        }
    }
}
