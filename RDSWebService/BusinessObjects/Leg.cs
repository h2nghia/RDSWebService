using System.Collections.Generic;

namespace RDSWebService.BusinessObjects
{
    public class Leg
    {
        public int FileNo { get; set; }

        public int ParentLegNo { get; set; }

        public int LegNo { get; set; }

        public string CompanyNameFrom { get; set; }

        public string AddressFrom { get; set; }

        public string CityFrom { get; set; }

        public string StateFrom { get; set; }

        public string ZipCodeFrom { get; set; }

        public string CompanyNameTo { get; set; }

        public string AddressTo { get; set; }

        public string CityTo { get; set; }

        public string StateTo { get; set; }

        public string ZipCodeTo { get; set; }

        public bool CountFlag { get; set; }

        public bool WeightFlag { get; set; }

        public bool OutboundFlag { get; set; }

        public int Pieces { get; set; }

        public decimal Weight { get; set; }

        public string Seal { get; set; }

        public string Destination { get; set; }

        public string BOL { get; set; }

        public int Pallets { get; set; }

        public string Commodity { get; set; }

        public string YardLocation { get; set; }

        public List<LegExtra> LegExtras { get; set; }
    }
}