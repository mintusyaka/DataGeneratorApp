using System;
using System.Collections.Generic;

namespace DataGeneratorApp.Models;

public partial class Level
{
    public int LevelId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public TimeOnly CreationTime { get; set; }

    public DateOnly CreationDate { get; set; }

    public int UnlockScore { get; set; }
}
