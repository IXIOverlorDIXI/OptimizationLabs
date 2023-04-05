using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptimizationLabs.API.Contexts;
using OptimizationLabs.Shared.Entities;

namespace OptimizationLabs.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeliveryController : ControllerBase
    {
        private readonly DataContext _dataContext;
        
        public DeliveryController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _dataContext.Deliveries.ToListAsync());
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Delivery>> GetDelivery(Guid id)
        {
            var delivery = await  _dataContext.Deliveries.FindAsync(id);

            if (delivery == null)
            {
                return NotFound();
            }

            return Ok(delivery);
        }
        
        [HttpPost]
        public async Task<ActionResult<Delivery>> Create([Required] [FromBody] Delivery delivery)
        {
            try
            {
                _dataContext.Deliveries.Add(delivery);
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return StatusCode(400);
            }
            
            return CreatedAtAction(nameof(GetDelivery), new { id = delivery.Id }, delivery);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([Required] [FromBody] Delivery delivery)
        {
            _dataContext.Entry(delivery).State = EntityState.Modified;

            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeliveryExists(delivery.Id))
                {
                    return NotFound();
                }

                return StatusCode(500);
            }

            return Ok(delivery);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var delivery = await _dataContext.Deliveries.FindAsync(id);
            if (delivery == null)
            {
                return NotFound();
            }

            _dataContext.Deliveries.Remove(delivery);
            await _dataContext.SaveChangesAsync();

            return Ok();
        }
        
        private bool DeliveryExists(Guid id)
        {
            return _dataContext.Deliveries.Any(e => e.Id.Equals(id));
        }
    }
}