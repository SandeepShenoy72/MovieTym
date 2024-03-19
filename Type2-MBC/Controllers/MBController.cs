using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Type2_MBC.Models;

namespace Type2_MBC.Controllers
{
    [ApiController]
    [Route("id/[controller]")]
    public class MBController : Controller
    {
        private readonly Type2MbcContext dbContext;

        public MBController(Type2MbcContext dbContext) 
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllCities()
        {
            List<City> cities = dbContext.Cities.ToList();
            return Ok(cities);
        }

        [HttpGet]
        [Route("GetMoviesInCity")]
        public IActionResult GetAllMoviesInACity([FromQuery] int id)
        {
            List<MovieDatabase> moviesInACity = dbContext.MovieDatabases.Where(x => x.CityId == id).ToList();
            return Ok(moviesInACity);
        }

        [HttpGet]
        [Route("GetTheatersForMovie")]
        public IActionResult GetAllTheatresInACityForMovie([FromQuery] int city_id, [FromQuery] int movie_id)
        {
            List<Theatre> theatresAvailable = dbContext.Theatres.Where(x => x.CityId == city_id && x.MovieId==movie_id).ToList();
            return Ok(theatresAvailable);
        }

        [HttpPost]
        [Route("ConfirmBooking")]

        public IActionResult ConfirmBooking([FromBody] Booking booking) 
        {
            dbContext.Bookings.Add(booking);
            Seat res = dbContext.Seats.FirstOrDefault(x => x.TheatreId == booking.TheatreId && x.SeatId==booking.SeatId);
            res.IsBooked = true;
            dbContext.SaveChanges();
            return Ok(booking);
        }

        [HttpDelete]
        [Route("CancelBooking")]

        public IActionResult CancelBooking(int? booking_id)
        {
            Booking bookDetails = dbContext.Bookings.FirstOrDefault(x => x.BookingId == booking_id);
            if (bookDetails == null) return NotFound("No Bookings Done");
            Seat res = dbContext.Seats.FirstOrDefault(x => x.TheatreId == bookDetails.TheatreId && x.SeatId == bookDetails.SeatId);
            res.IsBooked = false;
            dbContext.Bookings.Remove(bookDetails);
            dbContext.SaveChanges();
            return Ok(bookDetails);
        }

        [HttpGet]
        [Route("ShowAvailableSeats")]

        public IActionResult ShowAvailableSeats(int? theatre_id)
        {
            var res = dbContext.Seats.Where(x => x.TheatreId == theatre_id && x.IsBooked==false).ToList();
            return Ok(res);
        }
    }
}
