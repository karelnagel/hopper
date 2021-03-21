using System.Threading.Tasks;
using Hopper.Data;
using Hopper.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

            var user = await Context.Users.SingleOrDefaultAsync(u => u.Id == currentUserId);

            if (user == null)
                return NotFound();

            return new UserDto(user);
        }

        [HttpPut("me")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutSound(UserDto userDto)
        {
            var user = await GetUser();
            if (user == null)
                return NotFound();
            if (user.Id != userDto?.Id)
                return BadRequest();
            user.Update(userDto);
            await Context.SaveChangesAsync();

            return NoContent();
        }
    }
}