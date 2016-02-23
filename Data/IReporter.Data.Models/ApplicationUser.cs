namespace IReporter.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using IReporter.Common;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        private ICollection<Article> articles;

        private ICollection<Comment> comments;

        private ICollection<Vote> votes;

        public ApplicationUser()
        {
            this.articles = new HashSet<Article>();
            this.votes = new HashSet<Vote>();
            this.comments = new HashSet<Comment>();
        }

        [DisplayName(GlobalConstants.FirstNameDisplayName)]
        [StringLength(maximumLength: 30, ErrorMessage = "{0} should be between {2} and {1} characters long.", MinimumLength = 2)]
        public string FirstName { get; set; }

        [DisplayName(GlobalConstants.LastNameDisplayName)]
        [StringLength(maximumLength: 30, ErrorMessage = "{0} should be between {2} and {1} characters long.", MinimumLength = 2)]
        public string LastName { get; set; }

        public DateTime? BirthDate { get; set; }

        public virtual ICollection<Article> Articles
        {
            get
            {
                return this.articles;
            }

            set
            {
                this.articles = value;
            }
        }

        public virtual ICollection<Comment> Comments
        {
            get
            {
                return this.comments;
            }

            set
            {
                this.comments = value;
            }
        }

        public virtual ICollection<Vote> Votes
        {
            get
            {
                return this.votes;
            }

            set
            {
                this.votes = value;
            }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            return userIdentity;
        }
    }
}
