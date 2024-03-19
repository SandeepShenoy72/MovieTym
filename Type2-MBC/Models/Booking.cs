using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Type2_MBC.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public int? UserId { get; set; }

    public int? TheatreId { get; set; }

    public int? MovieId { get; set; }

    public int? SeatId { get; set; }

    public DateTime? BookingDate { get; set; }

    [JsonIgnore]
    public virtual MovieDatabase? Movie { get; set; }
    [JsonIgnore]
    public virtual Seat? Seat { get; set; }
    [JsonIgnore]
    public virtual Theatre? Theatre { get; set; }
    [JsonIgnore]
    public virtual User? User { get; set; }
}
