using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryAPI.Data;
using LibraryAPI.Models;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookcasesController : ControllerBase
    {
        private readonly LibraryContext _context;

        public BookcasesController(LibraryContext context)
        {
            _context = context;
        }

        // GET: api/Bookcase
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bookcase>>> GetBookcase()
        {
            return await _context.Bookcase.ToListAsync();
        }

        // GET: api/Bookcase/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bookcase>> GetBookcase(int id)
        {
            var bookcase = await _context.Bookcase.FindAsync(id);

            if (bookcase == null)
            {
                return NotFound();
            }

            return bookcase;
        }

        // PUT: api/Bookcase/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookcase(int id, Bookcase bookcase)
        {
            if (id != bookcase.Id)
            {
                return BadRequest();
            }

            _context.Entry(bookcase).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookcaseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Bookcase
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Bookcase>> PostBookcase(Bookcase bookcase)
        {
            _context.Bookcase.Add(bookcase);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBookcase", new { id = bookcase.Id }, bookcase);
        }

        // DELETE: api/Bookcase/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookcase(int id)
        {
            var bookcase = await _context.Bookcase.FindAsync(id);
            if (bookcase == null)
            {
                return NotFound();
            }

            _context.Bookcase.Remove(bookcase);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookcaseExists(int id)
        {
            return _context.Bookcase.Any(e => e.Id == id);
        }
    }
}
