namespace IReporter.Services.Data
{
    using System.Linq;

    using IReporter.Data.Models;

    public interface ICategoriesService
    {
        IQueryable<Category> GetAll();

        Category EnsureCategory(string name);
    }
}
