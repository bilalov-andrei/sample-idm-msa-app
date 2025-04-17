using FluentMigrator;
using static IDM.EmployeeService.Infractructure.Database.DatabaseSchemeInfo;

namespace IDM.EmployeeService.Migrator.Migrations
{
    [Migration(1)]
    public class EmployeeTable: Migration
    {
        public override void Up()
        {
            if (!Schema.Schema("public").Table(EmployeesTable.TableName).Exists())
            {
                Create.Table(EmployeesTable.TableName)
                    .WithColumn(EmployeesTable.id).AsInt32().Identity().PrimaryKey()
                    .WithColumn(EmployeesTable.email).AsString(50).NotNullable()
                    .WithColumn(EmployeesTable.fullname_firstname).AsString(50).NotNullable()
                    .WithColumn(EmployeesTable.fullname_lastname).AsString(50).NotNullable()
                    .WithColumn(EmployeesTable.fullname_middlename).AsString(50).Nullable()
                    .WithColumn(EmployeesTable.position).AsString(50).NotNullable()
                    .WithColumn(EmployeesTable.status_id).AsInt32().NotNullable()
                    .WithColumn(EmployeesTable.hire_date).AsDateTimeOffset().NotNullable()
                    .WithColumn(EmployeesTable.dismissal_date).AsDateTimeOffset().Nullable();
            }
        }

        public override void Down()
        {
            if (Schema.Schema("public").Table(EmployeesTable.TableName).Exists())
            {
                Delete.Table(EmployeesTable.TableName);
            }
        }
    }
}
