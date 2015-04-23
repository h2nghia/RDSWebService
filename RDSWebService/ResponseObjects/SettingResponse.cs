using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RDSWebService.ResponseObjects
{
    public class SettingResponse : Response
    {
        public string DownloadOrderInterval { get; set; }
        public string DownloadMessageInterval { get; set; }
        public string UploadServiceInterval { get; set; }
        public string LocationServiceInterval { get; set; }
        public string LocationUpdateInterval { get; set; }
        public string FastestLocationUpdateInterval { get; set; }
        public string SendGpsMessageWhenOffline { get; set; }
        public string SyncTimeInSeconds { get; set; }
    }
}