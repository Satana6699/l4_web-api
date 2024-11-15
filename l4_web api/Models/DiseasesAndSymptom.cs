using System;
using System.Collections.Generic;

namespace l4_web_api.Models;

public partial class DiseasesAndSymptom
{
    public int Id { get; set; }

    public int DiseasesId { get; set; }

    public int MedicinesId { get; set; }

    public string? Dosage { get; set; }

    public virtual Disease Diseases { get; set; } = null!;

    public virtual Medicine Medicines { get; set; } = null!;
}
