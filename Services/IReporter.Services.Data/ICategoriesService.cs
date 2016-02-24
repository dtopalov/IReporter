namespace IReporter.Services.Data
{
    using System.Linq;

    using IReporter.Data.Models;

    public interface ICategoriesService
    {
        IQueryable<Category> GetAll();

        IQueryable<Category> GetAllWithDeleted();

        Category EnsureCategory(string name);
    }
}
