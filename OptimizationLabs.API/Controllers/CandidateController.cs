using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptimizationLabs.API.Contexts;
using OptimizationLabs.Shared.Entities;

namespace OptimizationLabs.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidateController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public CandidateController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _dataContext.Candidates.ToListAsync());
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Candidate>> GetItem(Guid id)
        {
            var candidate = await  _dataContext.Candidates.FindAsync(id);

            if (candidate == null)
            {
                return NotFound();
            }

            return Ok(candidate);
        }
        
        [HttpPost]
        public async Task<ActionResult<Candidate>> Create([Required] [FromBody] Candidate candidate)
        {
            try
            {
                _dataContext.Candidates.Add(candidate);
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return StatusCode(400);
            }

            return CreatedAtAction(nameof(GetItem), new { id = candidate.Id }, candidate);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([Required] [FromBody] Candidate candidate)
        {
            _dataContext.Entry(candidate).State = EntityState.Modified;

            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CandidateExists(candidate.Id))
                {
                    return NotFound();
                }

                return StatusCode(500);
            }

            return Ok(candidate);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var candidate = await _dataContext.Candidates.FindAsync(id);
            if (candidate == null)
            {
                return NotFound();
            }

            _dataContext.Candidates.Remove(candidate);
            await _dataContext.SaveChangesAsync();

            return Ok();
        }
        
        private bool CandidateExists(Guid id)
        {
            return _dataContext.Candidates.Any(e => e.Id.Equals(id));
        }
    }
}