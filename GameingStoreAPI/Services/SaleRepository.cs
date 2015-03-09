using GamingStoreAPI.DAL;
using GamingStoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
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


        public Models.Sale getSaleByEmployeeId(int Id)
        {
            Sale searchEmployee = db.Sales.Find(Id);
            return searchEmployee;
        }
        public void createSale(Models.Sale sale)
        {
            db.Sales.Add(sale);
            db.SaveChanges();
        }


        public void putSale(int id, Models.Sale sale)
        {
            Sale modifySale = getSaleById(id);
            modifySale.Date = sale.Date;
            modifySale.CartID = sale.CartID;
            modifySale.EmployeeID = sale.EmployeeID;
            modifySale.TotalAmount = sale.TotalAmount;
         
            db.Entry(modifySale).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void deleteSale(Models.Sale sale)
        {
            db.Sales.Remove(sale);
            db.SaveChanges();

        }
        

    


       

}
    


    }
