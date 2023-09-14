using System;
using System.Collections.Generic;

namespace Imdb_API.Models;

public partial class Comment
{
    public int Id { get; set; }

    public string Body { get; set; } = null!;

    public DateTime? CreatedDate { get; set; }

    public int UserId { get; set; }

    public int MovieId { get; set; }

    public virtual Movie Movie { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
