﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyInfo.API.Migrations
{
    /// <inheritdoc />
    public partial class DescriptionAddedtoCarmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CarModels",
                type: "TEXT",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "CarModels");
        }
    }
}