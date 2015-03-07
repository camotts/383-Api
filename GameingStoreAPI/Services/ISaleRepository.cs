using GamingStoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GamingStoreAPI.Services
{
    interface ISaleRepository
    {

        IEnumerable<Sale> getListOfSales();
        Sale getSaleById(int id);
        Sale getSaleByEmployeeId(int id);

    }
}