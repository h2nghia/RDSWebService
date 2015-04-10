namespace RDSWebService.RequestObjects
{
    public class AuthenticationRequest
    {
        public int DriverNo { get; set; }

        public string Password { get; set; }
    }
}