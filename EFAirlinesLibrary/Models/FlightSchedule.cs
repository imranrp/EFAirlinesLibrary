using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFAirlinesLibrary.Models;

[Table("FlightSchedule")]
[PrimaryKey("FlightNo", "TravelDate")]
public partial class FlightSchedule
{
    [StringLength(6)]
    public string FlightNo { get; set; }
    [Required]
    public DateTime TravelDate { get; set; }
    public DateTime? DepartTime { get; set; }
    public DateTime? ArriveTime { get; set; }

    [ForeignKey("FlightNo")]
    public virtual Flight? Flight { get; set; }

    public virtual ICollection<ReservationMaster> ReservationMasters { get; set; } = new List<ReservationMaster>();
}
