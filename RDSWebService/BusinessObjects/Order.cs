using System;
using System.Collections.Generic;

namespace RDSWebService.BusinessObjects
{
    public class Order
    {
        public int FileNo { get; set; }

        public int ParentFileNo { get; set; }

        public int DriverNo { get; set; }

        public string VoyageNo { get; set; }

        public bool HazmatFlag { get; set; }

        public DateTime? AppointmentDateTime { get; set; }

        public string AppointmentTime { get; set; }

        public string MoveType { get; set; }

        public string ContainerNo { get; set; }

        public string ChassisNo { get; set; }

        public bool LumperFlag { get; set; }

        public bool ScaleFlag { get; set; }

        public bool WeightFlag { get; set; }

        public string BookingNo { get; set; }

        public string PONo { get; set; }

        public string TripNo { get; set; }

        public string PickupNo { get; set; }

        public string RailNo { get; set; }

        public string ManifestNo { get; set; }

        public string Comments { get; set; }

        public List<Leg> Legs { get; set; }
    }
}