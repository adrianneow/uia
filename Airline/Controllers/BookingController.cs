using Airline.Class;
using Airline.Models;
using Airline.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Airline.Controllers
{
    public class BookingController : Controller
    {
        //
        // GET: /Booking/
        UIAirlinesEntities db = new UIAirlinesEntities();
      
        public ActionResult Manage(string q)
        {
            string str = q;
            //var item = (from s in db.Flights where s.Origin.StartsWith(str) select s).ToList();
            var item = db.Flights.ToList();
            if(!string.IsNullOrWhiteSpace(q))
            {
                item = item.Where(p => p.DepartureVenue.ToLower().Contains(q.ToLower())).ToList();
            }
            if (item.Count != 0)
            {
                return View(item);
            }
            else
            {
                ViewBag.empty = "empty";
                return View();
            }
        }
        
        [Authorize]
        public ActionResult Book(Guid id)
        {
            var FlightID = id;
            var loginID = Session["loginID"];
            BookFlightVM item = new BookFlightVM();
            var data = db.Flights.Where(x => x.Flight_id == FlightID).First();

            item.FlightID = FlightID;
            item.Origin = data.DepartureVenue;
            item.Destination = data.DestinationVenue;
            item.DepartureTime = data.DepartureTime;
            item.ArrivalTime = data.ArrivalTime;
            return View(item);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Book(BookFlightVM model)
        {

            Booking tbl = new Booking();
            Guid loginID = (Guid)Session["loginID"];

            Guid guid = Guid.NewGuid();
            tbl.Booking_id = guid;
            tbl.Flight_id = model.FlightID;
            tbl.User_id = loginID;



            var tbl2 = db.Flights.Where(x => x.Flight_id == model.FlightID).First();

            if (tbl2.SeatRemainedNum != 0)
            {
                tbl2.SeatRemainedNum = tbl2.SeatRemainedNum - 1;
                db.SaveChanges();
                var seat = tbl2.TotalSeatNum - tbl2.SeatRemainedNum;

                if (seat >= 1 && seat <= 3)
                {
                    var seatMod = seat % 3;
                    if(seatMod == 0)
                    {
                        seatMod = 3;
                    }
                    tbl.SeatNum = "A" + seatMod.ToString();
                }

                if (seat >= 4 && seat <= 6)
                {
                    var seatMod = seat % 3;
                    if (seatMod == 0)
                    {
                        seatMod = 3;
                    }
                    tbl.SeatNum = "B" + seatMod.ToString();
                }

                if (seat >= 7 && seat <= 9)
                {
                    var seatMod = seat % 3;
                    if (seatMod == 0)
                    {
                        seatMod = 3;
                    }
                    tbl.SeatNum = "C" + seatMod.ToString();
                }

                if (seat >= 10 && seat <= 12)
                {
                    var seatMod = seat % 3;
                    if (seatMod == 0)
                    {
                        seatMod = 3;
                    }
                    tbl.SeatNum = "D" + seatMod.ToString();
                }
                if (seat >= 13 && seat <= 15)
                {
                    var seatMod = seat % 3;
                    if (seatMod == 0)
                    {
                        seatMod = 3;
                    }
                    tbl.SeatNum = "E" + seatMod.ToString();
                }
                if (seat >= 16 && seat <= 18)
                {
                    var seatMod = seat % 3;
                    if (seatMod == 0)
                    {
                        seatMod = 3;
                    }
                    tbl.SeatNum = "E" + seatMod.ToString();
                }
                if (seat >= 19 && seat <= 20)
                {
                    var seatMod = seat % 3;
                    if (seatMod == 0)
                    {
                        seatMod = 3;
                    }
                    tbl.SeatNum = "E" + seatMod.ToString();
                }
                
            }
            db.Bookings.Add(tbl);
            db.SaveChanges();
            return RedirectToAction("ViewBooking","Booking");
        }

        [Authorize]
        public ActionResult ViewBooking()
        {
            
            Guid userID = (Guid)Session["loginID"]; 

            var data = (from s in db.Bookings
                        join d in db.Flights on s.Flight_id equals d.Flight_id 
                        where s.User_id == userID
                        select new ViewBookingVM
                        {
                            Origin = d.DepartureVenue,
                            Destination = d.DestinationVenue,
                            DepartureTime = d.DepartureTime,
                            ArrivalTime = d.ArrivalTime,
                            Seat = s.SeatNum
                        }).ToList();

            ListViewBookingVM model = new ListViewBookingVM();
            model.List1 = data;

            if (data.Count != 0)
            {
                return View(model);
            }
            else
            {
                ViewBag.empty = "empty";
                    return View();
            }


        }
    }
}
