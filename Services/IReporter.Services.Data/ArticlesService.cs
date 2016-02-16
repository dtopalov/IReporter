namespace IReporter.Services.Data
{
    using System.Linq;

    using IReporter.Data.Common;
    using IReporter.Data.Models;
    using IReporter.Services.Web;

    public class ArticlesService : IArticlesService
    {
        private readonly IDbRepository<Article> articles;
        private readonly IIdentifierProvider identifierProvider;

        public ArticlesService(IDbRepository<Article> articles, IIdentifierProvider identifierProvider)
        {
            this.articles = articles;
            this.identifierProvider = identifierProvider;
        }

        public IQueryable<Article> GetAll()
        {
            return this.articles.All();
        }

        public Article GetById(string id)
        {
            var intId = this.identifierProvider.DecodeId(id);
            var article = this.articles.GetById(intId);
            return article;
        }
    }
}
