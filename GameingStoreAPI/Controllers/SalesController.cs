using GamingStoreAPI.Models;
using GamingStoreAPI.Models.DTOS;
using GamingStoreAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GamingStoreAPI.Controllers
{
    public class SalesController : BaseApiController
    {
        private ISaleRepository repo = new SaleRepository();

        //[Authorize(Roles="")]
        // GET api/Sales
        public HttpResponseMessage GetListOfSales()
        {
            List<SaleDTO> ListOfSales = new List<SaleDTO>();
            foreach (var item in repo.getListOfSales())
            {

                ListOfSales.Add(TheFactory.Create(item));

            }
            return Request.CreateResponse(HttpStatusCode.OK, ListOfSales);
        }

        [AllowAnonymous]
        // GET api/Sales/5
        public HttpResponseMessage GetSale(int id)
        {
            Sale sale = repo.getSaleById(id);
            if (sale == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            SaleDTO factoredSale = TheFactory.Create(sale);



            return Request.CreateResponse(HttpStatusCode.OK, factoredSale);


          
        }


        public HttpResponseMessage PostSale(Sale sale)
        {
            if (ModelState.IsValid)
            {

                repo.createSale(sale);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, sale);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = sale.ID }));
                SaleDTO factoredSale = TheFactory.Create(sale);



                return Request.CreateResponse(HttpStatusCode.OK, factoredSale);

            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }
         // PUT api/Sales/5
        public HttpResponseMessage PutSale(int id, Sale sale)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != sale.ID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            repo.putSale(id,sale);

          
            SaleDTO factoredSale = TheFactory.Create(sale);



            return Request.CreateResponse(HttpStatusCode.OK, factoredSale);
        }

        public Sale GetEmployee(int employeeid)
        {
            Sale sale = repo.getSaleByEmployeeId(employeeid);

            if (sale == null)
            {
              throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return sale;
        }
         public HttpResponseMessage DeleteSale(int id)
        {
            Sale sale = repo.getSaleById(id);
            if (sale == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            repo.deleteSale(sale);

            return Request.CreateResponse(HttpStatusCode.OK, sale);
        }



        }
    }

