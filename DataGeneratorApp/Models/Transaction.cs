using System;
using System.Collections.Generic;

namespace DataGeneratorApp.Models;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public int PlayerId { get; set; }

    public int CharityFundId { get; set; }

    public double MoneyCount { get; set; }

    public TimeOnly TransactionTime { get; set; }

    public DateOnly TransactionDate { get; set; }

    public virtual CharityFund CharityFund { get; set; } = null!;

    public virtual Player Player { get; set; } = null!;
}
