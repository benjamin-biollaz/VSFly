using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Entities;
using MVCClient.Models;
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
                //free seats and flight date is not passed
                if (f.FreeSeats == 0 || DateTime.Now > f.Date) continue;
                var fm = f.ConvertToFlightM();
                fm.CurrentPrice = (float)(CalculateFlightPrice(f)) /100;
                flightMs.Add(fm);
            }

            return flightMs;
        }

        /*
         * Get booking details related to a destination
         */
        [Route("Flight/{destination}:string/Booking/Passenger")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DestinationBookingDetails>>> GetBookingDetailsByDestination(
            string destination)
        {
            var flightsList = await _context.Flights.Where(f => f.Destination == destination).ToListAsync();

            if (flightsList == null)
            {
                return null;
            }

            List<DestinationBookingDetails> bookingDetails = null;
            foreach (var f in flightsList)
            {
                var bookingList = await _context.Bookings.Include(f => f.Passenger).Where(b =>
                      b.Flight.FlightId == f.FlightId).ToListAsync();

                foreach (var b in bookingList)
                {
                    if (bookingDetails == null)
                        bookingDetails = new List<DestinationBookingDetails>();

                    var passengers = await _context.Persons.Where(p => p.PersonId == b.Passenger.PersonId).ToListAsync();
                    foreach (var p in passengers)
                    {
                        bookingDetails.Add(new DestinationBookingDetails()
                        {
                            FlightNo = b.Flight.FlightId,
                            PaidPrice = (float)(b.PaidPrice)/100,
                            Passenger = p,
                            Destination = destination
                        });
                    }
                }
            }
            return bookingDetails;
        }

        // GET: api/Flights/5
        [Route("Flight/{id}:int/Price")]
        [HttpGet]
        public async Task<ActionResult<float>> GetFlightSalePrice(int id)
        {
            var flight = await _context.Flights.FindAsync(id);

            if (flight == null)
            {
                return NotFound();
            }

            return (float)(CalculateFlightPrice(flight)) / 100;
        }

        private int CalculateFlightPrice(Flight flight)
        {
            int price = flight.BasePrice;
            DateTime today = DateTime.Now;
            float filling = ((float)flight.Capacity / flight.Capacity - flight.FreeSeats);
            if (filling > 0.8)
            {
                price = 150 * price / 100;
            }
            else if (filling < 0.2 && today.AddMonths(2) > flight.Date)
            {
                price = 80 * price / 100;
            }
            else if (filling < 0.5 && today.AddMonths(1) > flight.Date)
            {
                price = 70 * price / 100;
            }

            return price;
        }

        [Route("api/[controller]/{id}/totalSale")]
        [HttpGet]
        public async Task<ActionResult<float>> GetTotalSalePrice(int id)
        {
            var flight = await _context.Flights.Include(f => f.Bookings).FirstAsync((f => f.FlightId == id));

            if (flight == null)
            {
                return NotFound();
            }

            int totalPrice = 0;
            foreach (var b in flight.Bookings)
            {
                totalPrice += b.PaidPrice;
            }

            return (float)(totalPrice) /100;
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<int>> BookFlight(BookingDetailsM bd)
        {
            var flight = await _context.Flights.FindAsync(bd.FlightNo);

            if (flight == null)
            {
                return NotFound();
            }

            var lastName = bd.LastName;
            var firstName = bd.FirstName;
            Passenger p = new Passenger()
            {
                BirthDate = DateTime.Now,
                CustomerSince = DateTime.Now,
                Email = lastName + "." + firstName + "@mail.com",
                FirstName = firstName,
                LastName = lastName,
                Status = "null"
            };
            _context.Passengers.Add(p);
            _context.Bookings.Add(new Booking()
            {
                Flight = flight,
                Passenger = p,
                PaidPrice = CalculateFlightPrice(flight)
            });

            return _context.SaveChanges();
        }

        // GET: api/Flights/5
        [Route("Flight/{id}:int/")]
        [HttpGet]
        public FlightM GetFlight(int id)
        {
            var flight = _context.Flights.FirstOrDefault(f => f.FlightId == id);

            if (flight == null)
            {
                return null;
            }

            var flightM = flight.ConvertToFlightM();
            flightM.CurrentPrice = (float) (CalculateFlightPrice(flight))/100;

            return flightM;
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

        [Route("Flight/{destination}:string/averagePrice")]
        [HttpGet]
        public async Task<ActionResult<float>> GetAveragePriceByDestination(string destination)
        {
            var flightsList = await _context.Flights.Where(f => f.Destination == destination).ToListAsync();
            int sumPrice = 0;
            int nbOfReservation = 0;

            if (flightsList == null)
                return 0;

            foreach (var f in flightsList)
            {
                var bookingList = await _context.Bookings.Where(b =>
                    b.Flight.FlightId == f.FlightId).ToListAsync();

                if (bookingList == null) continue;
                nbOfReservation += bookingList.Count;
                foreach (var b in bookingList)
                {
                    sumPrice += b.PaidPrice;
                }
            }

            if (nbOfReservation == 0)
                return 0f;

            float floatAveragePrice = (float)(sumPrice) / nbOfReservation / 100;

            return floatAveragePrice;
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
