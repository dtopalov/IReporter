namespace IReporter.Web.ViewModels.Comment
{
    using System.Web.Mvc;

    using IReporter.Data.Models;
    using IReporter.Web.Infrastructure.Mapping;

    public class CommentPostModel : IMapTo<Comment>
    {
        [AllowHtml]
        public string Content { get; set; }

        public int ArticleId { get; set; }

        public string AuthorId { get; set; }
    }
}
