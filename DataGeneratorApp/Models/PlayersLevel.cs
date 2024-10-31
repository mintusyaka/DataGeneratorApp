using System;
using System.Collections.Generic;

namespace DataGeneratorApp.Models;

public partial class PlayersLevel
{
    public int PlayerId { get; set; }

    public int LevelId { get; set; }

    public virtual Level Level { get; set; } = null!;

    public virtual Player Player { get; set; } = null!;
}
