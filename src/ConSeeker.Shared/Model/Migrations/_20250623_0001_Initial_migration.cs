using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConSeeker.Shared.Model.Migrations
{
    [Migration(20250623_0001)]
    public class _20250623_0001_Initial_migration : Migration
    {
        public override void Down()
        {
            Delete.Table("CHANNELS");
        }

        public override void Up()
        {
            Create.Table("CHANNELS")
                .WithColumn("ID").AsInt64().PrimaryKey().Identity()
                .WithColumn("LINK").AsString().NotNullable()
                .WithColumn("LASTREAD").AsDateTime()
                .WithColumn("LASTUPDATE").AsDateTime()
                .WithColumn("CONTENT").AsString();

            Create.Table("CONVENTIONS")
                .WithColumn("ID").AsInt64().PrimaryKey().Identity()
                .WithColumn("CHANNEL_ID").AsInt64().NotNullable()
                .WithColumn("NAME").AsString().NotNullable();

            Create.ForeignKey("FK_CONVENTION_CHANNEL")
                .FromTable("CONVENTIONS").ForeignColumn("CHANNEL_ID")
                .ToTable("CHANNEL").PrimaryColumn("ID");
        }
    }
}
