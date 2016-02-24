namespace IReporter.Services.Data
{
    using System.Linq;

    using IReporter.Data.Common;
    using IReporter.Data.Models;

    public class CommentsService : ICommentsService
    {
        private readonly IDbRepository<Comment> comments;

        public CommentsService(IDbRepository<Comment> comments)
        {
            this.comments = comments;
        }

        public IQueryable<Comment> GetAll()
        {
            return this.comments.All();
        }

        public IQueryable<Comment> GetAllWithDeleted()
        {
            return this.comments.AllWithDeleted();
        }

        public Comment GetById(int id)
        {
            return this.comments.GetById(id);
        }

        public void Update(Comment comment)
        {
            this.comments.Update(comment);
            this.Save();
        }

        public void Create(Comment comment)
        {
            this.comments.Add(comment);
            this.Save();
        }

        public void Save()
        {
            this.comments.Save();
        }
    }
}
