using System.Threading.Tasks;
using Hopper.Data;
using Hopper.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hopper.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ApiBaseController
    {
        public UsersController(ApplicationDbContext context, IAuthorizationService authorizationService) : base(context,
            authorizationService)
        {
        }

        // GET: api/Users/Me
        [HttpGet("me")]
        public async Task<ActionResult<UserDto>> GetUserMe()
        {
            var currentUserId = GetUserId();

            var applicationUser = await Context.Users.SingleOrDefaultAsync(u => u.Id == currentUserId);

            if (applicationUser == null) return NotFound();

            return new UserDto(applicationUser);
        }
    }
}