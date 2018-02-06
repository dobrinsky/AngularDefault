using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RaportareOTR.Data.Migrations
{
    public partial class addSuperUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [dbo].[AspNetRoles] ([Id], [ConcurrencyStamp], [Name], [NormalizedName]) VALUES (N'e497902e-9969-43e6-9b60-ae81f861072f', N'7305e555-a630-4e56-bb50-f8862a222c7b', N'OTR', N'OTR')");
            migrationBuilder.Sql("INSERT INTO [dbo].[AspNetUsers] ([Id], [AccessFailedCount], [ConcurrencyStamp], [Email], [EmailConfirmed], [LockoutEnabled], [LockoutEnd], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [SecurityStamp], [TwoFactorEnabled], [UserName], [LastActiveDate], [FirstName], [LastName], [CIF]) VALUES (N'2fd7f359-6704-4e64-9899-8df63544d252', 0, N'3c86f860-f1ca-4105-b75e-c794afbe5286', N'office@snx.ro', 0, 1, NULL, N'OFFICE@SNX.RO', N'ADMIN', N'AQAAAAEAACcQAAAAEHsoFb+ab+DxIVj7M2qTc6gizyL36hAnkOucHvQ8eUYx0qNr1JNnLxYhPABt3QKSaA==', NULL, 0, N'7126fd7f-105d-4bf0-ad64-b81cf34e7251', 0, N'admin', N'2018-02-03 16:32:52', NULL, NULL, NULL)");
            migrationBuilder.Sql("INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'2fd7f359-6704-4e64-9899-8df63544d252', N'e497902e-9969-43e6-9b60-ae81f861072f')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
