namespace LearnHibernate.Api
{
    public static class ApiConstants
    {
        public static class Auth
        {
            public const string StandardId = "StandardId";
            public const string PersonNumber = "PersonNumber";
            public const string ClaimsStore = "ClaimsStore";
        }

        public static class AuthPolicy
        {
            public const string CanCreateRiskRequest = "CanCreateRiskRequest";
            public const string CanApproveRiskRequest = "CanApproveRiskRequest";
        }

        public static class GESRoleClaim
        {
            public const string RiskCreatorRole = "RiskCreatorRole";
            public const string RiskApproverRole = "RiskApproverRole";
        }
    }
}
