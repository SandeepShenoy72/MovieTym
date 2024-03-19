using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Type2_MBC.Models;

public partial class Theatre
{
    public int TheatreId { get; set; }

    public string? Name { get; set; }

    public int? CityId { get; set; }

    public int? MovieId { get; set; }
    [JsonIgnore]
    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    [JsonIgnore]
    public virtual City? City { get; set; }
    [JsonIgnore]
    public virtual MovieDatabase? Movie { get; set; }
    [JsonIgnore]
    public virtual ICollection<Seat> Seats { get; set; } = new List<Seat>();
}
