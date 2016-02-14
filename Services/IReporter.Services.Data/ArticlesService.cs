namespace IReporter.Services.Data
{
    using System.Linq;

    using IReporter.Data.Common;
    using IReporter.Data.Models;

    public class ArticlesService : IArticlesService
    {
        private readonly IDbRepository<Article> articles;

        public ArticlesService(IDbRepository<Article> articles)
        {
            this.articles = articles;
        }

        public IQueryable<Article> GetAll()
        {
            return this.articles.All();
        }

        public Article GetById(int id)
        {
            return this.articles.GetById(id);
        }
    }
}
