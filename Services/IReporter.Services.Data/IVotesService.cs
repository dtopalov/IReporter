namespace IReporter.Services.Data
{
    using System.Linq;

    using IReporter.Data.Models;

    public interface IVotesService
    {
        IQueryable<Vote> GetAll();

        IQueryable<Vote> GetAllWithDeleted();

        Vote GetById(int id);

        void Update(Vote article);

        void Create(Vote comment);

        void Save();
    }
}
