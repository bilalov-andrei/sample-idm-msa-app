namespace IDM.AccessManagement.Application.Queries.GetUserAccounts
{
    public class GetUserAccountsQueryResponse
    {
        /// <summary>
        ///  Founded user accounts
        /// </summary>
        public IReadOnlyCollection<UserAccountDto> Items { get; set; }
    }
}
