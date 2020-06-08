using API.SmartApartment.ViewModel;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.SmartApartment.Helper
{
    /// <summary>
    /// This is ScopeHandler class
    /// </summary>
    /// <remarks>This class verifies scopes, roles and permissions to access rest api</remarks>
    public class HasScopeHandler : AuthorizationHandler<VMScopeRequirement>
    {
        /// <summary>
        /// This fucntion perform the Authorization of User
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        /// <remarks>This function checks if the permissions claim issued by your Auth0 tenant is present. If the permission claim exists then it will successed the requirement</remarks>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, VMScopeRequirement requirement)
        {

            // If user does not have the scope claim, get out of here
            if (!context.User.HasClaim(c => c.Type == "permissions" && c.Issuer == requirement.Issuer))
                return Task.CompletedTask;

            if (context.User.HasClaim(c => c.Type == "permissions" && c.Issuer == requirement.Issuer && c.Value == requirement.Scope))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}
