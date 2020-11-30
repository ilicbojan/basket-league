using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Queries.LoginUser
{
    public class LoginUserQueryValidator : AbstractValidator<LoginUserQuery>
    {
        private readonly IAppDbContext _context;

        public LoginUserQueryValidator(IAppDbContext context)
        {
            _context = context;

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email je obavezan")
                .EmailAddress().WithMessage("Email nije u ispravnom formatu")
                .MustAsync(EmailExists).WithMessage("Izabrani email ne postoji, registrujte se");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password je obavezan");
        }

        public async Task<bool> EmailExists(string email, CancellationToken cancellationToken)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }
    }
}
