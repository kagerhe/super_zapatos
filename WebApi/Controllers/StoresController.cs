using Business_Logic;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
namespace WebApi2.Controllers
{
    
    [RoutePrefix("services/stores")]

    public class StoresController : ApiController
    {
        private object articulo;
        private HttpResponseMessage respuesta;

        // GET: api/Store/
         public HttpResponseMessage GetAllStores()
        {
            List<Store> list = StoreBusiness.GetStore();
            articulo = new
            {
                stores = list,
                success = true,
                total_elements = list.Count
            };
            return Request.CreateResponse(HttpStatusCode.OK, articulo);
        }

        // GET: api/Store/5
        [HttpGet]
        [Route("{id}")]
         public HttpResponseMessage GetStoreId(int id)
        {
            Store article = StoreBusiness.GetStoreById(ref id);
            if (article == null)
            {
                respuesta = Request.CreateResponse(HttpStatusCode.NotFound, MsgError("Invalid Idn Store", 404));
                return respuesta;
            }
            articulo = new
            {
                store = article,
                success = true,
                total_elements = 1
            };
            return Request.CreateResponse(HttpStatusCode.OK, articulo);
        }


        // POST: api/Store
        [HttpPost]
        [Route("Add")]
        public HttpResponseMessage AddStore([FromBody()] Store store)
        {
            if ((ModelState.IsValid))
            {
                articulo = new
                {
                    success = true,
                    store = StoreBusiness.AddStore(store)
                };
                return Request.CreateResponse(HttpStatusCode.Created, articulo);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, MsgError("Record not Found", 404));
        }

        // PUT: api/Store/5
        [HttpPost]
        [Route("Edit/{id}")]
        public HttpResponseMessage EditStore(int id, [FromBody()] Store store)
        {
            if ((store.Id.Equals(id)))
            {
                articulo = new
                {
                    success = true,
                    store = StoreBusiness.EditStore(ref store)
                };
                return Request.CreateResponse(HttpStatusCode.Created, articulo);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, MsgError("Invalid Id Store", 404));
        }

        // DELETE: api/Store/5
        [HttpPost]
        [Route("Delete/{id}")]
        public HttpResponseMessage DeleteStore(int id)
        {
            if ((!id.Equals(0)))
            {
                articulo = new
                {
                    success = true,
                    store = StoreBusiness.DeleteStore(id)
                };
                return Request.CreateResponse(HttpStatusCode.OK, articulo);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, MsgError("Invalid Id Store", 404));
        }


        private object MsgError(string v1, int v2)
        {
            var valor = new
            {
                success = false,
                error_code = v2,
                error_msg = v1
            };
            return valor;
        }
    }
}
