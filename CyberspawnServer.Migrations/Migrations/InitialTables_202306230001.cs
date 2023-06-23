using FluentMigrator;
using FluentMigrator.Postgres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberspawnServer.Migrations.Migrations
{
    [Migration(202306230001)]

    public class InitialTables_202306230001 : Migration
    {

            public override void Down()
            {
                Delete.Table("Companies");
                Delete.Table("Employee");
            }

            public override void Up()
            {
                Create.Table("Messages")
                    .WithColumn("Id").AsInt64().NotNullable().PrimaryKey().Identity()
                    .WithColumn("SenderId").AsString(50).NotNullable()
                    .WithColumn("ReceiveId").AsString(50).NotNullable()
                    .WithColumn("Content").AsString(50).NotNullable()
                    .WithColumn("ChatRoomId").AsGuid().NotNullable()
                    .WithColumn("MediaUrl").AsString(50).NotNullable()
                    .WithColumn("Timestamp").AsDateTime().NotNullable();

                Create.Table("Employee")
                        .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                        .WithColumn("Name").AsString(50).NotNullable()
                        .WithColumn("UserIds").AsString(int.MaxValue).NotNullable()
                        .WithColumn("UpdatedAt").AsDateTime().NotNullable()
                        .WithColumn("CreatedAt").AsDateTime().NotNullable();


        }
    }

    }
}
