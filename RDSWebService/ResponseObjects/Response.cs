namespace RDSWebService.ResponseObjects
{
    public class Response
    {
        internal ResponseStatus ResponseStatusInternal { get; set; }

        public string ResponseStatus
        {
            get
            {
                return ResponseStatusInternal.ToString();
            }
        }

        public string ResponseMessage { get; set; }

        public bool ShouldSerializeResponseMessage()
        {
            return (!string.IsNullOrEmpty(ResponseMessage));
        }
    }
}