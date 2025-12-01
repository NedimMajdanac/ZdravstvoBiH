using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Zdravstvo.Infrastructure.Helpers
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetDoktorId(this ClaimsPrincipal user)
        {
            var claim = user.FindFirst("doktorId");
            if (claim == null || !int.TryParse(claim.Value, out var id))
                throw new InvalidOperationException("DoktorID claim missing or invalid");
            return id;
        }
    }
}
