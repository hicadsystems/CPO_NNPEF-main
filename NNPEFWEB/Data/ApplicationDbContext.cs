using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NNPEFWEB.Models;
using NNPEFWEB.ViewModel;

namespace NNPEFWEB.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbContext Instance => this;
        public DbSet<ef_personalInfo> ef_personalInfos { get; set; }
        public DbSet<ef_bank> ef_banks { get; set; }
        public DbSet<ef_ship> ef_ships { get; set; }
        public DbSet<ef_branch> ef_branches { get; set; }
        public DbSet<ef_command> ef_commands { get; set; }
        public DbSet<ef_specialisationarea> ef_specialisationareas { get; set; }
        public DbSet<ef_state> ef_states { get; set; }
        public DbSet<ef_localgovt> ef_localgovts { get; set; }
        public DbSet<ef_rank> ef_ranks { get; set; }
        public DbSet<ef_personnelLogin> ef_PersonnelLogins { get; set; }
        public DbSet<ef_relationship> ef_relationships { get; set; }
        public DbSet<ef_shiplogin> ef_shiplogins { get; set; }

        public DbSet<ef_systeminfo> ef_systeminfos { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.HasDefaultSchema("Identity");
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "User");
            });
            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Role");
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
            });
            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });
            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });
            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });
            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });
            builder.Entity<ef_relationship>().HasData(
                new ef_relationship { Id = 1, description = "Mother" },
                new ef_relationship { Id = 2, description = "Father" },
                new ef_relationship { Id = 3, description = "son" },
                new ef_relationship { Id = 4, description = "Daughter" },
                new ef_relationship { Id = 5, description = "Brother" },
                new ef_relationship { Id = 6, description = "Sister" },
                new ef_relationship { Id = 7, description = "Wife" }
    
                );
        }
        //public DbSet<NNPEFWEB.ViewModel.UserViewModel> UserViewModel { get; set; }
    }
}
