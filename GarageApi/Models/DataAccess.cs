using GarageApi.Models.Vehicles;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace GarageApi.Models
{
    public class DataAccess
    {
        //public IVehicle TestConn()
        //{
        //    IVehicle vehicle = null;
        //    using (SqlConnection sqlConnection = new SqlConnection(Helper.CnnVal("GarageDB")))
        //    {
        //        string queryString = "SELECT * FROM Vehicles WHERE Id = 1";
        //        SqlCommand command = new SqlCommand(queryString, sqlConnection);
        //        sqlConnection.Open();
        //        SqlDataReader reader = command.ExecuteReader();
        //        try
        //        {
        //            while (reader.Read())
        //            {
        //                vehicle = CreateVehicle(reader);
        //            }
        //        }
        //        finally
        //        {
        //            reader.Close();
        //        }
        //    }

        //    return vehicle;
        //}

        public string CheckIn(Customer customer)
        {

            string message = "";
            if (!CheckDimensions(customer.Ticket, customer.Vehicle)) //Vehicle Dimensions doesn't suitable with Ticket  Dimensions.
            {
                message = OfferTicket(customer.Vehicle, customer.Ticket);
            }
            if (!message.Equals("")) return message; //Message not empty. Dimensions doesn't suitable.

            customer.LotNumber = AssignLot(customer.Ticket);
            if (customer.LotNumber == 0) return "There is no parking space available.";

            using (SqlConnection sqlConnection = new SqlConnection(Helper.CnnVal("GarageDB")))
            {
                string queryString = $"INSERT INTO Vehicles VALUES " +
                    $"('{ customer.Name}'," +
                    $"'{ customer.Vehicle.LicenseId }'," +
                    $"'{ customer.Phone }'," +
                    $" { customer.LotNumber });";

                SqlCommand command = new SqlCommand(queryString, sqlConnection);
                sqlConnection.Open();
                int result = command.ExecuteNonQuery();
                if (result < 0) message = "No Vehicle was Added.";
            }
            message = "Vehicle Added.";
            return message;
        }

        public bool CheckOut(string licenseId)
        {
            bool Ok = true;
            using (SqlConnection sqlConnection = new SqlConnection(Helper.CnnVal("GarageDB")))
            {
                string queryString = $"DELETE FROM Vehicles WHERE LicenseId = { licenseId };";
                SqlCommand command = new SqlCommand(queryString, sqlConnection);
                sqlConnection.Open();
                int result = command.ExecuteNonQuery();
                if (result < 0) Ok = false;
            }
            return Ok;
        }

        private bool CheckDimensions(ITicket ticket, Vehicle vehicle)
        {
            if (ticket.Height < vehicle.Height || ticket.Length < vehicle.Length || ticket.Width < vehicle.Width)
            {
                return false;
            }
            return true;
        }

        private string OfferTicket(Vehicle vehicle, ITicket customerTicket)
        {
            ITicket ticket = new Value();
            if (!CheckDimensions(ticket, vehicle))
            {
                ticket = new Vip();
                return "Your Vehicle Dimensions doesn't suitable with Ticket  Dimensions." +
                       " You need to purchase a VIP ticket with extra " + (ticket.Cost - customerTicket.Cost) + " Dollars.";
            }
            return "Your Vehicle Dimensions doesn't suitable with Ticket  Dimensions." +
                   " You need to purchase a Value ticket with extra " + (ticket.Cost - customerTicket.Cost) + " Dollars.";
        }

        private int AssignLot(ITicket ticket)
        {
            int lot = 0;

            List<int> lotList = new List<int>();
            using (SqlConnection sqlConnection = new SqlConnection(Helper.CnnVal("GarageDB")))
            {
                string queryString = $"SELECT LotNumber FROM Vehicles WHERE LotNumber BETWEEN {ticket.FirstLot} AND {ticket.LastLot};";

                SqlCommand command = new SqlCommand(queryString, sqlConnection);
                sqlConnection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        lotList.Add(reader.GetInt32(0));
                    }
                }
                finally
                {
                    reader.Close();
                }
            }

            lotList.Sort();

            for (int i = ticket.FirstLot; i <= ticket.LastLot; i++)
            {
                if (!lotList.Contains(i)) return i;
            }
            return lot;
        }


        //public Vehicle CreateVehicle(SqlDataReader reader)
        //{
        //    return new Vehicle
        //    {
        //        Id = (int)reader["id"],
        //        OwnerName = reader["OwnerName"].ToString(),
        //        LicenseId = reader["LicenseId"].ToString(),
        //        OwnerPhone = reader["OwnerPhone"].ToString(),
        //        TicketType = (int)reader["TicketType"],
        //        VehicleType = (int)reader["VehicleType"],
        //        VehicleHeight = (int)reader["VehicleHeight"],
        //        VehicleWidth = (int)reader["VehicleWidth"],
        //        VehicleLength = (int)reader["VehicleLength"],
        //        LotNumber = (int)reader["LotNumber"]
        //    };
        //}




    }
}