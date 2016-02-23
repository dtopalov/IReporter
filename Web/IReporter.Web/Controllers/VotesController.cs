namespace IReporter.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using IReporter.Data.Models;
    using IReporter.Services.Data;
    using IReporter.Services.Web;
    using IReporter.Web.ViewModels.Vote;

    using Microsoft.AspNet.Identity;

    public class VotesController : Controller
    {
        private readonly IArticlesService articles;
        private readonly IVotesService votes;
        private readonly IIdentifierProvider identifierProvider;

        private int currentArticleId;

        public VotesController(IArticlesService articles, IIdentifierProvider identifierProvider, IVotesService votes)
        {
            this.articles = articles;
            this.identifierProvider = identifierProvider;
            this.votes = votes;
        }

        [HttpPost]
        public ActionResult ProcessVote(VotePostModel vote)
        {
            if (vote != null && vote.Value != 0 && this.ModelState.IsValid)
            {
                if (vote.Value > 1)
                {
                    vote.Value = 1;
                }
                else if(vote.Value < -1)
                {
                    vote.Value = -1;
                }

                this.currentArticleId = vote.ArticleId;

                var newVote = new Vote
                                  {
                                      Value = vote.Value,
                                      ArticleId = this.currentArticleId,
                                      AuthorId = this.User.Identity.GetUserId()
                                  };
                this.votes.Create(newVote);
            }
            else
            {
                this.TempData["ErrorMessage"] = "Illegal operation!";
                return this.View("Error");
            }

            var currentStringId = this.identifierProvider.EncodeId(this.currentArticleId);
            var currentArticle = this.articles.GetById(currentStringId);
            var newRating = currentArticle.Votes.Sum(x => x.Value);

            return this.Json(new { value = newRating });
        }
    }
}
