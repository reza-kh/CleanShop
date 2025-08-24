namespace Application.Common.Constants
{
    public static class ClaimExposer
    {
        public static Dictionary<string, List<string>> Expose() => new Dictionary<string, List<string>>
            {
                {"Admin",new List<string>{"ViewAdmin"}},
                {"User",new List<string>{"ListUser","CreateUser","EditUser","DeleteUser"}},
                {"Role",new List<string>{ "ListRole", "CreateRole", "EditRole", "DeleteRole"}}
            };
    }
}
