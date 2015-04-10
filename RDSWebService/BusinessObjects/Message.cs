using System;

namespace RDSWebService.BusinessObjects
{
    public class Message
    {
        public int MessageId { get; set; }

        public int DriverNo { get; set; }

        public string InOutFlag { get; set; }

        public int? FileNo { get; set; }

        public int? LegNo { get; set; }

        public string Label { get; set; }

        public string FormName { get; set; }

        public string MessageText { get; set; }

        public string Gps { get; set; }

        public DateTime? ClientDateTime { get; set; }

        public DateTime? ServerDateTime { get; set; }
    }
}