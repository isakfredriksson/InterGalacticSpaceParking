using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//Av Isak Fredriksson
namespace InterGalacticSpaceParking.Models
{
	public class ParkingContext : DbContext
	{
		public ParkingContext(DbContextOptions<ParkingContext> options) : base(options)
		{
		}

		public DbSet<Reservation> Reservations { get; set; }
	}
}
