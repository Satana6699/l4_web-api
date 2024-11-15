using System;
using System.Collections.Generic;

namespace l4_web_api.Models;

public partial class CostMedicine
{
    public int Id { get; set; }

    public int MedicinesId { get; set; }

    public string? Manufacturer { get; set; }

    public decimal? Price { get; set; }

    public DateOnly? Date { get; set; }

    public virtual Medicine Medicines { get; set; } = null!;
}
