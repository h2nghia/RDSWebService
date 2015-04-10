namespace RDSWebService.RequestObjects
{
    public class SelectMessagesRequest
    {
        public int DriverNo { get; set; }

        public int MessageStartId { get; set; }

        public string InOutFlag { get; set; }
    }
}