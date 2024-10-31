using System;
using System.Collections.Generic;

namespace DataGeneratorApp.Models;

public partial class PlayersCharacter
{
    public int PlayerId { get; set; }

    public int CharacterId { get; set; }

    public virtual Character Character { get; set; } = null!;

    public virtual Player Player { get; set; } = null!;
}
