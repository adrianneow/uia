//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Airline.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Flight
    {
        public System.Guid Flight_id { get; set; }
        public string DepartureVenue { get; set; }
        public string DestinationVenue { get; set; }
        public Nullable<System.DateTime> DepartureTime { get; set; }
        public Nullable<System.DateTime> ArrivalTime { get; set; }
        public Nullable<int> TotalSeatNum { get; set; }
        public Nullable<int> SeatRemainedNum { get; set; }
    }
}