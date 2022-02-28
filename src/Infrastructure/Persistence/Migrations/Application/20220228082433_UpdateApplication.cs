using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftwareAssuranceMaturityModel.Infrastructure.Persistence.Migrations.Application
{
    public partial class UpdateApplication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Batches",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Batches");
        }
    }
}
