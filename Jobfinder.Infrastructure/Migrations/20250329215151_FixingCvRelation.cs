using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobfinder.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixingCvRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobSeekerProfiles_Cvs_CvId",
                table: "JobSeekerProfiles");

            migrationBuilder.DropIndex(
                name: "IX_JobSeekerProfiles_CvId",
                table: "JobSeekerProfiles");

            migrationBuilder.DropColumn(
                name: "CvId",
                table: "JobSeekerProfiles");

            migrationBuilder.CreateIndex(
                name: "IX_Cvs_JobSeekerId",
                table: "Cvs",
                column: "JobSeekerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cvs_JobSeekerProfiles_JobSeekerId",
                table: "Cvs",
                column: "JobSeekerId",
                principalTable: "JobSeekerProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cvs_JobSeekerProfiles_JobSeekerId",
                table: "Cvs");

            migrationBuilder.DropIndex(
                name: "IX_Cvs_JobSeekerId",
                table: "Cvs");

            migrationBuilder.AddColumn<Guid>(
                name: "CvId",
                table: "JobSeekerProfiles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekerProfiles_CvId",
                table: "JobSeekerProfiles",
                column: "CvId",
                unique: true,
                filter: "[CvId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_JobSeekerProfiles_Cvs_CvId",
                table: "JobSeekerProfiles",
                column: "CvId",
                principalTable: "Cvs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
