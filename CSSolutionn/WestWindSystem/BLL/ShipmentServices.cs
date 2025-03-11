
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using WestWindSystem.DAL;
using WestWindSystem.Entities;
using Microsoft.EntityFrameworkCore;
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

        // Used to navigate on the ShipmentQuery page to the Shipper talble
        //  Technique A
        //public List<Shipment> Shipment_GetByYearAndMonth(int year, int month)
        //{
        //    IEnumerable<Shipment> info = _context.Shipments
        //                                        .Where(s => s.ShippedDate.Year == year
        //                                                && s.ShippedDate.Month == month)
        //                                        .OrderBy(s => s.ShippedDate)
        //                                        ;
        //    return info.ToList();
        //}

        //This uses the technique (b) discussed on the ShipmentTable page
        //note there is a required using class, see Additional namespaces above.
        //uses the .Include method to add navigational instances to the return record
        //note the predicate uses the virtual navigational property of the Shipment entity
        //This will include the associated record from the Shippers table (parent) for
        //      the shipment record (child)
        public List<Shipment> Shipment_GetByYearAndMonth(int year, int month)
        {
            IEnumerable<Shipment> info = _context.Shipments
                                                .Include(s => s.ShipViaNavigation)
                                                .Where(s => s.ShippedDate.Year == year
                                                        && s.ShippedDate.Month == month)
                                                .OrderBy(s => s.ShippedDate)
                                                ;
            return info.ToList();
        }
        #endregion
    }
}
