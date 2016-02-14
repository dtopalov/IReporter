namespace IReporter.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;

    using IReporter.Common;
    using IReporter.Data.Models;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            const string AdministratorUserName = "admin@admin.com";
            const string AdministratorPassword = AdministratorUserName;

            const string RegularUserUserName = "user@user.com";
            const string RegularUserPassword = RegularUserUserName;

            const string AuthorUserName = "author@author.com";
            const string AuthorPassword = AuthorUserName;

            if (!context.Roles.Any())
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
                var admin = new ApplicationUser { UserName = AdministratorUserName, Email = AdministratorUserName };
                userManager.Create(admin, AdministratorPassword);
                var author = new ApplicationUser { UserName = AuthorUserName, Email = AuthorUserName };
                userManager.Create(author, AuthorPassword);
                var regularUser = new ApplicationUser { UserName = RegularUserUserName, Email = RegularUserUserName };
                userManager.Create(regularUser, RegularUserPassword);

                // Assign users to roles
                userManager.AddToRole(admin.Id, GlobalConstants.AdministratorRoleName);
                userManager.AddToRole(author.Id, GlobalConstants.AuthorRoleName);
                userManager.AddToRole(regularUser.Id, GlobalConstants.UserRoleName);
            }
        }
    }
}
