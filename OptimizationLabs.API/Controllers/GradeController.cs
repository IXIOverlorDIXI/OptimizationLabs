using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptimizationLabs.API.Contexts;
using OptimizationLabs.Shared.Entities;

namespace OptimizationLabs.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GradeController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public GradeController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _dataContext.Grades
                .Include(x => x.Candidate)
                .Include(x => x.Judge)
                .ToListAsync());
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Grade>> GetItem(Guid id)
        {
            var grade = await  _dataContext.Grades
                .Include(x => x.Candidate)
                .Include(x => x.Judge)
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (grade == null)
            {
                return NotFound();
            }

            return Ok(grade);
        }
        
        [HttpGet("randomData")]
        public async Task<ActionResult<Grade>> RandomGeneration()
        {
            try
            {
                var grades = await _dataContext.Grades.ToListAsync();
                
                _dataContext.Grades.RemoveRange(grades);

                var candidates = await _dataContext.Candidates.ToListAsync();
                var judges = await _dataContext.Judges.ToListAsync();

                var random = new Random();
                
                foreach (var judge in judges)
                {
                    var candidatesList = new System.Collections.Generic.List<Candidate>(candidates);

                    while (candidatesList.Any())
                    {
                        var index = random.Next(0, candidatesList.Count);
                        
                        grades.Add(new Grade
                        {
                            CandidateId = candidatesList[index].Id,
                            JudgeId = judge.Id,
                            Id = Guid.NewGuid(),
                            GradeValue = candidates
                                .FindLastIndex(x => x.Id
                                    .Equals(candidatesList[index].Id))
                        });
                        
                        candidatesList.RemoveAt(index);
                    }
                }

                await _dataContext.Grades.AddRangeAsync(grades);
                
                await _dataContext.SaveChangesAsync();

                return Ok(await _dataContext.Grades
                    .Include(x => x.Candidate)
                    .Include(x => x.Judge)
                    .ToListAsync());
            }
            catch (Exception e)
            {
                return StatusCode(400);
            }
        }
        
        [HttpPost]
        public async Task<ActionResult<Grade>> Create([Required] [FromBody] Grade grade)
        {
            try
            {
                _dataContext.Grades.Add(grade);
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return StatusCode(400);
            }

            return CreatedAtAction(nameof(GetItem), new { id = grade.Id }, grade);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([Required] [FromBody] Grade grade)
        {
            _dataContext.Entry(grade).State = EntityState.Modified;

            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GradeExists(grade.Id))
                {
                    return NotFound();
                }

                return StatusCode(500);
            }

            return Ok(grade);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var grade = await _dataContext.Grades.FindAsync(id);
            if (grade == null)
            {
                return NotFound();
            }

            _dataContext.Grades.Remove(grade);
            await _dataContext.SaveChangesAsync();

            return Ok();
        }
        
        private bool GradeExists(Guid id)
        {
            return _dataContext.Grades.Any(e => e.Id.Equals(id));
        }
    }
}