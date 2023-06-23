using CyberspawnServer.Migrations.Entities;
using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberspawnServer.Migrations.Migrations
{
    [Migration(202306230002)]
    public class InitialSeedMigration_202306230002 : Migration
    {
            public override void Down()
            {
                Delete.FromTable("Messages")
                    .Row(new Messages
                    {
                        SenderId = "#59c0d403",
                        ReceiveId = "#b0e54e7c",
                        Content = "How far!",
                        ChatRoomId = new Guid("67fbac34-1ee1-4697-b916-1748861dd275"),
                        MediaUrl = "https://pexels.com/wonder.png",
                        Timestamp = DateTime.UtcNow

                    });

                Delete.FromTable("ChatRoom")
                    .Row(new ChatRoom
                    {
                        Id = new Guid("67fbac34-1ee1-4697-b916-1748861dd275"),
                        Name = "Test Address",
                        UpdatedAt = "USA",
                        CreatedAt = "Test name"
                    });
            }

            public override void Up()
            {
                Insert.IntoTable("Messages")
                    .Row(new Messages
                    {
                        SenderId = "#59c0d403",
                        ReceiveId = "#b0e54e7c",
                        Content = "How far!",
                        ChatRoomId = new Guid("67fbac34-1ee1-4697-b916-1748861dd275"),
                        MediaUrl = "https://pexels.com/wonder.png",
                        Timestamp = DateTime.UtcNow

                    });
                Insert.IntoTable("ChatRoom")
                    .Row(new ChatRoom
                    {
                        Id = new Guid("67fbac34-1ee1-4697-b916-1748861dd275"),
                        Name = "Test Address",
                        UpdatedAt = "USA",
                        CreatedAt = "Test name"
                    });
            }
    }
}


