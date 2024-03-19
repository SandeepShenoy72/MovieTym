using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Type2_MBC.Models;

public partial class Seat
{
    public int SeatId { get; set; }
    
    public int? TheatreId { get; set; }

    public bool? IsBooked { get; set; }
    [JsonIgnore]
    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    [JsonIgnore]
    public virtual Theatre? Theatre { get; set; }
}
