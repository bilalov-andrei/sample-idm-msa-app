using FluentMigrator;
using static IDM.AccessManagement.Infractructure.Database.DatabaseSchemeInfo;

namespace OzonEdu.MerchandiseService.Migrator.Migrations
{
    [Migration(2)]
    public class UserAccountSystemIdEmployeeIdIdx : ForwardOnlyMigration
    {
        public override void Up()
        {
            if (Schema.Schema("public").Table(UserAccountsTable.TableName).Exists())
            {
                Create.Index("useraccounts_system_id_employee_id_idx")
                    .OnTable(UserAccountsTable.TableName)
                    .InSchema("public")
                    .OnColumn(UserAccountsTable.system_id).Ascending()
                    .OnColumn(UserAccountsTable.employee_id)
                    .Unique();
            }
        }
    }
}
