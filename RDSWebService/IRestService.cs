using System.IO;
using System.ServiceModel;

namespace RDSWebService
{
    [ServiceContract]
    public interface IRestService
    {
        [OperationContract]
        Stream Process(Stream jsonStream, string type, string method);
    }
}