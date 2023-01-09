using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EnginCan.Dal.Migrations
{
    public partial class Inital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LookupType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 150, nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LookupType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    ParentId = table.Column<int>(nullable: true),
                    Order = table.Column<short>(nullable: false),
                    Name = table.Column<string>(maxLength: 75, nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    AllName = table.Column<string>(maxLength: 250, nullable: true),
                    RouterLink = table.Column<string>(maxLength: 100, nullable: true),
                    AllRouterLink = table.Column<string>(maxLength: 250, nullable: true),
                    Icon = table.Column<string>(maxLength: 50, nullable: true),
                    WidgetIcon = table.Column<string>(maxLength: 50, nullable: true),
                    Color = table.Column<string>(maxLength: 10, nullable: true),
                    HomeWidget = table.Column<bool>(nullable: false),
                    MenuShow = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pages_Pages_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Pages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedUserId = table.Column<int>(nullable: true),
                    LastUpdatedUserId = table.Column<int>(nullable: true),
                    DataStatus = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedUserId = table.Column<int>(nullable: true),
                    LastUpdatedUserId = table.Column<int>(nullable: true),
                    DataStatus = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Surname = table.Column<string>(maxLength: 150, nullable: false),
                    FullName = table.Column<string>(maxLength: 250, nullable: false),
                    Username = table.Column<string>(maxLength: 50, nullable: true),
                    Photo = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lookup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedUserId = table.Column<int>(nullable: true),
                    LastUpdatedUserId = table.Column<int>(nullable: true),
                    DataStatus = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(nullable: true),
                    LookupTypeId = table.Column<int>(nullable: true),
                    ParentId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    BooleanValue1 = table.Column<bool>(nullable: true),
                    BooleanValue2 = table.Column<bool>(nullable: true),
                    LookupTypeId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lookup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lookup_LookupType_LookupTypeId1",
                        column: x => x.LookupTypeId1,
                        principalTable: "LookupType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lookup_Lookup_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Lookup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PagePermissions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedUserId = table.Column<int>(nullable: true),
                    LastUpdatedUserId = table.Column<int>(nullable: true),
                    DataStatus = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<int>(nullable: true),
                    RoleId = table.Column<int>(nullable: true),
                    PageId = table.Column<int>(nullable: false),
                    Forbidden = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PagePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PagePermissions_Pages_PageId",
                        column: x => x.PageId,
                        principalTable: "Pages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PagePermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PagePermissions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedUserId = table.Column<int>(nullable: true),
                    LastUpdatedUserId = table.Column<int>(nullable: true),
                    DataStatus = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSessions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    Token = table.Column<string>(maxLength: 1500, nullable: true),
                    RequestHeader = table.Column<string>(nullable: true),
                    RemoteIpAddress = table.Column<string>(maxLength: 30, nullable: true),
                    LoginAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSessions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "LookupType",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 8, null, "Cinsiyet" },
                    { 9, null, "Ülke" },
                    { 10, null, "İl" },
                    { 11, null, "İlçe" },
                    { 12, null, "Döviz" }
                });

            migrationBuilder.InsertData(
                table: "Pages",
                columns: new[] { "Id", "AllName", "AllRouterLink", "Color", "Description", "HomeWidget", "Icon", "MenuShow", "Name", "Order", "ParentId", "RouterLink", "WidgetIcon" },
                values: new object[] { 1, "Yönetim Paneli", "/yonetim", null, null, false, null, true, "Yönetim Paneli", (short)0, null, "/yonetim", null });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "CreatedUserId", "DataStatus", "LastUpdatedAt", "LastUpdatedUserId", "Name" },
                values: new object[] { 1, new DateTime(2020, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, null, null, "Admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "CreatedUserId", "DataStatus", "Email", "FullName", "LastUpdatedAt", "LastUpdatedUserId", "Name", "Password", "PhoneNumber", "Photo", "Surname", "Username" },
                values: new object[] { 1, new DateTime(2020, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, "admin@mail.com", "Yönetici Admin", null, null, "Yönetici", "9K7Cwg3Qw/8FR/S9VvrNdgl8znxhPagMZ4QrajV/3AQ=", null, null, "Admin", null });

            migrationBuilder.InsertData(
                table: "PagePermissions",
                columns: new[] { "Id", "CreatedAt", "CreatedUserId", "DataStatus", "Forbidden", "LastUpdatedAt", "LastUpdatedUserId", "PageId", "RoleId", "UserId" },
                values: new object[] { 1, new DateTime(2020, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, false, null, null, 1, 1, null });

            migrationBuilder.InsertData(
                table: "Pages",
                columns: new[] { "Id", "AllName", "AllRouterLink", "Color", "Description", "HomeWidget", "Icon", "MenuShow", "Name", "Order", "ParentId", "RouterLink", "WidgetIcon" },
                values: new object[,]
                {
                    { 2, "Yönetim Paneli / Ana Sayfa", "/yonetim/ana-sayfa", null, null, false, "fa fa-home", true, "Ana Sayfa", (short)0, 1, "/ana-sayfa", null },
                    { 3, "Yönetim Paneli / İdari İşler", "/yonetim/idari-isler", null, null, false, "fa fa-copy", true, "İdari İşler", (short)1, 1, "/idari-isler", null }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "CreatedAt", "CreatedUserId", "DataStatus", "LastUpdatedAt", "LastUpdatedUserId", "RoleId", "UserId" },
                values: new object[] { 1, new DateTime(2020, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, null, null, 1, 1 });

            migrationBuilder.InsertData(
                table: "PagePermissions",
                columns: new[] { "Id", "CreatedAt", "CreatedUserId", "DataStatus", "Forbidden", "LastUpdatedAt", "LastUpdatedUserId", "PageId", "RoleId", "UserId" },
                values: new object[,]
                {
                    { 2, new DateTime(2020, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, false, null, null, 2, 1, null },
                    { 3, new DateTime(2020, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, false, null, null, 3, 1, null }
                });

            migrationBuilder.InsertData(
                table: "Pages",
                columns: new[] { "Id", "AllName", "AllRouterLink", "Color", "Description", "HomeWidget", "Icon", "MenuShow", "Name", "Order", "ParentId", "RouterLink", "WidgetIcon" },
                values: new object[,]
                {
                    { 4, "Yönetim Paneli / İdari İşler/ Kullanıcı İşlemleri", "/yonetim/idari-isler/kullanici-islemleri", null, null, false, "fa fa-user", true, "Kullanıcı İşlemleri", (short)1, 3, "/kullanici-islemleri", null },
                    { 7, "Yönetim Paneli / İdari İşler/ Personel İşlemleri", "/yonetim/idari-isler/personel-islemleri", null, null, false, "fa fa-users", true, "Personel İşlemleri", (short)1, 3, "/personel-islemleri", null },
                    { 10, "Yönetim Paneli / İdari İşler / Organizasyon Kadro İşlemleri", "/yonetim/idari-isler/organizasyon-kadro-islemleri", null, null, false, "fa fa-sitemap", true, "Organizasyon Kadro İşlemleri", (short)1, 3, "/organizasyon-kadro-islemleri", null }
                });

            migrationBuilder.InsertData(
                table: "PagePermissions",
                columns: new[] { "Id", "CreatedAt", "CreatedUserId", "DataStatus", "Forbidden", "LastUpdatedAt", "LastUpdatedUserId", "PageId", "RoleId", "UserId" },
                values: new object[,]
                {
                    { 4, new DateTime(2020, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, false, null, null, 4, 1, null },
                    { 7, new DateTime(2020, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, false, null, null, 7, 1, null },
                    { 10, new DateTime(2020, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, false, null, null, 10, 1, null }
                });

            migrationBuilder.InsertData(
                table: "Pages",
                columns: new[] { "Id", "AllName", "AllRouterLink", "Color", "Description", "HomeWidget", "Icon", "MenuShow", "Name", "Order", "ParentId", "RouterLink", "WidgetIcon" },
                values: new object[,]
                {
                    { 5, "Yönetim Paneli / İdari İşler/ Kullanıcı İşlemleri / Tüm Kullanıcılar", "/yonetim/idari-isler/kullanici-islemleri/tum-kullanicilar", null, null, false, null, true, "Tüm Kullanıcılar", (short)0, 4, "/tum-kullanicilar", null },
                    { 6, "Yönetim Paneli / İdari İşler/ Kullanıcı İşlemleri / Yeni Kullanıcı", "/yonetim/idari-isler/kullanici-islemleri/tum-kullanicilar/yeni-kullanici", null, null, false, null, true, "Yeni Kullanıcı", (short)1, 4, "/yeni-kullanici", null },
                    { 8, "Yönetim Paneli / İdari İşler/ Personel İşlemleri / Tüm Personeller", "/yonetim/idari-isler/personel-islemleri/tum-personeller", null, null, false, null, true, "Tüm Kullanıcılar", (short)0, 7, "/tum-personeller", null },
                    { 9, "Yönetim Paneli / İdari İşler/ Personel İşlemleri / Yeni Personel", "/yonetim/idari-isler/personel-islemleri/yeni-personel", null, null, false, null, true, "Yeni Kullanıcı", (short)1, 7, "/yeni-personel", null },
                    { 11, "Yönetim Paneli / İdari İşler/ Organizasyon Kadro İşlemleri / Organizasyon Şeması", "/yonetim/idari-isler/organizasyon-kadro-islemleri/organizasyon-semasi", null, null, false, null, true, "Organizasyon Şeması", (short)1, 10, "/organizasyon-semasi", null },
                    { 12, "Yönetim Paneli / İdari İşler/ Organizasyon Kadro İşlemleri / Yeni Birim", "/yonetim/idari-isler/organizasyon-kadro-islemleri/yeni-birim", null, null, false, null, true, "Yeni Birim", (short)1, 10, "/yeni-birim", null }
                });

            migrationBuilder.InsertData(
                table: "PagePermissions",
                columns: new[] { "Id", "CreatedAt", "CreatedUserId", "DataStatus", "Forbidden", "LastUpdatedAt", "LastUpdatedUserId", "PageId", "RoleId", "UserId" },
                values: new object[,]
                {
                    { 5, new DateTime(2020, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, false, null, null, 5, 1, null },
                    { 6, new DateTime(2020, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, false, null, null, 6, 1, null },
                    { 8, new DateTime(2020, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, false, null, null, 8, 1, null },
                    { 9, new DateTime(2020, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, false, null, null, 9, 1, null },
                    { 11, new DateTime(2020, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, false, null, null, 11, 1, null },
                    { 12, new DateTime(2020, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, false, null, null, 12, 1, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lookup_LookupTypeId1",
                table: "Lookup",
                column: "LookupTypeId1");

            migrationBuilder.CreateIndex(
                name: "IX_Lookup_ParentId",
                table: "Lookup",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_PagePermissions_PageId",
                table: "PagePermissions",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_PagePermissions_RoleId",
                table: "PagePermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_PagePermissions_UserId",
                table: "PagePermissions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_ParentId",
                table: "Pages",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSessions_UserId",
                table: "UserSessions",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lookup");

            migrationBuilder.DropTable(
                name: "PagePermissions");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserSessions");

            migrationBuilder.DropTable(
                name: "LookupType");

            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
