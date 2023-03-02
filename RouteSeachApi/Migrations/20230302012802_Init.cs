using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RouteSeachApi.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Origin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OriginDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DestinationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TimeLimit = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.Id);
                });

            migrationBuilder.Sql(@"
                begin 
	                insert into [dbo].[Routes] ([Id], [Origin], [Destination], [OriginDateTime], [DestinationDateTime], [Price], [TimeLimit])
	                values (NEWID(), 'Moscow', 'Tombow', '2023-05-04 13:00:00', '2023-05-04 18:00:00', 7000, '2023-05-04 23:00:00')

                    insert into [dbo].[Routes] ([Id], [Origin], [Destination], [OriginDateTime], [DestinationDateTime], [Price], [TimeLimit])
	                values (NEWID(), 'Saint-Petersburg', 'Moscow', '2023-04-14 17:00:00', '2023-04-14 19:30:00', 4000, '2023-04-14 23:00:00')

                    insert into [dbo].[Routes] ([Id], [Origin], [Destination], [OriginDateTime], [DestinationDateTime], [Price], [TimeLimit])
	                values (NEWID(), 'Saint-Petersburg', 'Helsinki', '2023-04-25 13:00:00', '2023-04-25 15:30:00', 11000, '2023-04-25 23:00:00')

                    insert into [dbo].[Routes] ([Id], [Origin], [Destination], [OriginDateTime], [DestinationDateTime], [Price], [TimeLimit])
	                values (NEWID(), 'Saint-Petersburg', 'London', '2023-05-01 12:30:00', '2023-05-01 16:30:00', 21000, '2023-05-01 23:00:00')

                    insert into [dbo].[Routes] ([Id], [Origin], [Destination], [OriginDateTime], [DestinationDateTime], [Price], [TimeLimit])
	                values (NEWID(), 'London', 'Moscow', '2023-05-01 11:30:00', '2023-05-01 16:30:00', 20000, '2023-05-01 23:00:00')

                    insert into [dbo].[Routes] ([Id], [Origin], [Destination], [OriginDateTime], [DestinationDateTime], [Price], [TimeLimit])
	                values (NEWID(), 'Saint-Petersburg', 'Moscow', '2023-04-28 13:00:00', '2023-04-28 14:30:00', 11000, '2023-04-28 23:00:00')

                    insert into [dbo].[Routes] ([Id], [Origin], [Destination], [OriginDateTime], [DestinationDateTime], [Price], [TimeLimit])
	                values (NEWID(), 'Moscow', 'Saint-Petersburg', '2023-04-28 15:00:00', '2023-04-28 16:30:00', 11000, '2023-04-28 23:00:00')

                    insert into [dbo].[Routes] ([Id], [Origin], [Destination], [OriginDateTime], [DestinationDateTime], [Price], [TimeLimit])
	                values (NEWID(), 'Saint-Petersburg', 'Krasnodar', '2023-04-25 13:00:00', '2023-04-25 15:30:00', 11000, '2023-04-25 23:00:00')
                    
                end
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Routes");
        }
    }
}
