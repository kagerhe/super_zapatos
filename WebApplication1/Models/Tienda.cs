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
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace WebApplication1.Models
{
    public class Tienda
    {
        protected string urlAll = "http://localhost:51645/Services/Stores/All";
        protected string urlId = "http://localhost:51645/Services/Stores/ById/{0}";
        protected string urlsave = "http://localhost:51645/Services/Stores/Edit/{0}";
        protected string urldelete = "http://localhost:51645/Services/Stores/Delete/{0}";
        protected string urlsavemo = "http://localhost:51645/Services/Stores/Add";
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

        public List<Store> ListarTodos()
        {
            string resp = GetApi(urlAll,"GET");
            JObject json = JObject.Parse(resp);
            var articles = json["stores"];
            List<Store> lista = new List<Store>();
            lista = JsonConvert.DeserializeObject<List<Store>>(json["stores"].ToString());
            return lista;
        }

        public Store Editar(int id)
        {

            string resp = GetApi(string.Format(urlId, id.ToString()),"GET");
            JObject json = JObject.Parse(resp);
            var storejson = json["store"];
            Store store = new Store();
            store = JsonConvert.DeserializeObject<Store>(storejson.ToString());
            return store;

        }

        public bool Guardar(int Id,Store store)
        {

            
            ASCIIEncoding encoding = new ASCIIEncoding();
            string postData = "Id=" + store.Id;
            postData += "&Name=" + store.Name;
            postData += "&Address=" + store.Address;
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

        public bool Eliminar(int Id)
        {
            string resp = GetApi(string.Format(urldelete, Id.ToString()), "Delete");
            JObject json = JObject.Parse(resp);
            return bool.Parse(json["success"].ToString());

        }

        public bool GuardarTienda(Store modelo)
        {
            

            ASCIIEncoding encoding = new ASCIIEncoding();
            string postDataStore = "Id=" + modelo.Id;
            postDataStore += "&Name=" + modelo.Name;
            postDataStore += "&Address=" + modelo.Address;
            byte[] data = encoding.GetBytes(postDataStore);

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
    }
}