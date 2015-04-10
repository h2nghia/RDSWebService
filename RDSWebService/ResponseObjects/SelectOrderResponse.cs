using RDSWebService.BusinessObjects;
using System.Collections.Generic;

namespace RDSWebService.ResponseObjects
{
    public class SelectOrderResponse : Response
    {
        public List<Order> Orders { get; set; }
    }
}