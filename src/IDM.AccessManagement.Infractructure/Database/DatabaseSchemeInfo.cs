namespace IDM.AccessManagement.Infractructure.Database
{
    public static class DatabaseSchemeInfo
    {
        public static class UserAccountsTable
        {
            public const string TableName = "useraccounts";

            #region Columns

            public const string id = nameof(id);
            public const string system_id = nameof(system_id);
            public const string employee_id = nameof(employee_id);
            public const string status_id = nameof(status_id);
            public const string created_date = nameof(created_date);
            public const string revoked_date = nameof(revoked_date);
            public const string rights = nameof(rights);

            #endregion
        }
    }
}
