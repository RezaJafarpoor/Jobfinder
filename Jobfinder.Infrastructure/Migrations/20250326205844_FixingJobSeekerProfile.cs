using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobfinder.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixingJobSeekerProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_JobSeekerProfiles_CvId",
                table: "JobSeekerProfiles");

            migrationBuilder.AlterColumn<Guid>(
                name: "CvId",
                table: "JobSeekerProfiles",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekerProfiles_CvId",
                table: "JobSeekerProfiles",
                column: "CvId",
                unique: true,
                filter: "[CvId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_JobSeekerProfiles_CvId",
                table: "JobSeekerProfiles");

            migrationBuilder.AlterColumn<Guid>(
                name: "CvId",
                table: "JobSeekerProfiles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekerProfiles_CvId",
                table: "JobSeekerProfiles",
                column: "CvId",
                unique: true);
        }
    }
}
