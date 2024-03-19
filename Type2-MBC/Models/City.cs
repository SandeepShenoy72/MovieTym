using System;
using System.Collections.Generic;

namespace Type2_MBC.Models;

public partial class City
{
    public int CityId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<MovieDatabase> MovieDatabases { get; set; } = new List<MovieDatabase>();

    public virtual ICollection<Theatre> Theatres { get; set; } = new List<Theatre>();
}
