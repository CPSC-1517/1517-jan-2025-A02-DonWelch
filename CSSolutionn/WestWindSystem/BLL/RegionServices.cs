﻿using System;
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
    public class RegionServices
    {
        #region setup of the context connection varibale and class constructor

        private readonly WestWindContext _context;

        //constructor
        internal RegionServices(WestWindContext registeredcontext)
        {
            _context = registeredcontext;
        }
        #endregion

        #region Services
        //first query will be used to retreive all the records of the
        //      entity so they may be used in a select control on the
        //      web page
        //this should ONLY be considered IF the entity has very few records
        //this can be considered IF you need the data as part of your
        //      foreign key display handling on your web page

        //remember methods are your services.
        public List<Region> Region_GetAll()
        {
            //by default the two generic collections used by Linq
            //  are IQueryAble and IEnumerable
            //within your C# the collection is IEnumerable<T>

            //create a variable to catch the data returned from the
            //  DAL context DbSet property associated with the
            //  Region entity
            //
            //the call consists of the context class instance (which is an object)
            //  and the DbSet and optionally any additional Linq methods

            IEnumerable<Region> info = _context.Regions;

            //send back an ordered list of regions
            //the collection info is being passed to a Linq method called OrderBy()
            //the OrderBy() method will return a temporary collection
            //      and can be passed to the next method
            //the temporary collection is being passed to the ToList() method
            //the ToList() method will return a ist<T> collection
            //that collection is returned from the service method

            //.OrderBy() ascending 1st ordering attribute
            //.OrderDescendingBy() descending 1st ordering attribute
            //.ThenBy() ascending for each additional ordering attribute
            //.ThenDescendingBy() descending for each additional ordering attribute
            //YES: these can be mixed and matched
            return info.OrderBy(rowplaceholder => rowplaceholder.RegionDescription).ToList();
        }

        //looking up a single record given a specific value
        public Region Region_GetByID(int regionid)
        {
            Region info = null;

            //the code within the FirstOrDefault is a predicate
            //the predicate is the condition that is applied to during the methods actions
            //the predicate for this method is the condition to match to find the desired record

            //since the query is based on the pkey of the table, only one record, if any, should be found
            info = _context.Regions.FirstOrDefault(x => x.RegionID == regionid);

            //an alternate way of coding the query
            //notice that the Linq methods can be broken up onto their own lines
            //info = _context.Regions
            //                .Where(x => x.RegionID == regionid)  //filters the dataset
            //                .FirstOrDefault();

            // an alternate way of coding the query
            // .Find() searchs primary keys therefore just needs the incoming argument
            // info = _context.Regions
            //                 .Find(regionid);

            return info;

            //another alternate solution
            //IEnumerable<Region> infoSet = _context.Regions
            //                .Where(x => x.RegionID == regionid);
            //return infoSet.FirstOrDefault();
        }
        #endregion
    }
}
