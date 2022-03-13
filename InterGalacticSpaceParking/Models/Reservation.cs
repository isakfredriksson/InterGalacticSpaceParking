using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
//Av Isak Fredriksson
namespace InterGalacticSpaceParking.Models
{
	public class Reservation
	{
		public Reservation()
		{
			Date = DateTime.Now;
		}
		[Key]
		public int ReservationId { get; set; }
		[Required]
		[Range(1, 3, ErrorMessage ="You can only choose between 1 and 3")]
		[Display(Name = "Floor number")]
		public int FloorId { get; set; }
		[Required]
		[Range(1, 15, ErrorMessage = "You can only choose between 1 and 15")]
		[Display(Name = "Parking number")]
		public int ParkingId { get; set; }
		[Required]
		[StringLength(10, MinimumLength = 10, ErrorMessage = "The registration code must be 10 digits.")]
		[Display(Name = "Registration Code")]
		public string RegistrationCode { get; set; }
		public DateTime Date { get; set; }


	}
}
