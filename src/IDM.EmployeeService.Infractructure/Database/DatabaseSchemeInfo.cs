namespace IDM.EmployeeService.Infractructure.Database
{
    public static class DatabaseSchemeInfo
    {
        public static class EmployeesTable
        {
            public const string TableName = "employees";

            #region Columns

            public const string id = nameof(id);
            public const string email = nameof(email);
            public const string fullname_firstname = nameof(fullname_firstname);
            public const string fullname_lastname = nameof(fullname_lastname);
            public const string fullname_middlename = nameof(fullname_middlename);
            public const string position = nameof(position);
            public const string status_id = nameof(status_id);
            public const string dismissal_date = nameof(dismissal_date);
            public const string hire_date = nameof(hire_date);

            #endregion
        }

        public static class OutboxMessagesTable
        {
            public const string TableName = "outbox_messages";

            #region Columns

            public const string id = nameof(id);
            public const string key = nameof(key);
            public const string eventtype = nameof(eventtype);
            public const string payload = nameof(payload);
            public const string createdat = nameof(createdat);
            public const string retrycount = nameof(retrycount);
            public const string failed = nameof(failed);
            public const string retryafter = nameof(retryafter);
            public const string processedat = nameof(processedat);

            #endregion
        }
    }
}
