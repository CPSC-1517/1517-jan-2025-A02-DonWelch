using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using WestWindSystem.DAL;
using WestWindSystem.Entities;
#endregion


namespace WestWindSystem.BLL
{
    public class ShipmentServices
    {
        #region setup of the context connection varibale and class constructor

        private readonly WestWindContext _context;

        //constructor
        internal ShipmentServices(WestWindContext registeredcontext)
        {
            _context = registeredcontext;
        }
        #endregion

        #region Services
        //retrieve all shipments for a specified month
        public List<Shipment> Shipment_GetByYearAndMonth(int year, int month)
        {
            IEnumerable<Shipment> info = _context.Shipments
                                                .Where(s => s.ShippedDate.Year == year
                                                        && s.ShippedDate.Month == month)
                                                .OrderBy(s => s.ShippedDate)
                                                ;
            return info.ToList();
        }
        #endregion
    }
}
