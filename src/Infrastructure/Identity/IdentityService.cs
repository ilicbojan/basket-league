using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ICurrentUserService _currentUserService;
        private readonly SignInManager<AppUser> _signInManager;

        public IdentityService(UserManager<AppUser> userManager,
                               RoleManager<IdentityRole> roleManager,
                               SignInManager<AppUser> signInManager,
                               ICurrentUserService currentUserService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _currentUserService = currentUserService;
            _signInManager = signInManager;
        }
        public async Task<AppUser> GetUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                throw new NotFoundException(nameof(AppUser), id);
            }

            return user;
        }

        public async Task<AppUser> GetUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new NotFoundException(nameof(AppUser), email);
            }

            return user;
        }

        public async Task<AppUser> GetCurrentUserAsync()
        {
            var user = await GetUserByIdAsync(_currentUserService.UserId);

            return user;
        }

        public async Task<AppUser> LoginUserAsync(string email, string password)
        {
            var user = await GetUserByEmailAsync(email);

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

            if (result.Succeeded)
            {
                return user;
            }

            throw new Exception("Pogresan password");
        }

        public async Task<string> GetUsernameAsync(string userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

            return user.UserName;
        }

        public async Task<string> CreateUserAsync(AppUser user, string password, string role)
        {
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, role);

                return user.Id;
            }

            throw new Exception("Problem creating user");
        }

        public async Task<bool> IsUserInRoleAsync(AppUser user, string roleName)
        {
            return await _userManager.IsInRoleAsync(user, roleName);
        }

        public async Task<bool> IsUserInRoleAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new NotFoundException(nameof(AppUser), userId);
            }

            return await _userManager.IsInRoleAsync(user, roleName);
        }
    }
}
