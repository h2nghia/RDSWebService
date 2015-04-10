using RDSWebService.BusinessObjects;
using RDSWebService.ResponseObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace RDSWebService.BusinessLogic
{
    public class FormBL
    {
        public Response SelectForms()
        {
            SelectFormsResponse response = new SelectFormsResponse();

            string commandText = "SELECT * " +
                "FROM Forms " +
                "ORDER BY FrmID";

            try
            {
                using (SqlConnection connection = new SqlConnection(Constants.DB_CONNECTION_STRING))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.Text;
                        command.CommandText = commandText;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            List<Form> formList = new List<Form>();

                            while (reader.Read())
                            {
                                Form form = new Form();
                                form.FormId = (int)reader["FrmID"];
                                form.FormName = Convert.ToString(reader["FrmName"]);
                                form.Label = Convert.ToString(reader["Label"]);
                                form.FillIn = reader["FillIn"] == DBNull.Value ? null : (int?)reader["FillIn"];
                                form.FormType = Convert.ToString(reader["FrmType"]);
                                form.CreatedDateTime = reader["CreateDateTime"] == DBNull.Value ? null : (DateTime?)reader["CreateDateTime"];
                                form.ModifiedDateTime = reader["ModifyDateTime"] == DBNull.Value ? null : (DateTime?)reader["ModifyDateTime"];
                                form.MustFillFlag = reader["MustFill"] == DBNull.Value ? false : Convert.ToBoolean(reader["MustFill"]);
                                form.DriverFlag = reader["Driver"] == DBNull.Value ? false : Convert.ToBoolean(reader["Driver"]);

                                formList.Add(form);
                            }

                            response.Forms = formList;
                            response.ResponseStatusInternal = ResponseStatus.Success;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                response.Forms = null;
                response.ResponseStatusInternal = ResponseStatus.Exception;
                response.ResponseMessage = e.Message;
                Utils.LogError("FormBL", "SelectForms", e.Message);
            }

            return response;
        }
    }
}