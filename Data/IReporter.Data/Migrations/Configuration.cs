namespace IReporter.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using IReporter.Common;
    using IReporter.Data.Models;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        private readonly List<Article> articles = new List<Article>();
        private List<Category> categories = new List<Category>();

        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            if (!context.Roles.Any())
            {
                this.SeedUsersAndRoles(context);
            }

            if (!context.Categories.Any())
            {
                this.SeedCategories(context);
            }
            else
            {
                this.categories = context.Categories.ToList();
            }

            if (!context.Articles.Any())
            {
               this.SeedArticles(context);
            }

            if (!context.Comments.Any())
            {
                this.SeedComments(context);
            }

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                var exception = ex;
            }
        }

        private void SeedUsersAndRoles(ApplicationDbContext context)
        {
            // Create admin role
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            var role = new IdentityRole { Name = GlobalConstants.AdministratorRoleName };
            roleManager.Create(role);

            // Create user role
            role = new IdentityRole { Name = GlobalConstants.UserRoleName };
            roleManager.Create(role);

            // Create author role
            role = new IdentityRole { Name = GlobalConstants.AuthorRoleName };
            roleManager.Create(role);

            // Create admin, author, and user
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var admin = new ApplicationUser { UserName = GlobalConstants.AdministratorUserName, Email = GlobalConstants.AdministratorUserName };
            userManager.Create(admin, GlobalConstants.AdministratorPassword);
            var author = new ApplicationUser { UserName = GlobalConstants.AuthorUserName, Email = GlobalConstants.AuthorUserName };
            userManager.Create(author, GlobalConstants.AuthorPassword);
            var regularUser = new ApplicationUser { UserName = GlobalConstants.RegularUserUserName, Email = GlobalConstants.RegularUserUserName };
            userManager.Create(regularUser, GlobalConstants.RegularUserPassword);

            // Assign users to roles
            userManager.AddToRole(admin.Id, GlobalConstants.AdministratorRoleName);
            userManager.AddToRole(author.Id, GlobalConstants.AuthorRoleName);
            userManager.AddToRole(regularUser.Id, GlobalConstants.UserRoleName);
        }

        private void SeedCategories(ApplicationDbContext context)
        {
            var categoryNames = new[]
                                    {
                                            "Sport", "Music", "Politics", "Cinema", "Entertainment", "Gossip", "Tech",
                                            "Business"
                                        };
            for (int i = 0; i < categoryNames.Length; i++)
            {
                var category = new Category { Name = categoryNames[i] };
                context.Categories.Add(category);
                this.categories.Add(category);
            }

            context.SaveChanges();
        }

        private void SeedArticles(ApplicationDbContext context)
        {
            var author = context.Users.FirstOrDefault(u => u.UserName == GlobalConstants.AuthorUserName);

            for (int i = 0; i < 50; i++)
            {
                if (author != null)
                {
                    var article = new Article
                    {
                        Title = "Article Title " + i,
                        AuthorId = author.Id,
                        CategoryId = (i % this.categories.Count) + 1,
                        PrimaryImageUrl = GlobalConstants.DefaultImageUrl,
                        Content = $"<img src=\"../../Content/images/{GlobalConstants.DefaultImageUrl}\" alt=\"Default image\" width=\"500\" height=\"350\" />"
                                                    + $"Some random content. Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium "
                                                    + $"doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi "
                                                    + $"architecto beatae vitae dicta sunt explicabo.<br /><br />Nemo enim ipsam voluptatem quia voluptas sit aspernatur "
                                                    + $"aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt.<br /><br />"
                                                    + $"Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia "
                                                    + $"non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem. Ut enim ad minima "
                                                    + $"veniam, quis nostrum exercitationem ullam corporis suscipit laboriosam, nisi ut aliquid ex ea commodi consequatur?<br /><br />"
                                                    + $"Quis autem vel eum iure reprehenderit qui in ea voluptate velit esse quam nihil molestiae consequatur, "
                                                    + $"vel illum qui dolorem eum fugiat quo voluptas nulla pariatur?<br /><br />Sed ut perspiciatis unde omnis iste natus error "
                                                    + $"sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis "
                                                    + $"et quasi architecto beatae vitae dicta sunt explicabo.<br /><br />Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut "
                                                    + $"odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt.<br /><br />Neque porro quisquam est,"
                                                    + $" qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut "
                                                    + $"labore et dolore magnam aliquam quaerat voluptatem.<br /><br />Ut enim ad minima veniam, quis nostrum exercitationem ullam corporis "
                                                    + $"suscipit laboriosam, nisi ut aliquid ex ea commodi consequatur? Quis autem vel eum iure reprehenderit qui in ea voluptate "
                                                    + $"velit esse quam nihil molestiae consequatur, vel illum qui dolorem eum fugiat quo voluptas nulla pariatur?",
                        Excerpt = "Some random content. Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium "
                                                    + "doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi "
                                                    + "architecto beatae vitae dicta sunt explicabo."
                    };

                    context.Articles.Add(article);
                    this.articles.Add(article);
                }
            }

            context.SaveChanges();
        }

        private void SeedComments(ApplicationDbContext context)
        {
            var user = context.Users.FirstOrDefault(u => u.UserName == GlobalConstants.RegularUserUserName);
            for (int i = 0; i < 200; i++)
            {
                if (user != null)
                {
                    var comment = new Comment
                    {
                        Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                        ArticleId = (i % 50) + 3,
                        AuthorId = user.Id
                    };
                    context.Comments.Add(comment);
                }
            }
        }
    }
}
