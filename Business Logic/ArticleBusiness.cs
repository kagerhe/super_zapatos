using Entity;
using Data_Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic
{
    public class ArticleBusiness
    {
        public static List<Article> GetArticle()
        {
            ArticleData obj = new ArticleData();
            return obj.GetArticle();
        }

        public static Article GetArticleById(ref int id)
        {
            ArticleData article = new ArticleData();
            return article.GetArticleById(id);
        }

        public static string AddArticle(Article artic)
        {
            ArticleData article = new ArticleData();
            return article.AddArticle(ref artic);
        }

        public static string EditArticle(ref Article artic)
        {
            ArticleData article = new ArticleData();
            return article.EditArticle(ref artic);
        }

        public static string DeleteArticle(int id)
        {
            ArticleData article = new ArticleData();
            return article.DeteleArticle(id);
        }
    }
}
