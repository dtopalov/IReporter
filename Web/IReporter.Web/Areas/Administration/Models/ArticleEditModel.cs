namespace IReporter.Web.Areas.Administration.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using AutoMapper;

    using Data.Common.Models;
    using Data.Models;
    using Infrastructure.Mapping;

    public class ArticleEditModel : IMapFrom<Article>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 100, ErrorMessage = "{0} must be between {2} and {1} characters long.", MinimumLength = 3)]
        public string Title { get; set; }

        public string PrimaryImageUrl { get; set; }

        [Required]
        [AllowHtml]
        [StringLength(maximumLength: 1300, ErrorMessage = "{0} must be between {2} and {1} characters long.", MinimumLength = 20)]
        public string Excerpt { get; set; }

        [Required]
        [AllowHtml]
        [StringLength(maximumLength: 10000, ErrorMessage = "{0} must be between {2} and {1} characters long.", MinimumLength = 20)]
        public string Content { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public string Category { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Article, ArticleEditModel>()
                .ForMember(x => x.Category, opt => opt.MapFrom(x => x.Category.Name));
        }
    }
}