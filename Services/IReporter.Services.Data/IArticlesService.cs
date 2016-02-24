namespace IReporter.Services.Data
{
    using System.Linq;

    using IReporter.Data.Models;

    public interface IArticlesService
    {
        IQueryable<Article> GetAll();

        IQueryable<Article> GetAllWithDeleted();

        Article GetById(string id);

        void Update(Article article);

        void Create(Article article);

        void Save();
    }
}
