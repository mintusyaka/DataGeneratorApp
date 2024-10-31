using System;
using System.Collections.Generic;

namespace DataGeneratorApp.Models;

public partial class Player
{
    public int PlayerId { get; set; }

    public string Nickname { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Sex { get; set; } = null!;

    public TimeOnly RegistrationTime { get; set; }

    public DateOnly RegistrationDate { get; set; }

    public int Score { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
