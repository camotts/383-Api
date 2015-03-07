using GamingStoreAPI.DAL;
using GamingStoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GamingStoreAPI.Services
{
    public class SaleRepository:ISaleRepository
    {
        
        private DataContext db = new DataContext();

        public IEnumerable<Sale> getListOfSales()
        {
            return db.Sales.AsEnumerable();
        }


        public Models.Sale getSaleById(int Id)
        {
            //Sale searchSale = new Sale(); 
            var id = db.Sales.FirstOrDefault(g => g.EmployeeID == Id);
            return id;
        }

       // public Models.Sale getSaleByEmployeeId(int Id)
       // {
          //  Sale searchEmployee = db.Sales.Find(Id);
           // return searchEmployee;
       // }
        

    


       

}
    


    }
