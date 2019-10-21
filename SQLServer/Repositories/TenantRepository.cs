﻿using Abstractions;
using Abstractions.Models;
using Abstractions.Repositories;
using Microsoft.AspNetCore.Identity;
using SQLServer.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SQLServer.Repositories
{
    public class TenantRepository : ITenantRepository
    {
        //  Variables
        //  =========

        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        //  Constructors
        //  ============

        public TenantRepository(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        //  Methods
        //  =======

        public async Task<IRegisterTenantResult> RegisterTenant(string username, string email, string password)
        {
            var user = new IdentityUser
            {
                UserName = username,
                Email = email
            };

            IdentityResult identityResult = await userManager.CreateAsync(user, password).ConfigureAwait(false);

            if (!identityResult.Succeeded)
            {
                return new RegisterTenantResult
                {
                    Succeeded = identityResult.Succeeded,
                    Errors = identityResult.Errors.Select(e => e.Description)
                };
            }

#warning If the above is successful but this fails then there is a user created with no role
            IdentityResult addRoleIdentityResult = await userManager.AddToRoleAsync(user, Roles.Tenant).ConfigureAwait(false);

            return new RegisterTenantResult
            {
                Succeeded = addRoleIdentityResult.Succeeded,
                Errors = addRoleIdentityResult.Errors.Select(e => e.Description)
            };
        }
    }
}