using GarageApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GarageApi.Controllers
{
    public class VehiclesController : ApiController
    {
        // GET: api/Vehicles
        public IEnumerable<string> Get()
        {
            //DataAccess dataAccess = new DataAccess();
            //Vehicle vehicle = dataAccess.TestConn();
            return new string[] { "value1", "value2" };
        }

        // GET: api/Vehicles/vip
        [Route("api/Vehicles/{ticketType}")]
        [HttpGet]
        public string Get([FromUri]string ticketType)
        {
            DataAccess dataAccess = new DataAccess();
            return dataAccess.GetByTicket(ticketType);
        }

        // POST: api/Vehicles/CheckIn
        [Route("api/Vehicles/CheckIn")]
        public string Post([FromBody]Customer customer)
        {
            customer.Vehicle.ChooseClass();
            customer.ChooseTicket();
            DataAccess dataAccess = new DataAccess();
            return dataAccess.CheckIn(customer);
        }

        // DELETE: api/Vehicles/CheckOut/123456
        [Route("api/Vehicles/CheckOut/{licenseId}")]
        [HttpDelete]
        public string CheckOut([FromUri]string licenseId)
        {
            DataAccess dataAccess = new DataAccess();
            return dataAccess.CheckOut(licenseId);
        }
    }
}
