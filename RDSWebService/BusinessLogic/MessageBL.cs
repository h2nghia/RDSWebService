using RDSWebService.BusinessObjects;
using RDSWebService.RequestObjects;
using RDSWebService.ResponseObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace RDSWebService.BusinessLogic
{
    public class MessageBL
    {
        public Response CreateMessage(Message message)
        {
            Response response = new Response();

            string commandText = "INSERT Message (DriverNo, InboundOutboundFlag, FileNo, LegNo, Label, FormName, Message, Gps, ClientDateTime, ServerDateTime) " +
                "VALUES (@driverNo, @inOutFlag, @fileNo, @legNo, @label, @formName, @message, @gps, @clientDateTime, @serverDateTime)";

            try
            {
                if (message == null) { throw new NullReferenceException("Json request required"); }
                if (message.DriverNo == 0) { throw new ArgumentNullException("DriverNo"); }
                if (string.IsNullOrEmpty(message.InOutFlag)) { throw new ArgumentNullException("InOutFlag"); }
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
                        command.Parameters.Add(new SqlParameter("@inOutFlag", message.InOutFlag));

                        if (message.FileNo == null || message.FileNo == 0)
                        {
                            command.Parameters.Add(new SqlParameter("@fileNo", DBNull.Value));
                        }
                        else
                        {
                            command.Parameters.Add(new SqlParameter("@fileNo", (int?)message.FileNo));
                        }

                        if (message.LegNo == null || message.LegNo == 0)
                        {
                            command.Parameters.Add(new SqlParameter("@legNo", DBNull.Value));
                        }
                        else
                        {
                            command.Parameters.Add(new SqlParameter("@legNo", (int?)message.LegNo));
                        }

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

                        if (string.IsNullOrEmpty(message.Gps))
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
                Utils.LogError("MessageBL", "CreateMessage", e.Message);
            }

            return response;
        }

        public SelectMessagesResponse SelectMessages(SelectMessagesRequest request)
        {
            SelectMessagesResponse response = new SelectMessagesResponse();

            string commandText = "SELECT * " +
                "FROM Message " +
                "WHERE MessageId > @messageStartId " +
                "AND DriverNo = @driverNo " +
                "AND InboundOutboundFlag = @inOutFlag " +
                "AND ClientDateTime > dateadd(dd, -8, getdate()) " +
                "ORDER BY MessageId";

            try
            {
                if (request == null) { throw new NullReferenceException("Json request required"); }
                if (request.DriverNo == 0) { throw new ArgumentNullException("DriverNo"); }
                if (string.IsNullOrEmpty(request.InOutFlag)) { throw new ArgumentNullException("InOutFlag"); }

                using (SqlConnection connection = new SqlConnection(Constants.DB_CONNECTION_STRING))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.Text;
                        command.CommandText = commandText;

                        command.Parameters.Add(new SqlParameter("@messageStartId", request.MessageStartId));
                        command.Parameters.Add(new SqlParameter("@driverNo", request.DriverNo));
                        command.Parameters.Add(new SqlParameter("@inOutFlag", request.InOutFlag));

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            List<Message> messageList = new List<Message>();

                            while (reader.Read())
                            {
                                Message message = new Message();
                                message.MessageId = (int)reader["MessageId"];
                                message.DriverNo = (int)reader["DriverNo"];
                                message.InOutFlag = Convert.ToString(reader["InboundOutboundFlag"]);
                                message.FileNo = reader["FileNo"] == DBNull.Value ? null : (int?)reader["FileNo"];
                                message.FileNo = reader["LegNo"] == DBNull.Value ? null : (int?)reader["LegNo"];
                                message.Label = Convert.ToString(reader["Label"]);
                                message.FormName = Convert.ToString(reader["FormName"]);
                                message.MessageText = Convert.ToString(reader["Message"]);
                                message.ClientDateTime = reader["ClientDateTime"] == DBNull.Value ? null : (DateTime?)reader["ClientDateTime"];
                                message.ServerDateTime = reader["ServerDateTime"] == DBNull.Value ? null : (DateTime?)reader["ServerDateTime"];

                                messageList.Add(message);
                            }

                            response.Messages = messageList;
                            response.ResponseStatusInternal = ResponseStatus.Success;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                response.Messages = null;
                response.ResponseStatusInternal = ResponseStatus.Exception;
                response.ResponseMessage = e.Message;
                Utils.LogError("MessageBL", "SelectMessages", e.Message);
            }

            return response;
        }
    }
}