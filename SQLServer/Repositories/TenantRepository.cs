﻿using Abstractions;
using Abstractions.Models;
using Abstractions.Models.Results;
using Abstractions.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SQLServer.Models;
using SQLServer.Models.Results;
using System.Linq;
using System.Threading.Tasks;

namespace SQLServer.Repositories
{
    public class TenantRepository : ITenantRepository
    {
        //  Variables
        //  =========

        private readonly AppDbContext context;
        private readonly UserManager<ApplicationUserDbo> userManager;

        //  Constructors
        //  ============

        public TenantRepository(AppDbContext context, UserManager<ApplicationUserDbo> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        //  Methods
        //  =======

        public async Task<IRegisterTenantResult> RegisterTenant(string email, string password, string phoneNumber, string name)
        {
            var user = new ApplicationUserDbo
            {
                UserName = email,
                Email = email,
                PhoneNumber = phoneNumber,
                Name = name
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

        public async Task<ApplicationUser?> GetFromUsername(string username)
        {
            ApplicationUserDbo tenant = await context.Users
                .Include(u => u.House)
                .FirstOrDefaultAsync(u => u.UserName == username)
                .ConfigureAwait(false);

            if (!await userManager.IsInRoleAsync(tenant, Roles.Tenant).ConfigureAwait(false))
            {
                return null;
            }

            return tenant;
        }

        public async Task<ApplicationUser?> GetLandlord(string username)
        {
            ApplicationUser tenant = await context.Users
                                        .Include(u => u.House)
                                            .ThenInclude(h => h!.Landlord)
                                        .FirstOrDefaultAsync(u => u.UserName == username)
                                        .ConfigureAwait(false);

            return tenant.House?.Landlord;
        }
    }
}