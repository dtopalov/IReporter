namespace IReporter.Services.Data
{
    using System.Linq;

    using IReporter.Data.Models;

    public interface IArticlesService
    {
        IQueryable<Article> GetAll();

        Article GetById(string id);
    }
}

