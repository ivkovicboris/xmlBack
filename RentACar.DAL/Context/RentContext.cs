
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentACar.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentACar.DAL.Context
{
    public class RentContext : IdentityDbContext<User>
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ModelOfCar> CarModels { get; set; }
        public DbSet<FuelType> FuelTypes { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarBrand> CarBrands { get; set; }
        public DbSet<Ad> Ads { get; set; }
        public DbSet<AdRequest> AdRequests { get; set; }
        public DbSet<AdAdRequest> AdAdRequests { get; set; }

        public RentContext(DbContextOptions<RentContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AdAdRequest>()
                .HasKey(ar => new { ar.AdId, ar.AdRequestId });
            builder.Entity<AdAdRequest>()
                .HasOne(ar => ar.Ad)
                .WithMany(a => a.AdAdRequests)
                .HasForeignKey(ar => ar.AdId);
            builder.Entity<AdAdRequest>()
                .HasOne(ar => ar.AdRequest)
                .WithMany(r => r.AdAdRequests)
                .HasForeignKey(ar => ar.AdRequestId);
            base.OnModelCreating(builder);
        }
    }
}
