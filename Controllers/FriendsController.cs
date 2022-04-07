using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryAPI.Data;
using LibraryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendsController : ControllerBase
    {
        private readonly LibraryContext _context;

        public FriendsController(LibraryContext context)
        {
            _context = context;
        }

        // GET: api/Friends
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Friends>>> GetFriends()
        {
            return await _context.Friends.ToListAsync();
        }

        // GET: api/Friends/<guid-userId>
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<Friends>>> GetFriends(string userId)
        {
            var friends = await _context.Friends
                                            .Where(f => f.UserId == userId ||
                                                        f.FriendId == userId)
                                            .ToListAsync();

            if (friends == null || friends.Count == 0)
                return NotFound();

            return friends;
        }

        // PUT: Not implemented, because don't plan to edit records

        // POST: api/Friends
        [HttpPost]
        public async Task<ActionResult<Friends>> PostFriends(Friends friends)
        {
            _context.Friends.Add(friends);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFriends", new {userId = friends.UserId}, friends);
        }

        // DELETE: api/Friends
        [HttpDelete]
        public async Task<IActionResult> DeleteFriends(int id)
        {
            var friend = await _context.Friends.FindAsync(id);

            if (friend == null)
                return NotFound();

            _context.Friends.Remove(friend);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
