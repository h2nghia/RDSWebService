using RDSWebService.BusinessObjects;
using RDSWebService.ResponseObjects;
using System;
using System.Data;
using System.Data.SqlClient;

namespace RDSWebService.BusinessLogic
{
    public class DVIRMessageBL
    {
        public Response CreateMessage(DVIRMessage message)
        {
            Response response = new Response();

            string commandText = "INSERT DVIRMessage (DriverNo, Sequence, Label, FormName, Message, Gps, ClientDateTime, ServerDateTime) " +
                "VALUES (@driverNo, @sequence, @label, @formName, @message, @gps, @clientDateTime, @serverDateTime)";

            try
            {
                if (message == null) { throw new NullReferenceException("Json request required"); }
                if (message.DriverNo == 0) { throw new ArgumentNullException("DriverNo"); }
                if (message.Sequence == 0) { throw new ArgumentNullException("Sequence"); }
                if (string.IsNullOrEmpty(message.Label)) { throw new ArgumentNullException("Label"); }
                if (message.ClientDateTime == DateTime.MinValue || message.ClientDateTime == null) { throw new ArgumentNullException("ClientDateTime"); }

                using (SqlConnection connection = new SqlConnection(Constants.DB_CONNECTION_STRING))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.Text;
                        command.CommandText = commandText;

                        command.Parameters.Add(new SqlParameter("@driverNo", message.DriverNo));
                        command.Parameters.Add(new SqlParameter("@sequence", message.Sequence));

                        command.Parameters.Add(new SqlParameter("@label", message.Label));

                        if (string.IsNullOrEmpty(message.FormName))
                        {
                            command.Parameters.Add(new SqlParameter("@formName", DBNull.Value));
                        }
                        else
                        {
                            command.Parameters.Add(new SqlParameter("@formName", message.FormName));
                        }

                        if (string.IsNullOrEmpty(message.MessageText))
                        {
                            command.Parameters.Add(new SqlParameter("@message", DBNull.Value));
                        }
                        else
                        {
                            command.Parameters.Add(new SqlParameter("@message", message.MessageText));
                        }

                        if (string.IsNullOrEmpty(message.MessageText))
                        {
                            command.Parameters.Add(new SqlParameter("@gps", DBNull.Value));
                        }
                        else
                        {
                            command.Parameters.Add(new SqlParameter("@gps", message.Gps));
                        }

                        command.Parameters.Add(new SqlParameter("@clientDateTime", message.ClientDateTime));
                        command.Parameters.Add(new SqlParameter("@serverDateTime", DateTime.UtcNow));

                        command.ExecuteNonQuery();
                        response.ResponseStatusInternal = ResponseStatus.Success;
                    }
                }
            }
            catch (Exception e)
            {
                response.ResponseStatusInternal = ResponseStatus.Exception;
                response.ResponseMessage = e.Message;
                Utils.LogError("DVIRMessageBL", "CreateMessage", e.Message);
            }

            return response;
        }
    }
}