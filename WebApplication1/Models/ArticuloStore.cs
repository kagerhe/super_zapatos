using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class ArticuloStore
    {
        public Article Article { get; set; }
        public List<Store> ListStore { get; set; }
        public Store Store { get; set; }
    }
}