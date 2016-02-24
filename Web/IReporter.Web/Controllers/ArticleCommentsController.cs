namespace IReporter.Web.Controllers
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Http;
    using System.Web.Mvc;

    using IReporter.Data.Models;
    using IReporter.Services.Data;
    using IReporter.Web.Infrastructure;
    using IReporter.Web.Infrastructure.Mapping;
    using IReporter.Web.ViewModels.Home;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using Microsoft.AspNet.Identity;
    using ViewModels.Comment;

    public class ArticleCommentsController : BaseController
    {
        private readonly ICommentsService comments;
        private readonly ISanitizer sanitizer = new Sanitizer();

        public ArticleCommentsController(ICommentsService comments)
        {
            this.comments = comments;
        }

        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostComment(CommentPostModel comment)
        {
            var currentArticleId = comment.ArticleId;

            if (this.ModelState.IsValid)
            {
                var newComment = this.Mapper.Map<Comment>(comment);
                newComment.Content = this.sanitizer.Sanitize(newComment.Content);
                newComment.AuthorId = this.User.Identity.GetUserId();
                newComment.ArticleId = currentArticleId;
                this.comments.Create(newComment);
            }

            var articleComments =
                this.comments.GetAll().Where(c => c.ArticleId == currentArticleId).To<CommentViewModel>().ToList();

            return this.PartialView("_CommentsGrid", currentArticleId);
        }

        public ActionResult GetArticleComments([DataSourceRequest] DataSourceRequest request, [FromUri] int id)
        {
            var articleComments = this.comments.GetAll()
                .Where(c => c.ArticleId == id)
                .OrderByDescending(x => x.CreatedOn).To<CommentViewModel>();

            return this.Json(articleComments.ToDataSourceResult(request));
        }
    }
}
