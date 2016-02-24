namespace IReporter.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using IReporter.Common;
    using IReporter.Data.Models;
    using IReporter.Services.Data;
    using IReporter.Web.Controllers;
    using IReporter.Web.Infrastructure.Mapping;
    using IReporter.Web.ViewModels.Home;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using Microsoft.AspNet.Identity;

    using Models;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class CommentsController : BaseController
    {
        private readonly ICommentsService comments;
        private readonly IArticlesService articles;

        public CommentsController(ICommentsService comments, IArticlesService articles)
        {
            this.comments = comments;
            this.articles = articles;
        }

        public ActionResult GetAllComments()
        {
            var allArticles = this.articles.GetAll().To<ArticleEditModel>().ToList();
            this.ViewData["articles"] = allArticles;
            return this.PartialView("_AllComments");
        }

        [HttpPost]
        public ActionResult CommentsRead([DataSourceRequest]DataSourceRequest request)
        {
            var allComments = this.comments.GetAllWithDeleted()
                .OrderByDescending(x => x.CreatedOn)
                .To<CommentEditModel>()
                .ToList();

            return this.Json(allComments.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult CommentsUpdate([DataSourceRequest]DataSourceRequest request, CommentEditModel comment)
        {
            if (this.ModelState.IsValid)
            {
                var originalComment = this.comments.GetAll().FirstOrDefault(a => a.Id == comment.Id);
                if (originalComment != null)
                {
                    originalComment.Content = comment.Content;
                    originalComment.ArticleId = comment.ArticleId;
                }

                this.comments.Update(originalComment);
            }

            return this.Json(new[] { comment }.ToDataSourceResult(request, this.ModelState));
        }

        [HttpPost]
        public ActionResult CommentsCreate([DataSourceRequest]DataSourceRequest request, CommentEditModel comment)
        {
            if (this.ModelState.IsValid)
            {
                var newComment = new Comment
                {
                    Content = comment.Content,
                    ArticleId = comment.ArticleId
                };
                newComment.AuthorId = this.User.Identity.GetUserId();

                this.comments.Create(newComment);
            }

            return this.Json(new[] { comment }.ToDataSourceResult(request, this.ModelState));
        }

        [HttpPost]
        public ActionResult CommentsDelete([DataSourceRequest]DataSourceRequest request, CommentEditModel comment)
        {
            if (this.ModelState.IsValid)
            {
                var originalComment = this.comments.GetAll().FirstOrDefault(a => a.Id == comment.Id);
                if (originalComment != null)
                {
                    originalComment.IsDeleted = true;
                }

                this.comments.Update(originalComment);
            }

            return this.Json(new[] { comment }.ToDataSourceResult(request, this.ModelState));
        }
    }
}
