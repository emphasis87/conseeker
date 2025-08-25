using FluentMigrator;

namespace ConSeeker.Shared.Model.Migrations
{
    [Migration(20250623120000)]
    public class InitialMigration : Migration
    {
        public override void Down()
        {
            Delete.ForeignKey("FK_PROVIDERS_CONVENTIONS");

            Delete.Table("PROVIDERS");
            Delete.Table("CONVENTIONS");
            Delete.Table("PROFILES");
        }

        public override void Up()
        {
            Create.Table("PROVIDERS")
                .WithColumn("ID").AsInt64().PrimaryKey().Identity()
                .WithColumn("SOURCE_URL").AsString().NotNullable()
                .WithColumn("LAST_CHECKED").AsDateTime()
                .WithColumn("LAST_CHANGED").AsDateTime()
                .WithColumn("CONTENT").AsString();

            Create.Table("CONVENTIONS")
                .WithColumn("ID").AsInt64().PrimaryKey().Identity()
                .WithColumn("PROVIDER_ID").AsInt64().NotNullable()
                .WithColumn("NAME").AsString()
                .WithColumn("DESCRIPTION").AsString()
                .WithColumn("BEGIN").AsDateTime()
                .WithColumn("END").AsDateTime();

            Create.ForeignKey("FK_PROVIDERS_CONVENTIONS")
                .FromTable("CONVENTIONS").ForeignColumn("PROVIDER_ID")
                .ToTable("PROVIDERS").PrimaryColumn("ID");

            Create.Table("PROFILES")
                .WithColumn("ID").AsInt64().PrimaryKey().Identity()
                .WithColumn("NAME").AsString().NotNullable()
                .WithColumn("ALIASES").AsString()
                .WithColumn("BIO").AsString();
        }
    }
}
