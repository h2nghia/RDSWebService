using Newtonsoft.Json;
using RDSWebService.BusinessLogic;
using RDSWebService.BusinessObjects;
using RDSWebService.RequestObjects;
using RDSWebService.ResponseObjects;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace RDSWebService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class RestService : IRestService
    {
        [WebInvoke(Method = "POST",
            UriTemplate = "{type}/{method}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        public Stream Process(Stream jsonStream, string type, string method)
        {
            string typeMethod = type + "/" + method;

            switch (typeMethod)
            {
                case "driver/authenticate":
                    AuthenticationRequest authenticationRequest = JsonConvert.DeserializeObject<AuthenticationRequest>(Utils.GetSerializedData(jsonStream));
                    return new MemoryStream(JsonConvert.SerializeObject(new DriverBL().Authenticate(authenticationRequest)).ToByteArray());
                case "driver/get_setting":
                    return new MemoryStream(JsonConvert.SerializeObject(new DriverBL().GetSettings()).ToByteArray());

                case "message/create":
                    Message message = JsonConvert.DeserializeObject<Message>(Utils.GetSerializedData(jsonStream));
                    return new MemoryStream(JsonConvert.SerializeObject(new MessageBL().CreateMessage(message)).ToByteArray());

                case "message/select":
                    SelectMessagesRequest selectMessageRequest = JsonConvert.DeserializeObject<SelectMessagesRequest>(Utils.GetSerializedData(jsonStream));
                    return new MemoryStream(JsonConvert.SerializeObject(new MessageBL().SelectMessages(selectMessageRequest)).ToByteArray());

                case "dvirmessage/create":
                    DVIRMessage dvirMessage = JsonConvert.DeserializeObject<DVIRMessage>(Utils.GetSerializedData(jsonStream));
                    return new MemoryStream(JsonConvert.SerializeObject(new DVIRMessageBL().CreateMessage(dvirMessage)).ToByteArray());

                case "form/select":
                    return new MemoryStream(JsonConvert.SerializeObject(new FormBL().SelectForms()).ToByteArray());

                case "order/select":
                    SelectOrderRequest selectOrderRequest = JsonConvert.DeserializeObject<SelectOrderRequest>(Utils.GetSerializedData(jsonStream));
                    return new MemoryStream(JsonConvert.SerializeObject(new OrderBL().SelectOrders(selectOrderRequest)).ToByteArray());

                default:
                    return new MemoryStream(JsonConvert.SerializeObject(new Response() { ResponseStatusInternal = ResponseStatus.Exception, ResponseMessage = "Invalid command " + typeMethod }, Utils.GetSerializerSettings()).ToByteArray());
            }
        }
    }
}