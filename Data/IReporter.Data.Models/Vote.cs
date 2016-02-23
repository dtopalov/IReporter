namespace IReporter.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using IReporter.Data.Common.Models;

    public class Vote : BaseModel<int>
    {
        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public int ArticleId { get; set; }

        public Article Article { get; set; }

        [Range(-1, 1, ErrorMessage = "{0} must be between {1} and {2}")]
        public int Value { get; set; }
    }
}
