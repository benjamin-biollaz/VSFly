using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using Microsoft.EntityFrameworkCore;
using Web_API.Models;

namespace Web_API.Extensions
{
    public static class ConverterExtension
    {
        public static Flight ConvertToFlight(this FlightM fm)
        {
            Flight f = new Flight()
            {
                Date = fm.Date,
                Departure = fm.Departure,
                Destination = fm.Destination,
                FlightId = fm.FlightNo,
                FreeSeats = fm.FreeSeats,
            };
            return f;
        }

        public static FlightM ConvertToFlightM(this Flight f)
        {
            FlightM fm = new FlightM()
            {
                Date = f.Date,
                Departure = f.Departure,
                Destination = f.Destination,
                FlightNo = f.FlightId,
                FreeSeats = f.FreeSeats,
                BasePrice = f.BasePrice / 100
            };
            return fm;
        }
    }
}
