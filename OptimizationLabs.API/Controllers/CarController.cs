using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptimizationLabs.API.Contexts;
using OptimizationLabs.Shared.Entities;

namespace OptimizationLabs.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly DataContext _dataContext;
        
        public CarController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _dataContext.Cars.ToListAsync());
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCar(Guid id)
        {
            var car = await  _dataContext.Cars.FindAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            return Ok(car);
        }
        
        [HttpPost]
        public async Task<ActionResult<Car>> Create([Required] [FromBody] Car car)
        {
            try
            {
                _dataContext.Cars.Add(car);
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return StatusCode(400);
            }
            
            return CreatedAtAction(nameof(GetCar), new { id = car.Id }, car);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([Required] [FromBody] Car car)
        {
            _dataContext.Entry(car).State = EntityState.Modified;

            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(car.Id))
                {
                    return NotFound();
                }

                return StatusCode(500);
            }

            return Ok(car);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var car = await _dataContext.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            _dataContext.Cars.Remove(car);
            await _dataContext.SaveChangesAsync();

            return Ok();
        }
        
        private bool CarExists(Guid id)
        {
            return _dataContext.Cars.Any(e => e.Id.Equals(id));
        }
    }
}