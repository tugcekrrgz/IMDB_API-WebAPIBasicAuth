using System;
using System.Collections.Generic;

namespace Imdb_API.Models;

public partial class Cast
{
    public int Id { get; set; }

    public string? FullName { get; set; }

    public int? MovieId { get; set; }

    public virtual Movie? Movie { get; set; }
}
