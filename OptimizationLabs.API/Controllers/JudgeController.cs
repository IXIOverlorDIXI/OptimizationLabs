using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptimizationLabs.API.Contexts;
using OptimizationLabs.Shared.Entities;

namespace OptimizationLabs.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JudgeController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public JudgeController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _dataContext.Judges.ToListAsync());
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Judge>> GetItem(Guid id)
        {
            var judge = await  _dataContext.Judges.FindAsync(id);

            if (judge == null)
            {
                return NotFound();
            }

            return Ok(judge);
        }
        
        [HttpPost]
        public async Task<ActionResult<Judge>> Create([Required] [FromBody] Judge judge)
        {
            try
            {
                _dataContext.Judges.Add(judge);
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return StatusCode(400);
            }

            return CreatedAtAction(nameof(GetItem), new { id = judge.Id }, judge);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([Required] [FromBody] Judge judge)
        {
            _dataContext.Entry(judge).State = EntityState.Modified;

            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JudgeExists(judge.Id))
                {
                    return NotFound();
                }

                return StatusCode(500);
            }

            return Ok(judge);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var judge = await _dataContext.Judges.FindAsync(id);
            if (judge == null)
            {
                return NotFound();
            }

            _dataContext.Judges.Remove(judge);
            await _dataContext.SaveChangesAsync();

            return Ok();
        }
        
        private bool JudgeExists(Guid id)
        {
            return _dataContext.Judges.Any(e => e.Id.Equals(id));
        }
    }
}