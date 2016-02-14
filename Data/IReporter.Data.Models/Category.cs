namespace IReporter.Data.Models
{
    using System.Collections.Generic;

    using IReporter.Data.Common.Models;

    public class Category : BaseModel<int>
    {
        private ICollection<Article> articles;

        public Category()
        {
            this.articles = new HashSet<Article>();
        }

        public string Name { get; set; }

        public virtual ICollection<Article> Articles {
            get
            {
                return this.articles;
            }

            set
            {
                this.articles = value;
            }
        }
    }
}
