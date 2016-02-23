namespace IReporter.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper;

    using IReporter.Data.Models;
    using IReporter.Web.Infrastructure.Mapping;

    public class ArticleViewModel : IMapFrom<Article>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string PrimaryImageUrl { get; set; }

        [AllowHtml]
        public string Excerpt { get; set; }

        [AllowHtml]
        public string Content { get; set; }

        public string Category { get; set; }

        public int CategoryId { get; set; }

        public int Rating { get; set; }

        public int NumberOfViews { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }

        public bool CurrentUserHasVoted { get; set; }

        public AuthorViewModel Author { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Article, ArticleViewModel>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Category, opt => opt.MapFrom(x => x.Category.Name))
                .ForMember(x => x.Rating, opt => opt.MapFrom(x => x.Votes.Any() ? x.Votes.Sum(y => y.Value) : 0));
        }
    }
}
