using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFAirlinesLibrary.Models;

[Table("Flight")]
public partial class Flight
{
    [Key]
    [StringLength(6)]
    public string FlightNo { get; set; }
    [StringLength(20)]
    public string FromCity { get; set; }
    [StringLength(20)]
    public string ToCity { get; set; }

    public int? TotalSeats { get; set; }

    public virtual ICollection<FlightSchedule> FlightSchedules { get; set; } = new List<FlightSchedule>();
}
