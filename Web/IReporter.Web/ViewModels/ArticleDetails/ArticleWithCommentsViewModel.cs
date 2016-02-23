namespace IReporter.Web.ViewModels.ArticleDetails
{
    using IReporter.Web.ViewModels.Comment;
    using IReporter.Web.ViewModels.Home;

    public class ArticleWithCommentsViewModel
    {
        public ArticleViewModel Article { get; set; }

        public CommentPostModel Comment { get; set; }
    }
}
