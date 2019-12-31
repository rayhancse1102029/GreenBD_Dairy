using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GreenBD_Dairy.Models;

namespace GreenBD_Dairy.Models
{
    public class GreenBD_DairyContext : DbContext
    {
        public GreenBD_DairyContext (DbContextOptions<GreenBD_DairyContext> options)
            : base(options)
        {
        }

        public DbSet<GreenBD_Dairy.Models.AmountSign> AmountSign { get; set; }

        public DbSet<GreenBD_Dairy.Models.CowCollection> CowCollection { get; set; }

        public DbSet<GreenBD_Dairy.Models.CowGroup> CowGroup { get; set; }

        public DbSet<GreenBD_Dairy.Models.CowManagement> CowManagement { get; set; }

        public DbSet<GreenBD_Dairy.Models.CowPurpose> CowPurpose { get; set; }

        public DbSet<GreenBD_Dairy.Models.Days> Days { get; set; }

        public DbSet<GreenBD_Dairy.Models.Doctors> Doctors { get; set; }

        public DbSet<GreenBD_Dairy.Models.DoctorsSchedule> DoctorsSchedule { get; set; }

        public DbSet<GreenBD_Dairy.Models.FoodManagement> FoodManagement { get; set; }

        public DbSet<GreenBD_Dairy.Models.FoodType> FoodType { get; set; }

        public DbSet<GreenBD_Dairy.Models.Gender> Gender { get; set; }

        public DbSet<GreenBD_Dairy.Models.Investment> Investment { get; set; }

        public DbSet<GreenBD_Dairy.Models.LandManagement> LandManagement { get; set; }

        public DbSet<GreenBD_Dairy.Models.Month> Month { get; set; }

        public DbSet<GreenBD_Dairy.Models.NationOfCow> NationOfCow { get; set; }

        public DbSet<GreenBD_Dairy.Models.NumberSign> NumberSign { get; set; }

        public DbSet<GreenBD_Dairy.Models.OthersPayment> OthersPayment { get; set; }

        public DbSet<GreenBD_Dairy.Models.PaymentMethod> PaymentMethod { get; set; }

        public DbSet<GreenBD_Dairy.Models.ProductBuy> ProductBuy { get; set; }

        public DbSet<GreenBD_Dairy.Models.ProductQuality> ProductQuality { get; set; }

        public DbSet<GreenBD_Dairy.Models.ProductReadyToDeliver> ProductReadyToDeliver { get; set; }

        public DbSet<GreenBD_Dairy.Models.ProductSell> ProductSell { get; set; }

        public DbSet<GreenBD_Dairy.Models.ProductType> ProductType { get; set; }

        public DbSet<GreenBD_Dairy.Models.Rank> Rank { get; set; }

        public DbSet<GreenBD_Dairy.Models.SpecialistType> SpecialistType { get; set; }

        public DbSet<GreenBD_Dairy.Models.Transport> Transport { get; set; }

        public DbSet<GreenBD_Dairy.Models.TransportType> TransportType { get; set; }

        public DbSet<GreenBD_Dairy.Models.WorkerManagement> WorkerManagement { get; set; }

        public DbSet<GreenBD_Dairy.Models.WorkerSalary> WorkerSalary { get; set; }
    }
}
