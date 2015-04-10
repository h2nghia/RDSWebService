using System;
namespace RDSWebService.RequestObjects
{
    public class SelectOrderRequest
    {
        public int DriverNo { get; set; }

        public DateTime LastUpdateDateTime { get; set; }
    }
}