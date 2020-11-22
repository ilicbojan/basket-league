using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUsernameAsync(string userId);
        Task<string> CreateUserAsync(AppUser user, string password, string role);
    }
}
