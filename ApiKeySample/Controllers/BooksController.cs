using ApiKeySample.Filters;
using ApiKeySample.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiKeySample.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase
{
    private static readonly Dictionary<string, Book> Books = new()
    {
        {
            "9780261103566", new Book()
            {
                ISBN = "9780261103566",
                Author = "J R R Tolkien",
                Summary =
                    "Immerse yourself in Middle-earth with Tolkien's classic masterpiece, telling the complete story of Bilbo Baggins and the Hobbits' epic encounters with Gandalf, Gollum, dragons and monsters, in the quest to destroy the One Ring.",
                Title = "The Lord of the Rings And, The Hobbit"
            }
        },
        {
            "9780340960196", new Book()
            {
                ISBN = "9780340960196",
                Author = "Frank Herbert",
                Summary =
                    "Before The Matrix, before Star Wars, before Ender's Game and Neuromancer, there was Dune: winner of the prestigious Hugo and Nebula awards, and widely considered one of the greatest science fiction novels ever written.",
                Title = "Dune - The Dune Novels"
            }
        }
    };

    [Route("")]
    [PermissionReadAuthorize]
    [HttpGet]
    public IEnumerable<Book> Get()
    {
        
        return Books.Values;
    }

    [HttpGet]
    [Route("{isbn}")]
    [PermissionReadAuthorize]
    public Book GetByIsbn(string isbn)
    {
        return Books[isbn];
    }

    [Route("")]
    [PermissionWriteAuthorize]
    [HttpPost]
    public ActionResult Post([FromBody] Book body)
    {
        Books[body.ISBN] = body;
        return Created(nameof(GetByIsbn), body.ISBN);
    }
}