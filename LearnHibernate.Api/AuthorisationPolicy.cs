namespace LearnHibernate.Api
{
    using Microsoft.AspNetCore.Authorization;

    public class AuthorisationPolicy
    {
        internal static void GetPolicies(AuthorizationOptions options)
        {
            // refer https://andrewlock.net/custom-authorisation-policies-and-requirements-in-asp-net-core/ to get fancy here
            // disadvantage: We need to add a new policy and new constants for policy and ges role every time we think we need new GES role based auth
            // a diff perspective : Do we really want run time flexibility around security aspects?
            options.AddPolicy(ApiConstants.AuthPolicy.CanCreateRiskRequest, policyBuilder => policyBuilder.RequireClaim(ApiConstants.GESRoleClaim.RiskCreatorRole));
            options.AddPolicy(ApiConstants.AuthPolicy.CanApproveRiskRequest, policyBuilder => policyBuilder.RequireClaim(ApiConstants.GESRoleClaim.RiskApproverRole));
        }
    }
}
