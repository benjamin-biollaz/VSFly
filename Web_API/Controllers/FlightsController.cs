using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Entities;
using Web_API.Models;
using Web_API.Extensions;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly VSFlyContext _context;

        public FlightsController(VSFlyContext context)
        {
            _context = context;
        }
        /***
         * Get only available flights
         */
        // GET: api/Flights
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FlightM>>> GetFlights()
        {
            var flightsList = await _context.Flights.ToListAsync();
            List<FlightM> flightMs = new List<FlightM>();
            foreach (var f in flightsList)
            {
                if (f.FreeSeats == 0) continue;
                var fm = f.ConvertToFlightM();
                flightMs.Add(fm);
            }

            return flightMs;
        }

        // GET: api/Flights/5
        [HttpGet("{id}")]
        public async Task<ActionResult<int>> GetFlightSalePrice(int id)
        {
            var flight = await _context.Flights.FindAsync(id);
            
            if (flight == null)
            {
                return NotFound();
            }

            int price = flight.BasePrice;
            DateTime today  = DateTime.Now;
            float filling = ((float)flight.Capacity / flight.Capacity-flight.FreeSeats);
            if (filling > 0.8)
            {
                price = 150 * price / 100;
            } else if (filling < 0.2 && today.AddMonths(2) > flight.Date)
            {
                price = 80 * price / 100;
            } else if (filling < 0.5 && today.AddMonths(1) > flight.Date)
            {
                price = 70 * price / 100;
            }

            return price;
        }

        // GET: api/Flights/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Flight>> GetFlight(int id)
        {
            var flight = await _context.Flights.FindAsync(id);

            if (flight == null)
            {
                return NotFound();
            }

            return flight;
        }

        // PUT: api/Flights/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFlight(int id, Flight flight)
        {
            if (id != flight.FlightId)
            {
                return BadRequest();
            }

            _context.Entry(flight).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FlightExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Flights
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Flight>> PostFlight(FlightM fm)
        {
            _context.Flights.Add(fm.ConvertToFlight());
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFlight", new { id = fm.FlightNo }, fm);
        }

        // DELETE: api/Flights/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlight(int id)
        {
            var flight = await _context.Flights.FindAsync(id);
            if (flight == null)
            {
                return NotFound();
            }

            _context.Flights.Remove(flight);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FlightExists(int id)
        {
            return _context.Flights.Any(e => e.FlightId == id);
        }
    }
}
