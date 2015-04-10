using Newtonsoft.Json;
using NLog;
using System.IO;
using System.Text;

namespace RDSWebService
{
    public static class Utils
    {
        public static string GetSerializedData(Stream stream)
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        public static byte[] ToByteArray(this string value)
        {
            return Encoding.ASCII.GetBytes(value);
        }

        public static JsonSerializerSettings GetSerializerSettings()
        {
            return new JsonSerializerSettings() { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
        }

        public static void LogError(string className, string methodName, string errorMessage)
        {
            Logger logger = LogManager.GetLogger("RDSWebServiceErrorLog");
            logger.Error("Class:" + className + " Method:" + methodName + " " + errorMessage);
        }
    }
}