namespace BurgerBackend.Identity.Interface
{
    public static class Endpoints
    {
        public static class Account
        {
            public const string Authenticate = "api/account/authenticate";
            public const string ValidateToken = "api/account/validatetoken";
            public const string Register = "api/account/register";
            public const string GetById = "api/account/{id}";
            public const string Update = "api/account/{id}";
            public const string Delete = "api/account/{id}";
        }
    }
}
