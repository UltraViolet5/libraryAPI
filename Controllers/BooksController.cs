using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryAPI.Data;
using LibraryAPI.Models;
using Serilog;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly LibraryContext _context;

        public BooksController(LibraryContext context)
        {
            _context = context;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBook()
        {
            var result = await _context
                .Book
                // .Include(b => b.Owner)
                .ToListAsync();

            foreach (var book in result)
            {
                book.Owner = _context.User
                    .FirstOrDefault(u => u.Id == book.OwnerId);
            }

            return result;
        }

        // GET: api/Book/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.Book
                .FindAsync(id);

            if (book == null)
                return NotFound();

            book.Owner = _context.User
                .FirstOrDefault(u => u.Id == book.OwnerId);

            return book;
        }

        [HttpGet("ByUserId/{userId}")]
        public async Task<ActionResult<List<Book>>> GetBook(string userId)
        {
            var books = await _context.Book
                .Where(x => x.OwnerId == userId)
                .OrderByDescending(b => b.AddingDate)
                .ToListAsync();

            if (books == null)
                return NotFound();

            var owner = _context.User
                .FirstOrDefault(u => u.Id == userId);

            foreach (var book in books)
                book.Owner = owner;

            return books;
        }

        [HttpGet("ByUserId/{userId}/{limit}")]
        public async Task<ActionResult<List<Book>>> GetBook(string userId, int limit)
        {
            var books = await _context.Book
                .Where(x => x.OwnerId == userId)
                .OrderByDescending(b => b.AddingDate)
                .Take(limit)
                .ToListAsync();
            
            if (books == null)
                return NotFound();

            var owner = _context.User
                .FirstOrDefault(u => u.Id == userId);

            foreach (var book in books)
                book.Owner = owner;

            return books;
        }

        [HttpGet("ByUserId/{userId}/Count")]
        public async Task<int> GetBooksCount(string userId)
        {
            var count = _context.Book
                .Count(x => x.OwnerId == userId);

            return count;
        }

        // PUT: api/Book/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
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

        // POST: api/Book
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            try
            {
                _context.Book.Add(book);

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Log.Error("Error: " + e.Message);
            }
            return CreatedAtAction("GetBook", new { id = book.Id }, book);
        }
        
        // DELETE: api/Book/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Book.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.Id == id);
        }
    }
}
