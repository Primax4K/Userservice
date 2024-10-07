using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Model.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "APPLICATIONS",
                columns: table => new
                {
                    APPLICATIONID = table.Column<int>(name: "APPLICATION_ID", type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    APPLICATIONNAME = table.Column<string>(name: "APPLICATION_NAME", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    APPLICATIONURL = table.Column<string>(name: "APPLICATION_URL", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PORT = table.Column<int>(type: "int", nullable: false),
                    APPLICATIONKEY = table.Column<string>(name: "APPLICATION_KEY", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APPLICATIONS", x => x.APPLICATIONID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "COUNTRIES",
                columns: table => new
                {
                    COUNTRYID = table.Column<int>(name: "COUNTRY_ID", type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NAME = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COUNTRIES", x => x.COUNTRYID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GENDERS",
                columns: table => new
                {
                    GENDERID = table.Column<int>(name: "GENDER_ID", type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    GENDERNAME = table.Column<string>(name: "GENDER_NAME", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GENDERS", x => x.GENDERID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LOGIN_USER_DATA",
                columns: table => new
                {
                    USERID = table.Column<int>(name: "USER_ID", type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EMAIL = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FIRSTNAME = table.Column<string>(name: "FIRST_NAME", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LASTNAME = table.Column<string>(name: "LAST_NAME", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CREATEDAT = table.Column<DateTime>(name: "CREATED_AT", type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOGIN_USER_DATA", x => x.USERID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ROLES",
                columns: table => new
                {
                    ROLEID = table.Column<int>(name: "ROLE_ID", type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ROLENAME = table.Column<string>(name: "ROLE_NAME", type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROLES", x => x.ROLEID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "STATES",
                columns: table => new
                {
                    STATEID = table.Column<int>(name: "STATE_ID", type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NAME = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    COUNTRYID = table.Column<int>(name: "COUNTRY_ID", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STATES", x => x.STATEID);
                    table.ForeignKey(
                        name: "FK_STATES_COUNTRIES_COUNTRY_ID",
                        column: x => x.COUNTRYID,
                        principalTable: "COUNTRIES",
                        principalColumn: "COUNTRY_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "REGISTERED_USERS",
                columns: table => new
                {
                    REGISTEREDUSERID = table.Column<int>(name: "REGISTERED_USER_ID", type: "int", nullable: false),
                    PASSWORDHASH = table.Column<string>(name: "PASSWORD_HASH", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ISLOCKED = table.Column<bool>(name: "IS_LOCKED", type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REGISTERED_USERS", x => x.REGISTEREDUSERID);
                    table.ForeignKey(
                        name: "FK_REGISTERED_USERS_LOGIN_USER_DATA_REGISTERED_USER_ID",
                        column: x => x.REGISTEREDUSERID,
                        principalTable: "LOGIN_USER_DATA",
                        principalColumn: "USER_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "APPLICATIONS_HAS_ROLES_JT",
                columns: table => new
                {
                    ROLEID = table.Column<int>(name: "ROLE_ID", type: "int", nullable: false),
                    APPLICATIONID = table.Column<int>(name: "APPLICATION_ID", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APPLICATIONS_HAS_ROLES_JT", x => new { x.APPLICATIONID, x.ROLEID });
                    table.ForeignKey(
                        name: "FK_APPLICATIONS_HAS_ROLES_JT_APPLICATIONS_APPLICATION_ID",
                        column: x => x.APPLICATIONID,
                        principalTable: "APPLICATIONS",
                        principalColumn: "APPLICATION_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_APPLICATIONS_HAS_ROLES_JT_ROLES_ROLE_ID",
                        column: x => x.ROLEID,
                        principalTable: "ROLES",
                        principalColumn: "ROLE_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ADDRESSES",
                columns: table => new
                {
                    ADDRESSID = table.Column<int>(name: "ADDRESS_ID", type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    STATEID = table.Column<int>(name: "STATE_ID", type: "int", nullable: false),
                    STREET = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ZIPCODE = table.Column<int>(name: "ZIP_CODE", type: "int", nullable: false),
                    LOCATION = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HOUSENUMBER = table.Column<string>(name: "HOUSE_NUMBER", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADDRESSES", x => x.ADDRESSID);
                    table.ForeignKey(
                        name: "FK_ADDRESSES_STATES_STATE_ID",
                        column: x => x.STATEID,
                        principalTable: "STATES",
                        principalColumn: "STATE_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LOGIN_ATTEMPTS",
                columns: table => new
                {
                    ATTEMPTID = table.Column<int>(name: "ATTEMPT_ID", type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ATTEMPTEDDATE = table.Column<DateTime>(name: "ATTEMPTED_DATE", type: "datetime(6)", nullable: false),
                    IPADDRESS = table.Column<string>(name: "IP_ADDRESS", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    REGISTEREDUSERID = table.Column<int>(name: "REGISTERED_USER_ID", type: "int", nullable: false),
                    ATTEMPTSTATUS = table.Column<string>(name: "ATTEMPT_STATUS", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    APPLICATIONID = table.Column<int>(name: "APPLICATION_ID", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOGIN_ATTEMPTS", x => x.ATTEMPTID);
                    table.ForeignKey(
                        name: "FK_LOGIN_ATTEMPTS_APPLICATIONS_APPLICATION_ID",
                        column: x => x.APPLICATIONID,
                        principalTable: "APPLICATIONS",
                        principalColumn: "APPLICATION_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LOGIN_ATTEMPTS_REGISTERED_USERS_REGISTERED_USER_ID",
                        column: x => x.REGISTEREDUSERID,
                        principalTable: "REGISTERED_USERS",
                        principalColumn: "REGISTERED_USER_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SESSIONS",
                columns: table => new
                {
                    SESSIONID = table.Column<int>(name: "SESSION_ID", type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SESSIONTOKEN = table.Column<string>(name: "SESSION_TOKEN", type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CREATEDAT = table.Column<DateTime>(name: "CREATED_AT", type: "datetime(6)", nullable: false),
                    VALIDUNTIL = table.Column<DateTime>(name: "VALID_UNTIL", type: "datetime(6)", nullable: false),
                    USERID = table.Column<int>(name: "USER_ID", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SESSIONS", x => x.SESSIONID);
                    table.ForeignKey(
                        name: "FK_SESSIONS_REGISTERED_USERS_USER_ID",
                        column: x => x.USERID,
                        principalTable: "REGISTERED_USERS",
                        principalColumn: "REGISTERED_USER_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ROLES_has_REGISTERED_USERS",
                columns: table => new
                {
                    REGISTEREDUSERID = table.Column<int>(name: "REGISTERED_USER_ID", type: "int", nullable: false),
                    ROLEID = table.Column<int>(name: "ROLE_ID", type: "int", nullable: false),
                    APPLICATIONID = table.Column<int>(name: "APPLICATION_ID", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROLES_has_REGISTERED_USERS", x => new { x.REGISTEREDUSERID, x.ROLEID, x.APPLICATIONID });
                    table.ForeignKey(
                        name: "FK_ROLES_has_REGISTERED_USERS_APPLICATIONS_HAS_ROLES_JT_APPLICA~",
                        columns: x => new { x.APPLICATIONID, x.ROLEID },
                        principalTable: "APPLICATIONS_HAS_ROLES_JT",
                        principalColumns: new[] { "APPLICATION_ID", "ROLE_ID" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ROLES_has_REGISTERED_USERS_REGISTERED_USERS_REGISTERED_USER_~",
                        column: x => x.REGISTEREDUSERID,
                        principalTable: "REGISTERED_USERS",
                        principalColumn: "REGISTERED_USER_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "USERS",
                columns: table => new
                {
                    USERID = table.Column<int>(name: "USER_ID", type: "int", nullable: false),
                    ADDRESSID = table.Column<int>(name: "ADDRESS_ID", type: "int", nullable: false),
                    PRECEDINGTITLE = table.Column<string>(name: "PRECEDING_TITLE", type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SUBSEQUENTTITLE = table.Column<string>(name: "SUBSEQUENT_TITLE", type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GENDERID = table.Column<int>(name: "GENDER_ID", type: "int", nullable: false),
                    BIRTHDATE = table.Column<DateOnly>(name: "BIRTH_DATE", type: "date", nullable: false),
                    BIRTHPLACE = table.Column<string>(name: "BIRTH_PLACE", type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SALUTATION = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TELEPHONE = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NOTES = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.USERID);
                    table.ForeignKey(
                        name: "FK_USERS_ADDRESSES_ADDRESS_ID",
                        column: x => x.ADDRESSID,
                        principalTable: "ADDRESSES",
                        principalColumn: "ADDRESS_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_USERS_GENDERS_GENDER_ID",
                        column: x => x.GENDERID,
                        principalTable: "GENDERS",
                        principalColumn: "GENDER_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_USERS_REGISTERED_USERS_USER_ID",
                        column: x => x.USERID,
                        principalTable: "REGISTERED_USERS",
                        principalColumn: "REGISTERED_USER_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "COUNTRIES",
                columns: new[] { "COUNTRY_ID", "NAME" },
                values: new object[] { 1, "Österreich" });

            migrationBuilder.InsertData(
                table: "GENDERS",
                columns: new[] { "GENDER_ID", "GENDER_NAME" },
                values: new object[,]
                {
                    { 1, "Männlich" },
                    { 2, "Weiblich" }
                });

            migrationBuilder.InsertData(
                table: "ROLES",
                columns: new[] { "ROLE_ID", "ROLE_NAME" },
                values: new object[,]
                {
                    { 1, "Patient" },
                    { 2, "Assistant" },
                    { 3, "Administrator" }
                });

            migrationBuilder.InsertData(
                table: "STATES",
                columns: new[] { "STATE_ID", "COUNTRY_ID", "NAME" },
                values: new object[,]
                {
                    { 1, 1, "Niederösterreich" },
                    { 2, 1, "Wien" },
                    { 3, 1, "Burgenland" },
                    { 4, 1, "Oberösterreich" },
                    { 5, 1, "Steiermark" },
                    { 6, 1, "Kärnten" },
                    { 7, 1, "Salzburg" },
                    { 8, 1, "Tirol" },
                    { 9, 1, "Vorarlberg" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ADDRESSES_STATE_ID",
                table: "ADDRESSES",
                column: "STATE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_APPLICATIONS_HAS_ROLES_JT_ROLE_ID",
                table: "APPLICATIONS_HAS_ROLES_JT",
                column: "ROLE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_LOGIN_ATTEMPTS_APPLICATION_ID",
                table: "LOGIN_ATTEMPTS",
                column: "APPLICATION_ID");

            migrationBuilder.CreateIndex(
                name: "IX_LOGIN_ATTEMPTS_REGISTERED_USER_ID",
                table: "LOGIN_ATTEMPTS",
                column: "REGISTERED_USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_LOGIN_USER_DATA_EMAIL",
                table: "LOGIN_USER_DATA",
                column: "EMAIL",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ROLES_ROLE_NAME",
                table: "ROLES",
                column: "ROLE_NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ROLES_has_REGISTERED_USERS_APPLICATION_ID_ROLE_ID",
                table: "ROLES_has_REGISTERED_USERS",
                columns: new[] { "APPLICATION_ID", "ROLE_ID" });

            migrationBuilder.CreateIndex(
                name: "IX_SESSIONS_USER_ID",
                table: "SESSIONS",
                column: "USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_STATES_COUNTRY_ID",
                table: "STATES",
                column: "COUNTRY_ID");

            migrationBuilder.CreateIndex(
                name: "IX_USERS_ADDRESS_ID",
                table: "USERS",
                column: "ADDRESS_ID");

            migrationBuilder.CreateIndex(
                name: "IX_USERS_GENDER_ID",
                table: "USERS",
                column: "GENDER_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LOGIN_ATTEMPTS");

            migrationBuilder.DropTable(
                name: "ROLES_has_REGISTERED_USERS");

            migrationBuilder.DropTable(
                name: "SESSIONS");

            migrationBuilder.DropTable(
                name: "USERS");

            migrationBuilder.DropTable(
                name: "APPLICATIONS_HAS_ROLES_JT");

            migrationBuilder.DropTable(
                name: "ADDRESSES");

            migrationBuilder.DropTable(
                name: "GENDERS");

            migrationBuilder.DropTable(
                name: "REGISTERED_USERS");

            migrationBuilder.DropTable(
                name: "APPLICATIONS");

            migrationBuilder.DropTable(
                name: "ROLES");

            migrationBuilder.DropTable(
                name: "STATES");

            migrationBuilder.DropTable(
                name: "LOGIN_USER_DATA");

            migrationBuilder.DropTable(
                name: "COUNTRIES");
        }
    }
}
