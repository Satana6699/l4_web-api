using System;
using System.Collections.Generic;

namespace l4_web_api.Models;

public partial class Prescription
{
    public int Id { get; set; }

    public int FamilyMemberId { get; set; }

    public int DiseasesId { get; set; }

    public DateOnly? Date { get; set; }

    public virtual Disease Diseases { get; set; } = null!;

    public virtual FamilyMember FamilyMember { get; set; } = null!;
}
