namespace Worker.Api.Configuration.AuthZero
{
    static class AuthZeroPermissions
    {
        public const string ReadOwnWorkers = "readOwn:workers";
        public const string ReadAllWorkers = "readAll:workers";
        public const string CreateWorkers = "create:workers";
        public const string SearchWorkers = "search:workers";
        public const string ModifyWorkers = "modify:worker";
    }
}