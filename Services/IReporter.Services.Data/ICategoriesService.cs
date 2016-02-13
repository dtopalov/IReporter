namespace IReporter.Services.Data
{
    using System.Linq;

    using IReporter.Data.Models;

    public interface ICategoriesService
    {
        IQueryable<JokeCategory> GetAll();

        JokeCategory EnsureCategory(string name);
    }
}
