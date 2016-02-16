namespace IReporter.Web.ViewModels.Home
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using AutoMapper;

    using IReporter.Data.Models;
    using IReporter.Services.Web;
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

        public int Ratings { get; set; }

        public int NumberOfViews { get; set; }

        public IEnumerable<Comment> Comments { get; set; }

        public AuthorViewModel Author { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Article, ArticleViewModel>()
                .ForMember(x => x.Category, opt => opt.MapFrom(x => x.Category.Name));
        }
    }
}
