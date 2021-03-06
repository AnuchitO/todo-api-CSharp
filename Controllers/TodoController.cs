using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
       private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;
            if (_context.TodoItems.Count() == 0)
            {
                _context.TodoItems.Add(new TodoItem { Name = "AnuchitO" });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
          return await _context.TodoItems.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
          var todoItem = await _context.TodoItems.FindAsync(id);

          if (todoItem == null)
          {
            return NotFound();
          }

          return todoItem;
        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
          _context.TodoItems.Add(todoItem);
          await _context.SaveChangesAsync();

          return CreatedAtAction("GetTodoItem", new { id = todoItem.Id}, todoItem);
        }

        // Handle exception please. Attempted to update or delete an entity that does not exist in the store.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItem todoItem)
        {
            if(id != todoItem.Id)
            {
              return BadRequest();
            }

            _context.Entry(todoItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoItem>> DeleteTodoItem(long id)
        {
          var todoItem = await _context.TodoItems.FindAsync(id);
          if (todoItem == null)
          {
            return NotFound();
          }

          _context.TodoItems.Remove(todoItem);
          await _context.SaveChangesAsync();

          return todoItem;
        }
    }
}