using Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace WebApplication1.Models
{
    public class Articulo
    {
        protected string urlAll = "http://localhost:51645/Services/Article/All";
        protected string urlId = "http://localhost:51645/Services/Article/ById/{0}";
        protected string urlsave = "http://localhost:51645/Services/Article/Edit/{0}";
        protected string urldelete = "http://localhost:51645/Services/Article/Delete/{0}";
        protected string urlsavemo = "http://localhost:51645/Services/Article/Add";

        private string GetApi(string url, string metodo)
        {
            string respuesta = string.Empty;

            var serializer = new JavaScriptSerializer();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = metodo;
            
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    respuesta = reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                }
                throw;
            }

            return respuesta;
        }

        public List<Article> ListarTodos()
        {

            string resp = GetApi(urlAll,"GET");
            JObject json = JObject.Parse(resp);
            var articlejson = json["articles"];
           List<Article> lista = new List<Article>();
            lista = JsonConvert.DeserializeObject<List<Article>>(articlejson.ToString());
            return lista;


        }
        public Article Editar(int id)
        {
            Article artistor = new Article();

            string resp = GetApi(string.Format(urlId, id.ToString()),"GET");
            JObject json = JObject.Parse(resp);
            var articlejson = json["article"];
            Article article = new Article();
            article = JsonConvert.DeserializeObject<Article>(articlejson.ToString());
            Tienda tien = new Tienda();
           
            return article;

        }


        public bool Guardar(int Id, Article articulo)
        {
            Tienda tienda = new Tienda();
            Store store = tienda.Editar(articulo.StoreId);


            ASCIIEncoding encoding = new ASCIIEncoding();
            string postDataStore = "Id=" + store.Id;
            postDataStore += "&Name=" + store.Name;
            postDataStore += "&Address=" + store.Address;

            string postData = "Id=" + articulo.Id;
            postData += "&StoreId=" + articulo.StoreId;
            postData += "&Store=" + postDataStore;
            postData += "&Name=" + articulo.Name;
            postData += "&Address=" + articulo.Description;
            postData += "&Price=" + articulo.Price;
            postData += "&Total_In_Shelf=" + articulo.Total_In_Shelf;
            postData += "&Total_In_Shelf=" + articulo.Total_In_Shelf;
            byte[] data = encoding.GetBytes(postData);

            // Prepare web request...
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(string.Format(urlsave, Id.ToString()));

            myRequest.Method = "PUT";
            myRequest.ContentType = "application/x-www-form-urlencoded";
            myRequest.ContentLength = data.Length;
            Stream newStream = myRequest.GetRequestStream();
            // Send the data.
            newStream.Write(data, 0, data.Length);
            newStream.Close();
            WebResponse response = myRequest.GetResponse();

            newStream = response.GetResponseStream();
            StreamReader sr = new StreamReader(newStream);
            var respuesta = sr.ReadToEnd();
            JObject json = JObject.Parse(respuesta);
            bool resp = bool.Parse(json["success"].ToString());
            var d = JsonConvert.DeserializeObject(json.ToString());
            return resp;

        }

        public bool GuardarArticulo(Article modelo)
        {
            Tienda tienda = new Tienda();
            Store store = tienda.Editar(modelo.StoreId);


            ASCIIEncoding encoding = new ASCIIEncoding();
            string postDataStore = "Id=" + store.Id;
            postDataStore += "&Name=" + store.Name;
            postDataStore += "&Address=" + store.Address;

            string postData = "Id=" + modelo.Id;
            postData += "&StoreId=" + modelo.StoreId;
            postData += "&Store=" + postDataStore;
            postData += "&Name=" + modelo.Name;
            postData += "&Address=" + modelo.Description;
            postData += "&Price=" + modelo.Price;
            postData += "&Total_In_Shelf=" + modelo.Total_In_Shelf;
            postData += "&Total_In_Shelf=" + modelo.Total_In_Shelf;
            byte[] data = encoding.GetBytes(postData);

            // Prepare web request...
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(urlsavemo);

            myRequest.Method = "POST";
            myRequest.ContentType = "application/x-www-form-urlencoded";
            myRequest.ContentLength = data.Length;
            Stream newStream = myRequest.GetRequestStream();
            // Send the data.
            newStream.Write(data, 0, data.Length);
            newStream.Close();
            WebResponse response = myRequest.GetResponse();

            newStream = response.GetResponseStream();
            StreamReader sr = new StreamReader(newStream);
            var respuesta = sr.ReadToEnd();
            JObject json = JObject.Parse(respuesta);
            bool resp = bool.Parse(json["success"].ToString());
            var d = JsonConvert.DeserializeObject(json.ToString());
            return resp;

        }

        public bool Eliminar(int Id)
        {
            string resp = GetApi(string.Format(urldelete, Id.ToString()), "Delete");
            JObject json = JObject.Parse(resp);
            //var articlejson = json["articles"];

            return true;

        }

    }
}