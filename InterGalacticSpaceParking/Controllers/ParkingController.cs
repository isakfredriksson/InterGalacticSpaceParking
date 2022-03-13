using InterGalacticSpaceParking.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Av Isak Fredriksson

namespace InterGalacticSpaceParking.Controllers
{
	/// <summary>
	/// Controller
	/// </summary>
	public class ParkingController : Controller
	{
		/// <summary>
		/// Dependency injection of repository 
		/// </summary>
		private readonly IReservationRepository _reservationRepository;
		public ParkingController(IReservationRepository reservationRepository)
		{
			_reservationRepository = reservationRepository;
		}
		/// <summary>
		/// Action
		/// </summary>
		/// <returns>Returns the index view in the UI</returns>
		public IActionResult Index()
		{
			return View();
		}
		/// <summary>
		/// Action
		/// </summary>
		/// <returns>Returns the form view for adding a parking reservation.</returns>
		public IActionResult AddReservation()
		{
			return View();
		}
		/// <summary>
		/// Action for posting a reservation from the form.
		/// </summary>
		/// <param name="reservation">New instance of an reservation sent from the form</param>
		/// <returns>Returns the view</returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult AddReservation(Reservation reservation)
		{
			if(!_reservationRepository.CheckReservationData(reservation))
			{
				ViewBag.ErrorMessage = "The floor number or/and parking number does not exist. Floor number: 1-3, Parking number: 1-15";
				return View();
			}
			else if(_reservationRepository.IsLotReserved(reservation.FloorId, reservation.ParkingId))
			{
				ViewBag.ErrorMessage = "That lot is already taken. Please check floor number and parking number.";
				return View();
			}
			else
			{
				_reservationRepository.AddReservation(reservation);
				return RedirectToAction("ThankYou", reservation);
			}
		}
		/// <summary>
		/// Action
		/// </summary>
		/// <returns>Returns the form view for removing an reservation</returns>
		public IActionResult RemoveReservation()
		{
			return View();
		}
		/// <summary>
		/// Action for posting a deregistration from the form.
		/// </summary>
		/// <param name="registrationCode">The registration code that the user enters in the form gets passed to this action.</param>
		/// <returns>Returns the view</returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult RemoveReservation(string registrationCode)
		{
			if (_reservationRepository.CheckRegistrationCode(registrationCode))
			{
				DateTime removeDate = DateTime.Now;
				Reservation reservation = _reservationRepository.GetReservationByCode(registrationCode);
				TempData["RemoveDate"] = removeDate;
				TempData["Sum"] = _reservationRepository.CalculatePayment(reservation.Date, removeDate).ToString();
				_reservationRepository.RemoveReservation(registrationCode);
				return RedirectToAction("Payment", reservation);
			}
			else
			{
				ViewBag.ErrorMessage = "The entered registration code does not exist, or incorrect format.";
				return View();
			}
		}
		/// <summary>
		/// Action. Cannot be called directly, can only be redirected from AddReservation action
		/// </summary>
		/// <param name="reservation">Instance of the regarded reservation gets passed from AddReservation action</param>
		/// <returns>Returns the Thank you-page when reservation has been added. Passes the instance for data display</returns>
		[NoDirectAccess]
		public IActionResult ThankYou(Reservation reservation)
		{
			return View(reservation);
		}
		/// <summary>
		/// Action. Cannot be called directly, can only be redirected from RemoveReservation action
		/// </summary>
		/// <param name="reservation">Instance of the regarded reservation gets passed from the RemoveReservation action</param>
		/// <returns>Returns the Payment-page when reservation has been added. Passes the instance for data display</returns>
		[NoDirectAccess]
		public IActionResult Payment(Reservation reservation)
		{
			return View(reservation);
		}

	}
}
