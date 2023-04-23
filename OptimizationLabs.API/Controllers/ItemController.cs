using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptimizationLabs.API.Contexts;
using OptimizationLabs.Shared.Entities;

namespace OptimizationLabs.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly DataContext _dataContext;
        
        public ItemController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _dataContext.Items.ToListAsync());
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(Guid id)
        {
            var item = await  _dataContext.Items.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }
        
        [HttpPost]
        public async Task<ActionResult<Item>> Create([Required] [FromBody] Item item)
        {
            try
            {
                _dataContext.Items.Add(item);
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return StatusCode(400);
            }

            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([Required] [FromBody] Item item)
        {
            _dataContext.Entry(item).State = EntityState.Modified;

            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(item.Id))
                {
                    return NotFound();
                }

                return StatusCode(500);
            }

            return Ok(item);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var item = await _dataContext.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _dataContext.Items.Remove(item);
            await _dataContext.SaveChangesAsync();

            return Ok();
        }
        
        private bool ItemExists(Guid id)
        {
            return _dataContext.Items.Any(e => e.Id.Equals(id));
        }
    }
}