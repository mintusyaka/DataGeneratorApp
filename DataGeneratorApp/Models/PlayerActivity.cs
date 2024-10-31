using System;
using System.Collections.Generic;

namespace DataGeneratorApp.Models;

public partial class PlayerActivity
{
    public int PlayerId { get; set; }

    public int KilledEnemiesCount { get; set; }

    public int DeathsCount { get; set; }

    public int WatchedAdsCount { get; set; }

    public int DonatedFundsCount { get; set; }

    public int OnlineHoursCount { get; set; }

    public bool IsOnline { get; set; }

    public virtual Player Player { get; set; } = null!;
}
