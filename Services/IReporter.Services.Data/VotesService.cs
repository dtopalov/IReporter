namespace IReporter.Services.Data
{
    using System.Linq;

    using IReporter.Data.Common;
    using IReporter.Data.Models;
    using IReporter.Services.Web;

    public class VotesService : IVotesService
    {
        private readonly IDbRepository<Vote> votes;

        public VotesService(IDbRepository<Vote> votes)
        {
            this.votes = votes;
        }

        public IQueryable<Vote> GetAll()
        {
            return this.votes.All();
        }

        public Vote GetById(int id)
        {
            return this.votes.GetById(id);
        }

        public void Update(Vote vote)
        {
            this.votes.Update(vote);
            this.Save();
        }

        public void Create(Vote vote)
        {
            this.votes.Add(vote);
            this.Save();
        }

        public void Save()
        {
            this.votes.Save();
        }
    }
}
