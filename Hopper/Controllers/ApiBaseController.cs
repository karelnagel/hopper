using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Hopper.Data;
using Hopper.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hopper.Controllers
{
    public abstract class ApiBaseController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;

        protected ApiBaseController(ApplicationDbContext context, IAuthorizationService authorizationService)
        {
            Context = context;
            _authorizationService = authorizationService;
        }

        protected ApplicationDbContext Context { get; }

        internal virtual Guid GetUserId()
        {
            return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        internal virtual Task<ApplicationUser> GetUser()
        {
            return Context.Users.Include(u=>u.CreatedSounds).Include(u=>u.Favorites).SingleOrDefaultAsync(u => u.Id == GetUserId());
        }
    }
}