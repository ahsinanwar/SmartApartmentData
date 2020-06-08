using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.SmartApartment.ViewModel
{
    /// <summary>
    /// This is a viewModel for Scope Requirement
    /// </summary>
    /// <remarks>This class contains the preoperties to verify JWT claims for every API request</remarks>
    public class VMScopeRequirement : IAuthorizationRequirement
    {
        public string Issuer { get; }
        public string Scope { get; }
        /// <summary>
        /// This is constructor of the viewmodel VMScopeRequirement
        /// </summary>
        /// <param name="scope">scope contains permission</param>
        /// <param name="issuer">issuer contains audience from Auth0 JWT token</param>
        public VMScopeRequirement(string scope, string issuer)
        {
            Scope = scope ?? throw new ArgumentNullException(nameof(scope));
            Issuer = issuer ?? throw new ArgumentNullException(nameof(issuer));
        }
    }
}
