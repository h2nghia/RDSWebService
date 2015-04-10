using System;

namespace RDSWebService.BusinessObjects
{
    public class Form
    {
        public int FormId { get; set; }

        public string FormName { get; set; }

        public string Label { get; set; }

        public int? FillIn { get; set; }

        public string FormType { get; set; }

        public DateTime? CreatedDateTime { get; set; }

        public DateTime? ModifiedDateTime { get; set; }

        public bool MustFillFlag { get; set; }

        public bool DriverFlag { get; set; }
    }
}