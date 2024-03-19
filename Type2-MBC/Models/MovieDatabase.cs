using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Type2_MBC.Models;

public partial class MovieDatabase
{
    public int MovieId { get; set; }

    public string? Title { get; set; }

    public int? CityId { get; set; }
    [JsonIgnore]
    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    [JsonIgnore]
    public virtual City? City { get; set; }

    [JsonIgnore]
    public virtual ICollection<Theatre> Theatres { get; set; } = new List<Theatre>();
}
