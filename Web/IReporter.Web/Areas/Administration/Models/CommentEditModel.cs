namespace IReporter.Web.Areas.Administration.Models
{
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using IReporter.Data.Models;
    using IReporter.Web.Infrastructure.Mapping;

    public class CommentEditModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 1500, ErrorMessage = "{0} must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string Content { get; set; }

        public bool IsDeleted { get; set; }

        public int ArticleId { get; set; }

        public string Article { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Comment, CommentEditModel>()
                .ForMember(x => x.Article, opt => opt.MapFrom(x => x.Article.Title));
        }
    }
}
