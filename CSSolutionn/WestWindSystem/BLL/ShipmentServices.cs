
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
            //validate incoming values
            if (year < 1950 || year > DateTime.Today.AddDays(1).Year)
            {
                throw new ArgumentException($"Your year value {year} is invalid. Year is between 1950 and today");
            }
            if (month < 1 || month > 12)
            {
                throw new ArgumentException($"Your month value {month} is invalid. Month is between 1 and 12");
            }

            IEnumerable<Shipment> info = _context.Shipments
                                                .Include(s => s.ShipViaNavigation)
                                                .Where(s => s.ShippedDate.Year == year
                                                        && s.ShippedDate.Month == month)
                                                .OrderBy(s => s.ShippedDate)
                                                ;
            return info.ToList();
        }

        //this copy of the query is used for Pagination web page
        public List<Shipment> Shipment_GetByYearAndMonthPaging(int year, 
                                                                int month,
                                                                int currentpagenumber,
                                                                int itemsperpage)
        {
            //validate incoming values
            if (year < 1950 || year > DateTime.Today.AddDays(1).Year)
            {
                throw new ArgumentException($"Your year value {year} is invalid. Year is between 1950 and today");
            }
            if (month < 1 || month > 12)
            {
                throw new ArgumentException($"Your month value {month} is invalid. Month is between 1 and 12");
            }

            //when using paging you must have your final ordered version of your query
            IEnumerable<Shipment> info = _context.Shipments
                                                .Include(s => s.ShipViaNavigation)
                                                .Where(s => s.ShippedDate.Year == year
                                                        && s.ShippedDate.Month == month)
                                                .OrderBy(s => s.ShippedDate)
                                                ;

            //paging calculations
            //caluclate the number of items to skip
            //subtract 1 from the natural page number to get the page index number
            int itemsSkipped = itemsperpage * (currentpagenumber - 1);

            //Skip: skip the first x items representing previous pages
            //Take: take up to the necessary number of items on a page

            return info.Skip(itemsSkipped).Take(itemsperpage).ToList();
        }

        //return the total number of records found by the query
        //this query will NOT return the actual records
        public int Shipment_GetByYearAndMonthCount(int year,int month)
        {
            //validate incoming values
            if (year < 1950 || year > DateTime.Today.AddDays(1).Year)
            {
                throw new ArgumentException($"Your year value {year} is invalid. Year is between 1950 and today");
            }
            if (month < 1 || month > 12)
            {
                throw new ArgumentException($"Your month value {month} is invalid. Month is between 1 and 12");
            }

            //when using paging you must have your final ordered version of your query
            IEnumerable<Shipment> info = _context.Shipments
                                                .Where(s => s.ShippedDate.Year == year
                                                        && s.ShippedDate.Month == month);

          
            return info.Count();

            //return _context.Shipments
            //                .Where(s => s.ShippedDate.Year == year
            //                        && s.ShippedDate.Month == month)
            //                .Count();

            //return _context.Shipments
            //             .Count(s => s.ShippedDate.Year == year
            //                     && s.ShippedDate.Month == month);
        }
        public Shipment Shipment_GetByID(int shipmmentid)
        {
            return _context.Shipments.AsNoTracking().FirstOrDefault(s => s.ShipmentID == shipmmentid);
        }
        #endregion
    }
}
