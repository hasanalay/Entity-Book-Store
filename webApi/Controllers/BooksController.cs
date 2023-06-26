using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using webApi.Models;
using webApi.Repositories;

namespace webApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly RepositoryContext _context;
        public BooksController(RepositoryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            try
            {
                var books = _context.Books.ToList();
                return Ok(books);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }
        
        [HttpGet("{id:int}")]
        public IActionResult GetABook([FromRoute(Name = "id")] int id)
        {
            try
            {
                var book = _context.Books.SingleOrDefault(b => b.Id == id);   
                if (book == null)
                {
                    return NotFound();
                }
                return Ok(book);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
          
        }
        
        [HttpPost]
        public IActionResult AddABook([FromBody] Book book)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _context.Books.Add(book);
                _context.SaveChanges();
                return StatusCode(201, book);
            }
            catch (Exception e)
            { 
                return BadRequest(e.Message);
            } 
        }
        
        [HttpPut("{id:int}")]
        public IActionResult UpdateABook([FromRoute(Name = "id")] int id,[FromBody] Book book)
        {
            try
            {
                //check the book exists
                var bookToUpdate = _context.Books.SingleOrDefault(b => b.Id == id);
                if (bookToUpdate == null)
                {
                    return NotFound();
                }
            
                //check the id in the body is the same as the id in the route
                if (book.Id != id)
                {
                    return BadRequest("The id in the body is not the same as the id in the route");
                }
            
                //check the model is valid
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
            
                //update the book
                bookToUpdate.Title = book.Title; 
                book.Id = bookToUpdate.Id;
                _context.SaveChanges();  
                return Ok(book);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                
            }
        }
         
        [HttpDelete("{id:int}")]
        public IActionResult DeleteABook([FromRoute(Name = "id")] int id)
        {
            try
            {
                //check the book exists
                var bookToDelete = _context.Books.SingleOrDefault(b => b.Id == id);
                if (bookToDelete == null)
                {
                    return NotFound();
                }
                _context.Books.Remove(bookToDelete);
                _context.SaveChanges();
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpPatch("{id:int}")]
        public IActionResult UpdateABookPartially([FromRoute(Name = "id")] int id,
            [FromBody] JsonPatchDocument<Book> bookPatch)
        {
            try
            {
                //check the book exists
                var bookToUpdate = _context.Books.SingleOrDefault(b => b.Id == id);
                if (bookToUpdate == null)
                {
                    return NotFound();
                }
                
                bookPatch.ApplyTo(bookToUpdate);
                _context.SaveChanges();
                return NoContent();
            } 
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
    }
}
