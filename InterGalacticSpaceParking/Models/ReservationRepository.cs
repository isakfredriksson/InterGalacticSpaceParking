using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
//Av Isak Fredriksson
namespace InterGalacticSpaceParking.Models
{
	public class ReservationRepository : IReservationRepository
	{
		private ParkingContext parkingContext;
		public ReservationRepository(ParkingContext _parkingContext)
		{
			parkingContext = _parkingContext;
		}
		public bool CheckReservationData(Reservation reservation)
		{
			if (reservation.FloorId <= 0 || reservation.FloorId > 3 || reservation.ParkingId <= 0 || reservation.ParkingId > 15)
			{
				return false;
			}
			else
			{
				return true;
			}
		}
		public void AddReservation(Reservation reservation)
		{
			reservation.RegistrationCode = GenerateNumber();
			parkingContext.Reservations.Add(reservation);
			parkingContext.SaveChanges();
		}

		public bool IsLotReserved(int floorId, int parkingId)
		{
			if (parkingContext.Reservations.Any(r => r.ParkingId == parkingId && r.FloorId == floorId))
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		public bool CheckRegistrationCode(string registrationCode)
		{
			string pattern = @"\d{11}";
			if(Regex.IsMatch(registrationCode, pattern))
			{
				return false;
			}
			if(parkingContext.Reservations.Any(r => r.RegistrationCode == registrationCode))
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		public Reservation GetReservationByCode(string registrationCode)
		{
			return parkingContext.Reservations.Where(r => r.RegistrationCode == registrationCode).First();
		}
		public void RemoveReservation(string registrationCode)
		{
			parkingContext.Reservations.Remove(
				parkingContext.Reservations.Where(r => r.RegistrationCode == registrationCode).First());
			parkingContext.SaveChanges();
		}
		public int CalculatePayment(DateTime startTime, DateTime removeTime)
		{
			int sum = 0;
			sum += (15 * (int)(startTime - removeTime).Negate().TotalHours) 
				+ (50 * (startTime - removeTime).Negate().Days);

			return sum;
		}
		public string GenerateNumber()
		{
			Random random = new Random();
			string r = "";
			int i;
			for (i = 1; i < 11; i++)
			{
				r += random.Next(0, 9).ToString();
			}
			if (parkingContext.Reservations.Any(s => s.RegistrationCode == r))
			{
				GenerateNumber();
			}
			return r;
		}
	}
}
