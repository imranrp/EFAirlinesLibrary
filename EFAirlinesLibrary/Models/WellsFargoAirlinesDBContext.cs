using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EFAirlinesLibrary.Models;

public partial class WellsFargoAirlinesDBContext : DbContext
{
    public WellsFargoAirlinesDBContext()
    {
    }

    public WellsFargoAirlinesDBContext(DbContextOptions<WellsFargoAirlinesDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Flight> Flights { get; set; }

    public virtual DbSet<FlightSchedule> FlightSchedules { get; set; }

    public virtual DbSet<ReservationDetail> ReservationDetails { get; set; }

    public virtual DbSet<ReservationMaster> ReservationMasters { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=(localdb)\\MSSQLLocalDB; database=WellsFargoAirlinesDB; integrated security=true");
}
