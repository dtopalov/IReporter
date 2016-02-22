namespace IReporter.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using IReporter.Data.Common.Models;

    public class Comment : BaseModel<int>
    {
        [Required]
        [StringLength(maximumLength: 300, ErrorMessage = "{0} must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string Content { get; set; }

        public int ArticleId { get; set; }

        public virtual Article Article { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }
    }
}
