using System;

namespace RDSWebService.BusinessObjects
{
    public class DVIRMessage
    {
        public int MessageId { get; set; }

        public int DriverNo { get; set; }

        public int Sequence { get; set; }

        public string Label { get; set; }

        public string FormName { get; set; }

        public string MessageText { get; set; }

        public string Gps { get; set; }

        public DateTime? ClientDateTime { get; set; }

        public DateTime? ServerDateTime { get; set; }
    }
}