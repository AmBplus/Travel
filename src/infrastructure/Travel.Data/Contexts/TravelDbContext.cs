using System;
using Microsoft.EntityFrameworkCore;
using Travel.Domain.Entities;

namespace Travel.Data.Contexts
{
    public class TravelDbContext : DbContext
    {
        public TravelDbContext(DbContextOptions<TravelDbContext> options) : base(options)
        {

        }

        private int b = 5;
        A

        public DbSet<TourList> TourLists { get; set; }
        public DbSet<TourPackage> TourPackages { get; set; }

    }
}
