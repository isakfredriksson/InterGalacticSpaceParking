using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//Av Isak Fredriksson
namespace InterGalacticSpaceParking.Models
{
	/// <summary>
	/// Interface for ReservationRepository
	/// </summary>
	public interface IReservationRepository
	{
		/// <summary>
		/// Method that checks if the input from form does not contain invalid values, e.g a floor that does not exist.
		/// </summary>
		/// <param name="reservation">Instance of the reservation gets passed for its properties to get checked.</param>
		/// <returns>Returns true or false depending on if the values in the properties are valid or not.</returns>
		bool CheckReservationData(Reservation reservation);
		/// <summary>
		/// Method that adds a valid registration code and then adds the object to the database.
		/// </summary>
		/// <param name="reservation">Instance of the reservation gets passed for it to get added to the database.</param>
		void AddReservation(Reservation reservation);
		/// <summary>
		/// Method that checks if the requested floor and parking lot already is occupied in the database.
		/// </summary>
		/// <param name="floorId">The FloorId property from the regarded reservation object gets passed as an int</param>
		/// <param name="parkingId">The ParkingId property from the regarded reservation object gets passed as an int</param>
		/// <returns>Returns true or false depending on if the values already exists in the database or not.</returns>
		bool IsLotReserved(int floorId, int parkingId);
		/// <summary>
		/// Method that checks if the entered registration code occurs in the database.
		/// </summary>
		/// <param name="registrationCode">The RegistrationCode property form the regarded reservation object gets passed as a string</param>
		/// <returns>Returns true or false depending if the value exists in the database or not.</returns>
		bool CheckRegistrationCode(string registrationCode);
		/// <summary>
		/// Method that extracts the regarded reservation object form the database by the registration code that gets passed.
		/// </summary>
		/// <param name="registrationCode">Registration code that gets entered in the deregistration form in the UI gets passed to this method</param>
		/// <returns>Returns the reservation object that has the same registration code as the string that gets passed</returns>
		Reservation GetReservationByCode(string registrationCode);
		/// <summary>
		/// Matches the entered registration code with a registration code found in one of the reservations in the database and then removes it.
		/// </summary>
		/// <param name="registrationCode">Registration code that gets entered in the derigestration form in the UI gets passed to this method</param>
		void RemoveReservation(string registrationCode);
		/// <summary>
		/// Method that calculates the amount that the user needs to pay after picking up her/his spaceship.
		/// </summary>
		/// <param name="startTime">The date and time that the reservation was added.</param>
		/// <param name="removeTime">The date and time that the reservation was removed.</param>
		/// <returns>Returns the total payment sum</returns>
		int CalculatePayment(DateTime startTime, DateTime removeTime);
		/// <summary>
		/// Method for generating a unique 10-digit registration code. Checks the database if the generated registration code already exists,
		/// if it does, the method will call itself again until a unique code has been generated.
		/// </summary>
		/// <returns>Returns the registration code as a string, for it to be assigned to the reservation object.</returns>
		string GenerateNumber();
	}
}
