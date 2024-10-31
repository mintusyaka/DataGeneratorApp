using System;
using System.Collections.Generic;

namespace DataGeneratorApp.Models;

public partial class Character
{
    public int CharacterId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Strength { get; set; }

    public int Speed { get; set; }
}
