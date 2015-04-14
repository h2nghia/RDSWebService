namespace RDSWebService.ResponseObjects
{
    public class AuthenticationResponse : Response
    {
        public bool Authentic { get; set; }
        public string DownloadOrderInterval { get; set; }
        public string DownloadMessageInterval { get; set; }
        public string UploadServiceInterval { get; set; }
        public string LocationServiceInterval { get; set; }
        public string LocationUpdateInterval { get; set; }
        public string FastestLocationUpdateInterval { get; set; }
        public string SendGpsMessageWhenOffline { get; set; }
    }
}