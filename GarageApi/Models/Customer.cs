

using GarageApi.Models.Vehicles;

namespace GarageApi.Models
{
    public class Customer
    {
        public string Name { get; set; }
        public int LotNumber { get; set; }
        public string Phone { get; set; }
        public Vehicle Vehicle { get; set; }
        public string TicketName { get; set; }
        public ITicket Ticket { get; set; }


        public void ChooseTicket()
        {
            switch (TicketName.ToLower().Trim())
            {
                case "vip":
                    Ticket = new Vip();
                    break;
                case "value":
                    Ticket = new Value();
                    break;
                case "regular":
                    Ticket = new Regular();
                    break;
            }
        }

    }
}