using System.Collections.Generic;
using System.Web.Security;
using OdeToFood.Models;
using WebMatrix.WebData;

namespace OdeToFood.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<OdeToFood.Models.OdeToFoodDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(OdeToFood.Models.OdeToFoodDb context)
        {
            context.Restaurants.AddOrUpdate(r => r.Name,
                                            new Restaurant { Name = "Sabatino's", City = "Baltimore", Country = "USA" },
                                            new Restaurant { Name = "Great Lake", City = "Chicago", Country = "USA" },
                                            new Restaurant
                                                {
                                                    Name = "Smaka",
                                                    City = "Gothenburg",
                                                    Country = "Sweden",
                                                    Reviews =
                                                        new List<RestaurantReview>
                                                            {
                                                                new RestaurantReview {Rating = 9, Body = "Great food!", ReviewerName = "Don"}
                                                            }
                                                });
            for (int i = 0; i < 1000; i++)
            {
                context.Restaurants.AddOrUpdate(r => r.Name,
                    new Restaurant
                        {
                            Name = i.ToString(),
                            City = "Nowhere",
                            Country = "USA"
                        });
            }

            SeedMembership();
        }

        private void SeedMembership()
        {
            WebSecurity.InitializeDatabaseConnection("DefaultConnection",
                "UserProfile", "UserId", "UserName", autoCreateTables: true);

            var roles = (SimpleRoleProvider)Roles.Provider;
            var membership = (SimpleMembershipProvider)Membership.Provider;

            if (!roles.RoleExists("Admin"))
            {
                roles.CreateRole("Admin");
            }
            if (membership.GetUser("dvince", false) == null)
            {
                membership.CreateUserAndAccount("dvince", "cheese");
            }
            if (!roles.GetRolesForUser("dvince").Contains("Admin"))
            {
                roles.AddUsersToRoles(new[] { "dvince" }, new[] { "admin" });
            }
        }
    }
}
