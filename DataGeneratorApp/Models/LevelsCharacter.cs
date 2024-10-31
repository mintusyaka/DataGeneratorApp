using System;
using System.Collections.Generic;

namespace DataGeneratorApp.Models;

public partial class LevelsCharacter
{
    public int LevelId { get; set; }

    public int CharacterId { get; set; }

    public virtual Character Character { get; set; } = null!;

    public virtual Level Level { get; set; } = null!;
}
