using FluentMigrator;
using static IDM.EmployeeService.Infractructure.Database.DatabaseSchemeInfo;

namespace IDM.EmployeeService.Migrator.Migrations
{
    [Migration(3)]
    public class OutboxMessageTable: Migration
    {
        public override void Up()
        {
            if (!Schema.Schema("public").Table(OutboxMessagesTable.TableName).Exists())
            {
                Create.Table(OutboxMessagesTable.TableName)
                    .WithColumn(OutboxMessagesTable.id).AsInt32().Identity().PrimaryKey()
                    .WithColumn(OutboxMessagesTable.key).AsString(100).NotNullable()
                    .WithColumn(OutboxMessagesTable.eventtype).AsString(200).NotNullable()
                    .WithColumn(OutboxMessagesTable.payload).AsString().NotNullable()
                    .WithColumn(OutboxMessagesTable.createdat).AsDateTimeOffset().NotNullable()
                    .WithColumn(OutboxMessagesTable.failed).AsBoolean()
                    .WithColumn(OutboxMessagesTable.retrycount).AsInt32().NotNullable()
                    .WithColumn(OutboxMessagesTable.retryafter).AsDateTimeOffset().Nullable()
                    .WithColumn(OutboxMessagesTable.processedat).AsDateTimeOffset().Nullable();
            }
        }

        public override void Down()
        {
            if (Schema.Schema("public").Table(OutboxMessagesTable.TableName).Exists())
            {
                Delete.Table(OutboxMessagesTable.TableName);
            }
        }
    }
}
