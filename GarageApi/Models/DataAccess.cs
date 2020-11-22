using GarageApi.Models.Vehicles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace GarageApi.Models
{
    public class DataAccess
    {

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
                if (result <= 0) message = "Sorry, We could not check your vehicle in.";
            }
            message = "Vehicle Added.";
            return message;
        }

        public string CheckOut(string licenseId)
        {
            string message = "Have a good day";
            using (SqlConnection sqlConnection = new SqlConnection(Helper.CnnVal("GarageDB")))
            {
                string queryString = $"DELETE FROM Vehicles WHERE LicenseId = { licenseId };";
                SqlCommand command = new SqlCommand(queryString, sqlConnection);
                sqlConnection.Open();
                int result = command.ExecuteNonQuery();
                if (result <= 0) message = "Sorry, We could not find your vehicle.";
            }
            return message;
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
                return "Your Vehicle Dimensions doesn't suitable with Ticket Dimensions." +
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

        public string GetByTicket(string ticketType)
        {
            Customer customer = new Customer();
            customer.TicketName = ticketType;
            customer.ChooseTicket();
            StringBuilder message = new StringBuilder(""); 
            using (SqlConnection sqlConnection = new SqlConnection(Helper.CnnVal("GarageDB")))
            {
                SqlCommand command = new SqlCommand("spVehicles_GetAllByTicket", sqlConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@First", SqlDbType.Int).Value = customer.Ticket.FirstLot;
                command.Parameters.AddWithValue("@Last", SqlDbType.Int).Value = customer.Ticket.LastLot;
                sqlConnection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {

                    while (reader.Read())
                    {
                        message.Append("Name : " + reader.GetString(0).Trim() + "    ");
                        message.Append("Phone : " + reader.GetString(1).Trim() + "    ");
                        message.Append("License ID : " + reader.GetString(2).Trim() + "    ");
                        message.Append("Lot number : " + reader.GetInt32(3) + "      ");
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return message.ToString();
        }

        public string GetAll()
        {
            StringBuilder message = new StringBuilder("");
            message.Append(GetByTicket("vip"));
            message.Append(GetByTicket("value"));
            message.Append(GetByTicket("regular"));
            return message.ToString();
        }
    }
}