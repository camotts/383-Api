using GamingStoreAPI.Models;
using GamingStoreAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GamingStoreAPI.Controllers
{
    public class SalesController : ApiController
    {
        private ISaleRepository repo = new SaleRepository();

        //[Authorize(Roles="")]
        // GET api/Sales
        public IEnumerable<Sale> GetSales()
        {
            return repo.getListOfSales();
        }

        [AllowAnonymous]
        // GET api/Sales/5
        public Sale GetSale(int id)
        {
            Sale sale = repo.getSaleById(id);
            if (sale == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return sale;
        }

       // public Sale GetEmployee(int employeeid)
       // {
          //  Sale sale = repo.getSaleByEmployeeId(employeeid);

         //   if (sale == null)
           // {
           //   throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
           // }

           // return sale;
       // }

        }
    }

