using Business_Logic;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    [RoutePrefix("services/articles")]
    public class ArticlesController : ApiController
    {
        private object articulo;
        private HttpResponseMessage respuesta;

        // GET: api/Article
        [HttpGet]
        public HttpResponseMessage GetAllArticles()
        {
            List<Article> list = ArticleBusiness.GetArticle();
            articulo = new
            {
                articles = list,
                success = true,
                total = list.Count
            };
            return Request.CreateResponse(HttpStatusCode.OK, articulo);
        }

        // GET: api/Article/5
        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage GetArticleId(int id)
        {
            Article article = ArticleBusiness.GetArticleById(ref id);
            if (article == null)
            {
                respuesta = Request.CreateResponse(HttpStatusCode.NotFound, MsgError("Record not Found", 404));
                return respuesta;
            }
            articulo = new
            {
                article = article,
                success = true,
                total = 1
            };
            return Request.CreateResponse(HttpStatusCode.OK, articulo);
        }

        // POST: api/Article
        [HttpPost]
        [Route("Add")]
        public HttpResponseMessage AddArticles([FromBody()] Article article)
        {
            if ((ModelState.IsValid))
            {
                articulo = new
                {
                    success = true,
                    article = ArticleBusiness.AddArticle(article)
                };
                return Request.CreateResponse(HttpStatusCode.Created, articulo);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, MsgError("Record not Found", 404));
        }

        // PUT: api/Article/5
        [HttpPost]
        [Route("Edit/{id}")]
        public HttpResponseMessage EditArticles(int id, [FromBody()] Article article)
        {

            if ((article.Id.Equals(id)))
            {
                articulo = new
                {
                    success = true,
                    article = ArticleBusiness.EditArticle(ref article)
                };
                return Request.CreateResponse(HttpStatusCode.Created, articulo);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, MsgError("Invalid Id Article", 404));
        }



        // DELETE: api/Article/5
        [HttpPost]
        [Route("Delete/{id}")]
        public HttpResponseMessage DeleteArticles(int id)
        {

            if ((!id.Equals(0)))
            {
                articulo = new
                {
                    success = true,
                    article = ArticleBusiness.DeleteArticle(id)
                };
                return Request.CreateResponse(HttpStatusCode.OK, articulo);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, MsgError("Invalid Id Article", 404));
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

