using RDSWebService.BusinessObjects;
using System.Collections.Generic;

namespace RDSWebService.ResponseObjects
{
    public class SelectMessagesResponse : Response
    {
        public List<Message> Messages { get; set; }
    }
}