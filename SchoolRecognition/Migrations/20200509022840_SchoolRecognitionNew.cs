using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolRecognition.Migrations
{
    public partial class SchoolRecognitionNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Offices",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Address = table.Column<string>(maxLength: 50, nullable: true),
                    StateID = table.Column<Guid>(nullable: true),
                    DateCreated = table.Column<DateTime>(type: "date", nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offices", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Ranks",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Code = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ranks", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RecognitionTypes",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Code = table.Column<string>(unicode: false, maxLength: 3, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecognitionTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    IsDefault = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsAdmin = table.Column<bool>(nullable: false),
                    IsSuperAdmin = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SchoolCategories",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Code = table.Column<string>(maxLength: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolCategories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Code = table.Column<string>(maxLength: 3, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Titles",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Titles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    Surname = table.Column<string>(maxLength: 50, nullable: true),
                    Othernames = table.Column<string>(maxLength: 50, nullable: true),
                    Password = table.Column<byte[]>(nullable: true),
                    RankID = table.Column<Guid>(nullable: true),
                    RoleID = table.Column<Guid>(nullable: true),
                    EmailAddress = table.Column<string>(maxLength: 50, nullable: true),
                    PhoneNo = table.Column<string>(maxLength: 20, nullable: true),
                    LPNO = table.Column<string>(maxLength: 30, nullable: true),
                    Signature = table.Column<byte[]>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsVerified = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                    table.ForeignKey(
                        name: "FK_User_Rank",
                        column: x => x.RankID,
                        principalTable: "Ranks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_Role",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LocalGovernments",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Code = table.Column<string>(maxLength: 3, nullable: true),
                    StateID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalGovernments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LocalGovernments_State",
                        column: x => x.StateID,
                        principalTable: "States",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pins",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    RecognitionTypeID = table.Column<Guid>(nullable: true),
                    SerialPin = table.Column<string>(maxLength: 25, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsInUse = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: true),
                    DateCreated = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pins", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PIN_User",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PIN_RecognitionType",
                        column: x => x.RecognitionTypeID,
                        principalTable: "RecognitionTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Schools",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    CategoryID = table.Column<Guid>(nullable: true),
                    OfficeID = table.Column<Guid>(nullable: true),
                    LgID = table.Column<Guid>(nullable: true),
                    Address = table.Column<string>(maxLength: 50, nullable: true),
                    EmailAddress = table.Column<string>(maxLength: 50, nullable: true),
                    PhoneNo = table.Column<string>(maxLength: 20, nullable: true),
                    YearEstablished = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schools", x => x.ID);
                    table.ForeignKey(
                        name: "FK_School_SchoolCategory",
                        column: x => x.CategoryID,
                        principalTable: "SchoolCategories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_School_LocalGovernment",
                        column: x => x.LgID,
                        principalTable: "LocalGovernments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_School_Office",
                        column: x => x.OfficeID,
                        principalTable: "Offices",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PinHistories",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    SchoolID = table.Column<Guid>(nullable: true),
                    PinID = table.Column<Guid>(nullable: true),
                    DateActive = table.Column<DateTime>(type: "date", nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PinHistories", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PinHistory_PIN",
                        column: x => x.PinID,
                        principalTable: "Pins",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PinHistory_School",
                        column: x => x.SchoolID,
                        principalTable: "Schools",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SchoolPayments",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    PinID = table.Column<Guid>(nullable: true),
                    SchoolID = table.Column<Guid>(nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18, 0)", nullable: true),
                    ReceiptNo = table.Column<string>(maxLength: 50, nullable: true),
                    ReceiptImage = table.Column<byte[]>(nullable: true),
                    DateCreated = table.Column<DateTime>(type: "date", nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolPayments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SchoolPayment_User",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SchoolPayment_PIN",
                        column: x => x.PinID,
                        principalTable: "Pins",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SchoolPayment_School",
                        column: x => x.SchoolID,
                        principalTable: "Schools",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocalGovernments_StateID",
                table: "LocalGovernments",
                column: "StateID");

            migrationBuilder.CreateIndex(
                name: "IX_PinHistories_PinID",
                table: "PinHistories",
                column: "PinID");

            migrationBuilder.CreateIndex(
                name: "IX_PinHistories_SchoolID",
                table: "PinHistories",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_Pins_CreatedBy",
                table: "Pins",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Pins_RecognitionTypeID",
                table: "Pins",
                column: "RecognitionTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolPayments_CreatedBy",
                table: "SchoolPayments",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolPayments_PinID",
                table: "SchoolPayments",
                column: "PinID");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolPayments_SchoolID",
                table: "SchoolPayments",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_Schools_CategoryID",
                table: "Schools",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Schools_LgID",
                table: "Schools",
                column: "LgID");

            migrationBuilder.CreateIndex(
                name: "IX_Schools_OfficeID",
                table: "Schools",
                column: "OfficeID");

            migrationBuilder.CreateIndex(
                name: "IX_State",
                table: "States",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RankID",
                table: "Users",
                column: "RankID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleID",
                table: "Users",
                column: "RoleID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PinHistories");

            migrationBuilder.DropTable(
                name: "SchoolPayments");

            migrationBuilder.DropTable(
                name: "Titles");

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
