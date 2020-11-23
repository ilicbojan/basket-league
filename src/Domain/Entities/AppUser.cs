using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual Player Player { get; set; }
        public virtual ICollection<Match> RefereeMatches { get; set; }
        public virtual ICollection<Match> DelegateMatches { get; set; }
    }
}
