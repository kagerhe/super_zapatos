
using Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access
{
    public class ArticleData
    {
        ConexionB DB = new ConexionB();

        public List<Article> GetArticle()
        {
            using (DB)
            {
                return DB.Articles.Include(s=>s.Store).ToList();
            }
        }

        public Article GetArticleById(int id)
        {
            using (DB)
            {
              
                return DB.Articles.Include(a=>a.Store).FirstOrDefault(x => x.Id == id);
            }
        }

        //Agregar articulo
        public string AddArticle(ref Article article)
        {
            try
            {
                //using (ConexionB based = new ConexionB() ) {
                //    based.Articles.Add(article);
                //    based.SaveChanges();

                //}
                //return "the item has been added successfully";
                using (DB)
                {
                    DB.Articles.Add(article);
                    DB.SaveChanges();
                }
                return "the item has been added successfully";
            }
            catch (Exception)
            {
                return "An error occurred when adding a new article";
            }
        }

        //permite modificar un articulo
        public string EditArticle(ref Article article)
        {
            try
            {
                using (DB)
                {
                    DB.Entry(article).State = EntityState.Modified;
                    DB.SaveChanges();
                }
                return ("Article"
                    + article.Description + "successfully edited");
            }
            catch (Exception)
            {
                return ("Error edited article" + article.Description);
            }
        }

        //Elimina un articulo
        public string DeteleArticle(int id)
        {
            Article article = null;
            try
            {
                using (DB)
                {
                    article = DB.Articles.FirstOrDefault(x => x.Id == id);
                    DB.Articles.Remove(article);
                    DB.SaveChanges();
                }
                return ("successfully"
                    + article.Description + " removed article");
            }
            catch (Exception)
            {
                return ("Error removed article" + article.Description);
            }
        }

    }
}
