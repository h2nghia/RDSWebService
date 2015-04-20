using RDSWebService.RequestObjects;
using RDSWebService.ResponseObjects;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace RDSWebService.BusinessLogic
{
    public class DriverBL
    {
        public AuthenticationResponse Authenticate(AuthenticationRequest request)
        {
            AuthenticationResponse response = new AuthenticationResponse();

            string commandText = "SELECT DriverNo, Password " +
                "FROM Driver " +
                "WHERE DriverNo = @driverNo";

            try
            {
                if (request == null) { throw new NullReferenceException("Json request required"); }
                if (request.DriverNo == 0) { throw new ArgumentNullException("DriverNo"); }
                if (string.IsNullOrEmpty(request.Password)) { throw new ArgumentNullException("Password"); }

                using (SqlConnection connection = new SqlConnection(Constants.DB_CONNECTION_STRING))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.Text;
                        command.CommandText = commandText;

                        command.Parameters.Add(new SqlParameter("@driverNo", request.DriverNo));

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    if (Convert.ToString(reader["Password"]) == request.Password)
                                    {
                                        response.Authentic = true;
                                        response.ResponseStatusInternal = ResponseStatus.Success;

                                        UpdateLastLoginDateTime(connection, request.DriverNo);
                                    }
                                    else
                                    {
                                        response.Authentic = false;
                                        response.ResponseStatusInternal = ResponseStatus.Exception;
                                        response.ResponseMessage = "Invalid Password";
                                    }
                                }
                            }
                            else
                            {
                                response.Authentic = false;
                                response.ResponseStatusInternal = ResponseStatus.Exception;
                                response.ResponseMessage = "Invalid Driver #";
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                response.Authentic = false;
                response.ResponseStatusInternal = ResponseStatus.Exception;
                response.ResponseMessage = e.Message;
                Utils.LogError("DriverBL", "Authenticate", e.Message);
            }

            return response;
        }

        public SettingResponse GetSettings()
        {
             SettingResponse settingResponse = new SettingResponse();
            try{
                settingResponse.DownloadOrderInterval = WebConfigurationManager.AppSettings["DownloadOrderIntervalInMillisecond"];
                settingResponse.DownloadMessageInterval = WebConfigurationManager.AppSettings["DownloadMessageIntervalInMillisecond"];
                settingResponse.UploadServiceInterval = WebConfigurationManager.AppSettings["UploadServiceIntervalInMillisecond"];
                settingResponse.LocationServiceInterval = WebConfigurationManager.AppSettings["LocationServiceIntervalInMillisecond"];
                settingResponse.LocationUpdateInterval = WebConfigurationManager.AppSettings["LocationUpdateIntervalInMillisecond"];
                settingResponse.FastestLocationUpdateInterval = WebConfigurationManager.AppSettings["FastestLocationUpdateIntervalInMillisecond"];
                settingResponse.SendGpsMessageWhenOffline = WebConfigurationManager.AppSettings["SendGpsMessageWhenOffline"];

                settingResponse.ResponseStatusInternal = ResponseStatus.Success;
                settingResponse.ResponseMessage = "Setting Received";
            }
            catch(Exception ex){
                settingResponse.ResponseStatusInternal = ResponseStatus.Exception;
                settingResponse.ResponseMessage = ex.Message;
            }
            return settingResponse;
        }

     
        private void UpdateLastLoginDateTime(SqlConnection connection, int driverNo)
        {
            string commandText = "UPDATE Driver " +
                "SET LastLoginDateTime = @lastLoginDateTime " +
                "WHERE DriverNo = @driverNo";

            try
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = commandText;

                    command.Parameters.Add(new SqlParameter("@lastLoginDateTime", DateTime.UtcNow));
                    command.Parameters.Add(new SqlParameter("@driverNo", driverNo));

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Utils.LogError("DriverBL", "UpdateLastLoginDateTime", e.Message);
            }
        }
    }
}