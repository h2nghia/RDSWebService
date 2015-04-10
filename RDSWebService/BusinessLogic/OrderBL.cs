using RDSWebService.BusinessObjects;
using RDSWebService.RequestObjects;
using RDSWebService.ResponseObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace RDSWebService.BusinessLogic
{
    public class OrderBL
    {
        public SelectOrderResponse SelectOrders(SelectOrderRequest request)
        {
            SelectOrderResponse response = new SelectOrderResponse();

            string commandText = "SELECT * " +
                "FROM [Order] " +
                "WHERE DriverNo = @driverNo " +
                "AND LastUpdateDateTime >= @lastUpdateDateTime " +
                "ORDER BY FileNo";

            try
            {
                if (request == null) { throw new NullReferenceException("Json request required"); }
                if (request.DriverNo == 0) { throw new ArgumentNullException("DriverNo"); }

                using (SqlConnection connection = new SqlConnection(Constants.DB_CONNECTION_STRING))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.Text;
                        command.CommandText = commandText;

                        command.Parameters.Add(new SqlParameter("@driverNo", request.DriverNo));
                        command.Parameters.Add(new SqlParameter("@lastUpdateDateTime", request.LastUpdateDateTime));

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            List<Order> orderList = new List<Order>();

                            while (reader.Read())
                            {
                                Order order = new Order();
                                order.AppointmentDateTime = reader["AppointmentDateTime"] != DBNull.Value ? (DateTime?)reader["AppointmentDateTime"] : null;
                                order.BookingNo = reader["BookingNo"] != DBNull.Value ? (string)reader["BookingNo"] : string.Empty;
                                order.ChassisNo = reader["ChassisNo"] != DBNull.Value ? (string)reader["ChassisNo"] : string.Empty;
                                order.ContainerNo = reader["ContainerNo"] != DBNull.Value ? (string)reader["ContainerNo"] : string.Empty;
                                order.DriverNo = reader["DriverNo"] != DBNull.Value ? (int)reader["DriverNo"] : 0;
                                order.FileNo = reader["FileNo"] != DBNull.Value ? (int)reader["FileNo"] : 0;
                                order.HazmatFlag = reader["HazmatFlag"] != DBNull.Value ? (bool)reader["HazmatFlag"] : false;
                                order.LumperFlag = reader["LumperFlag"] != DBNull.Value ? (bool)reader["LumperFlag"] : false;
                                order.ManifestNo = reader["ManifestNo"] != DBNull.Value ? (string)reader["ManifestNo"] : string.Empty;
                                order.MoveType = reader["MoveType"] != DBNull.Value ? (string)reader["MoveType"] : string.Empty;
                                order.ParentFileNo = reader["ParentFileNo"] != DBNull.Value ? (int)reader["ParentFileNo"] : 0;
                                order.PickupNo = reader["PickupNo"] != DBNull.Value ? (string)reader["PickupNo"] : string.Empty;
                                order.PONo = reader["PONo"] != DBNull.Value ? (string)reader["PONo"] : string.Empty;
                                order.RailNo = reader["RailNo"] != DBNull.Value ? (string)reader["RailNo"] : string.Empty;
                                order.Remark1 = reader["Remark1"] != DBNull.Value ? (string)reader["Remark1"] : string.Empty;
                                order.Remark2 = reader["Remark2"] != DBNull.Value ? (string)reader["Remark2"] : string.Empty;
                                order.Remark3 = reader["Remark3"] != DBNull.Value ? (string)reader["Remark3"] : string.Empty;
                                order.Remark4 = reader["Remark4"] != DBNull.Value ? (string)reader["Remark4"] : string.Empty;
                                order.ScaleFlag = reader["ScaleFlag"] != DBNull.Value ? (bool)reader["ScaleFlag"] : false;
                                order.TripNo = reader["TripNo"] != DBNull.Value ? (string)reader["TripNo"] : string.Empty;
                                order.VoyageNo = reader["VoyageNo"] != DBNull.Value ? (string)reader["VoyageNo"] : string.Empty;
                                order.WeightFlag = reader["WeightFlag"] != DBNull.Value ? (bool)reader["WeightFlag"] : false;

                                order.Legs = GetLegs(order.FileNo);

                                orderList.Add(order);
                            }

                            response.Orders = orderList;
                            response.ResponseStatusInternal = ResponseStatus.Success;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                response.Orders = null;
                response.ResponseStatusInternal = ResponseStatus.Exception;
                response.ResponseMessage = e.Message;
                Utils.LogError("OrderBL", "SelectOrders", e.Message);
            }

            return response;
        }

        private List<Leg> GetLegs(int fileNo)
        {
            List<Leg> legList = new List<Leg>();
            string commandText = "SELECT * " +
                "FROM Leg " +
                "WHERE FileNo = @fileNo " +
                "ORDER BY LegNo";

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

                        command.Parameters.Add(new SqlParameter("@fileNo", fileNo));

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Leg leg = new Leg();
                                leg.AddressFrom = reader["AddressFrom"] != DBNull.Value ? (string)reader["AddressFrom"] : string.Empty;
                                leg.AddressTo = reader["AddressTo"] != DBNull.Value ? (string)reader["AddressTo"] : string.Empty;
                                leg.BOL = reader["BOL"] != DBNull.Value ? (string)reader["BOL"] : string.Empty;
                                leg.CityFrom = reader["CityFrom"] != DBNull.Value ? (string)reader["CityFrom"] : string.Empty;
                                leg.CityTo = reader["CityTo"] != DBNull.Value ? (string)reader["CityTo"] : string.Empty;
                                leg.Commodity = reader["Commodity"] != DBNull.Value ? (string)reader["Commodity"] : string.Empty;
                                leg.CompanyNameFrom = reader["CompanyNameFrom"] != DBNull.Value ? (string)reader["CompanyNameFrom"] : string.Empty;
                                leg.CompanyNameTo = reader["CompanyNameTo"] != DBNull.Value ? (string)reader["CompanyNameTo"] : string.Empty;
                                leg.CountFlag = reader["CountFlag"] != DBNull.Value ? (bool)reader["CountFlag"] : false;
                                leg.Destination = reader["Destination"] != DBNull.Value ? (string)reader["Destination"] : string.Empty;
                                leg.FileNo = reader["FileNo"] != DBNull.Value ? (int)reader["FileNo"] : 0;
                                leg.LegNo = reader["LegNo"] != DBNull.Value ? (int)reader["LegNo"] : 0;
                                leg.OutboundFlag = reader["OutboundFlag"] != DBNull.Value ? (bool)reader["OutboundFlag"] : false;
                                leg.Pallets = reader["Pallets"] != DBNull.Value ? (int)reader["Pallets"] : 0;
                                leg.ParentLegNo = reader["ParentLegNo"] != DBNull.Value ? (int)reader["ParentLegNo"] : 0;
                                leg.Pieces = reader["Pieces"] != DBNull.Value ? (int)reader["Pieces"] : 0;
                                leg.Seal = reader["Seal"] != DBNull.Value ? (string)reader["Seal"] : string.Empty;
                                leg.StateFrom = reader["StateFrom"] != DBNull.Value ? (string)reader["StateFrom"] : string.Empty;
                                leg.StateTo = reader["StateTo"] != DBNull.Value ? (string)reader["StateTo"] : string.Empty;
                                leg.Weight = reader["Weight"] != DBNull.Value ? (decimal)reader["Weight"] : 0;
                                leg.WeightFlag = reader["WeightFlag"] != DBNull.Value ? (bool)reader["WeightFlag"] : false;
                                leg.YardLocation = reader["YardLocation"] != DBNull.Value ? (string)reader["YardLocation"] : string.Empty;
                                leg.ZipCodeFrom = reader["ZipCodeFrom"] != DBNull.Value ? (string)reader["ZipCodeFrom"] : string.Empty;
                                leg.ZipCodeTo = reader["ZipCodeTo"] != DBNull.Value ? (string)reader["ZipCodeTo"] : string.Empty;

                                leg.LegExtras = GetLegExtras(fileNo, leg.LegNo);

                                legList.Add(leg);
                            }
                        }
                    }
                }

                return legList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private List<LegExtra> GetLegExtras(int fileNo, int legNo)
        {
            List<LegExtra> legExtraList = new List<LegExtra>();
            string commandText = "SELECT * " +
                "FROM LegExtra " +
                "WHERE FileNo = @fileNo " +
                "AND LegNo = @legNo " +
                "ORDER BY LegNo";

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

                        command.Parameters.Add(new SqlParameter("@fileNo", fileNo));
                        command.Parameters.Add(new SqlParameter("@legNo", legNo));

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                LegExtra legExtra = new LegExtra();
                                legExtra.Extra = reader["Extra"] != DBNull.Value ? (string)reader["Extra"] : string.Empty;
                                legExtra.FileNo = fileNo;
                                legExtra.LegExtraId = reader["LegExtraId"] != DBNull.Value ? (int)reader["LegExtraId"] : 0;
                                legExtra.LegNo = legNo;
                                legExtra.LegPart = reader["LegPart"] != DBNull.Value ? (string)reader["LegPart"] : string.Empty;

                                legExtraList.Add(legExtra);
                            }
                        }
                    }
                }

                return legExtraList;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}