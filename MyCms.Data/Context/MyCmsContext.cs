using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyCms.Domain.Entities;

namespace MyCms.Data.Context
{
    public class MyCmsContext : DbContext
    {
        public MyCmsContext(DbContextOptions<MyCmsContext> options) : base(options)
        {

        }

        #region User
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        #endregion

        #region News

        public DbSet<News> News { get; set; }
        public DbSet<NewsLike> NewsLikes { get; set; }
        public DbSet<NewsComment> NewsComments { get; set; }
        public DbSet<Category> Categories { get; set; }
        #endregion

        #region Fluent Api

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // read more
            // https://stackoverflow.com/questions/49326769/entity-framework-core-delete-cascade-and-required/49326983
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<NewsLike>().HasKey(x => new { x.NewsId, x.UserId });
            modelBuilder.Entity<UserRole>().HasKey(x => new { x.RoleId, x.UserId });

            modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<Role>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<UserRole>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<News>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<NewsComment>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<Category>().HasQueryFilter(u => !u.IsDeleted);

        }

        #endregion

    }
}
