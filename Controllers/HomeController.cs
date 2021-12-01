using Microsoft.AspNetCore.Mvc;
using Todo.Data;
using Todo.Models;

namespace Todo.Controllers
{
    [ApiController]
    [Route("/home")]
    public class HomeController: ControllerBase
    {
        private readonly AppDbContext _context;
        public HomeController([FromServices] AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get() => Ok(_context.Todos);

        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var todo = _context.Todos.FirstOrDefault(x => x.Id == id);

            if(todo == null)
                return NotFound();

            return Ok(todo);
        }

        [HttpPost]
        public IActionResult Post([FromBody] TodoModel model)
        {
            _context.Add(model);
            _context.SaveChanges();

            return Created($"/{model.Id}", model);
        }

        [HttpPut]
        public IActionResult Put([FromBody] TodoModel model)
        {
            var todo = _context.Todos.FirstOrDefault(x => x.Id == model.Id);

            if(todo == null)
                return NotFound();

            todo.Title = model.Title;
            todo.Done = model.Done;

            _context.Update(todo);
            _context.SaveChanges();

            return Ok(model);
        }

         [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var todo = _context.Todos.FirstOrDefault(x => x.Id == id);

            if(todo == null)
                return NotFound();

            _context.Remove(todo);
            _context.SaveChanges();

            return Ok(todo);
        }

    }
}