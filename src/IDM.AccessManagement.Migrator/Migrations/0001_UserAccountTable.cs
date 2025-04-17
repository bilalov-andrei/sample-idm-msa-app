using FluentMigrator;
using static IDM.AccessManagement.Infractructure.Database.DatabaseSchemeInfo;

namespace IDM.EmployeeService.Migrator.Migrations
{
    [Migration(1)]
    public class UserAccountTable: Migration
    {
        public override void Up()
        {
            if (!Schema.Schema("public").Table(UserAccountsTable.TableName).Exists())
            {
                Create.Table(UserAccountsTable.TableName)
                    .WithColumn(UserAccountsTable.id).AsInt32().Identity().PrimaryKey()
                    .WithColumn(UserAccountsTable.system_id).AsInt32().NotNullable()
                    .WithColumn(UserAccountsTable.employee_id).AsInt32().NotNullable()
                    .WithColumn(UserAccountsTable.created_date).AsDateTimeOffset().NotNullable()
                    .WithColumn(UserAccountsTable.revoked_date).AsDateTimeOffset().Nullable()
                    .WithColumn(UserAccountsTable.rights).AsString().Nullable()
                    .WithColumn(UserAccountsTable.status_id).AsInt32().NotNullable();
            }
        }

        public override void Down()
        {
            if (Schema.Schema("public").Table(UserAccountsTable.TableName).Exists())
            {
                Delete.Table(UserAccountsTable.TableName);
            }
        }
    }
}
