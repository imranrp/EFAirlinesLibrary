using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFAirlinesLibrary.Models;

[Table("ReservationDetail")]
[PrimaryKey("PNRNo", "PassengerNo")]
public partial class ReservationDetail
{
    [StringLength(6)]
    public string PNRNo { get; set; }
    public int PassengerNo { get; set; }
    [StringLength(20)]
    public string FirstName { get; set; }
    [StringLength(20)]
    public string? LastName { get; set; }
    [StringLength(1)]
    public string? Gender { get; set; }
    public short? Age { get; set; }
    [ForeignKey("PNRNo")]
    public virtual ReservationMaster? ReservationMaster { get; set; } = null!;
}
