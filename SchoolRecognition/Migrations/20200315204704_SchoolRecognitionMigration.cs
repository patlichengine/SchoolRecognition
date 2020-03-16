using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolRecognition.Migrations
{
    public partial class SchoolRecognitionMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Offices",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    StateId = table.Column<Guid>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ranks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ranks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecognitionTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecognitionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    IsDefault = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsAdmin = table.Column<bool>(nullable: false),
                    IsSuperAdmin = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SchoolCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Surname = table.Column<string>(nullable: true),
                    Others = table.Column<string>(nullable: true),
                    Password = table.Column<byte[]>(nullable: true),
                    RankId = table.Column<Guid>(nullable: true),
                    RoleId = table.Column<Guid>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: true),
                    PhoneNo = table.Column<string>(nullable: true),
                    Lpno = table.Column<string>(nullable: true),
                    Signature = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsVerified = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Ranks_RankId",
                        column: x => x.RankId,
                        principalTable: "Ranks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LocalGovernments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    StateId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalGovernments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocalGovernments_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pins",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RecognitionTypeId = table.Column<Guid>(nullable: true),
                    SerialPin = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsInUse = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    CreatedByNavigationId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pins_Users_CreatedByNavigationId",
                        column: x => x.CreatedByNavigationId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pins_RecognitionTypes_RecognitionTypeId",
                        column: x => x.RecognitionTypeId,
                        principalTable: "RecognitionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Schools",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CategoryId = table.Column<Guid>(nullable: true),
                    OfficeId = table.Column<Guid>(nullable: true),
                    LgId = table.Column<Guid>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: true),
                    PhoneNo = table.Column<string>(nullable: true),
                    YearEstablished = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schools", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schools_SchoolCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "SchoolCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Schools_LocalGovernments_LgId",
                        column: x => x.LgId,
                        principalTable: "LocalGovernments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Schools_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PinHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SchoolId = table.Column<Guid>(nullable: true),
                    PinId = table.Column<Guid>(nullable: true),
                    DateActive = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PinHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PinHistories_Pins_PinId",
                        column: x => x.PinId,
                        principalTable: "Pins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PinHistories_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SchoolPayments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PinId = table.Column<Guid>(nullable: true),
                    SchoolId = table.Column<Guid>(nullable: true),
                    Amount = table.Column<decimal>(nullable: true),
                    ReceiptNo = table.Column<string>(nullable: true),
                    ReceiptImage = table.Column<byte[]>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    CreatedByNavigationId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SchoolPayments_Users_CreatedByNavigationId",
                        column: x => x.CreatedByNavigationId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SchoolPayments_Pins_PinId",
                        column: x => x.PinId,
                        principalTable: "Pins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SchoolPayments_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocalGovernments_StateId",
                table: "LocalGovernments",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_PinHistories_PinId",
                table: "PinHistories",
                column: "PinId");

            migrationBuilder.CreateIndex(
                name: "IX_PinHistories_SchoolId",
                table: "PinHistories",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Pins_CreatedByNavigationId",
                table: "Pins",
                column: "CreatedByNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_Pins_RecognitionTypeId",
                table: "Pins",
                column: "RecognitionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolPayments_CreatedByNavigationId",
                table: "SchoolPayments",
                column: "CreatedByNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolPayments_PinId",
                table: "SchoolPayments",
                column: "PinId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolPayments_SchoolId",
                table: "SchoolPayments",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Schools_CategoryId",
                table: "Schools",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Schools_LgId",
                table: "Schools",
                column: "LgId");

            migrationBuilder.CreateIndex(
                name: "IX_Schools_OfficeId",
                table: "Schools",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RankId",
                table: "Users",
                column: "RankId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PinHistories");

            migrationBuilder.DropTable(
                name: "SchoolPayments");

            migrationBuilder.DropTable(
                name: "Pins");

            migrationBuilder.DropTable(
                name: "Schools");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "RecognitionTypes");

            migrationBuilder.DropTable(
                name: "SchoolCategories");

            migrationBuilder.DropTable(
                name: "LocalGovernments");

            migrationBuilder.DropTable(
                name: "Offices");

            migrationBuilder.DropTable(
                name: "Ranks");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "States");
        }
    }
}
