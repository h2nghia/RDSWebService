using RDSWebService.BusinessObjects;
using System.Collections.Generic;

namespace RDSWebService.ResponseObjects
{
    public class SelectFormsResponse : Response
    {
        public List<Form> Forms { get; set; }
    }
}