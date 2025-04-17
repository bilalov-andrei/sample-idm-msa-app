using FluentMigrator;
using static IDM.EmployeeService.Infractructure.Database.DatabaseSchemeInfo;

namespace OzonEdu.MerchandiseService.Migrator.Migrations
{
    [Migration(2)]
    public class EmployeeEmailIdx : ForwardOnlyMigration
    {
        public override void Up()
        {
            if (Schema.Schema("public").Table(EmployeesTable.TableName).Exists())
            {
                Create.Index("employee_email_id_idx")
                    .OnTable(EmployeesTable.TableName)
                    .InSchema("public")
                    .OnColumn(EmployeesTable.email);
            }
        }
    }
}
