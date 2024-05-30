using Caber.Models;
using Caber.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Caber.Contexts
{
    public class CaberContext : DbContext
    {
        public CaberContext(DbContextOptions<CaberContext> options) : base(options)
        {

        }

        #region DBSets
        public DbSet<User> Users { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Cab> Cabs { get; set; }
        public DbSet<Ride> Rides { get; set; }
        public DbSet<DriverRating> DriverRatings { get; set; }
        public DbSet<FavoritePlaces> FavoritePlaces { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region ValueConversions
            modelBuilder.Entity<User>()
                .Property(User => User.UserType)
                .HasConversion(value => value.ToString(), v => (UserTypeEnum)Enum.Parse(typeof(UserTypeEnum), v));

            modelBuilder.Entity<Driver>()
                .Property(driver => driver.DriverStatus)
                .HasConversion(value => value.ToString(), v => (DriverStatusEnum)Enum.Parse(typeof(DriverStatusEnum), v));
            #endregion

            #region TFT Mapping
            modelBuilder.Entity<Driver>().ToTable("drivers");
            modelBuilder.Entity<Passenger>().ToTable("passengers");
            #endregion

            #region Keys
            modelBuilder.Entity<User>().HasKey(User => User.Id);
            modelBuilder.Entity<User>().Property(User => User.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Cab>().HasKey(Cab => Cab.Id);
            modelBuilder.Entity<Cab>().Property(Cab => Cab.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Ride>().HasKey(Ride => Ride.Id);
            modelBuilder.Entity<Ride>().Property(Ride => Ride.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<DriverRating>().HasKey(DriverRating => DriverRating.Id);
            modelBuilder.Entity<DriverRating>().Property(DriverRating => DriverRating.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<FavoritePlaces>().HasKey(FavoritePlaces => FavoritePlaces.Id);
            modelBuilder.Entity<FavoritePlaces>().Property(FavoritePlaces => FavoritePlaces.Id).ValueGeneratedOnAdd();
            #endregion

            #region Indexes
            modelBuilder.Entity<User>().HasIndex(User => User.Email).IsUnique();

            modelBuilder.Entity<User>().HasIndex(User => User.Phone).IsUnique();

            modelBuilder.Entity<Cab>().HasIndex(Cab => Cab.RegistrationNumber).IsUnique();
            #endregion

            #region Relationships
            modelBuilder.Entity<Driver>()
                .HasMany(Driver => Driver.OwnedCabs)
                .WithOne(Cab => Cab.Driver)
                .HasForeignKey(cab => cab.DriverId);

            modelBuilder.Entity<Driver>()
                .HasOne(Driver => Driver.User)
                .WithOne(User => User.Driver)
                .HasForeignKey<Driver>(Driver => Driver.UserId);

            modelBuilder.Entity<Passenger>()
                .HasMany(Passenger => Passenger.FavoritePlaces)
                .WithOne(FavoritePlaces => FavoritePlaces.Passenger)
                .HasForeignKey(favPlace => favPlace.PassengerId);

            modelBuilder.Entity<Passenger>()
                .HasOne(Passenger => Passenger.User)
                .WithOne(User => User.Passenger)
                .HasForeignKey<Passenger>(Passenger => Passenger.UserId);

            modelBuilder.Entity<Ride>()
                .HasOne(Ride => Ride.Cab)
                .WithMany(Cab => Cab.Rides)
                .HasForeignKey(ride => ride.CabId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ride>()
                .HasOne(Ride => Ride.Passenger)
                .WithMany(Passenger => Passenger.Rides)
                .HasForeignKey(ride => ride.PassengerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DriverRating>()
                .HasOne(DriverRating => DriverRating.Passenger)
                .WithMany(Passenger => Passenger.DriverRatings)
                .HasForeignKey(rating => rating.PassengerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DriverRating>()
                .HasOne(DriverRating => DriverRating.Driver)
                .WithMany(Driver => Driver.DriverRatings)
                .HasForeignKey(rating => rating.DriverId)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion
        }
    }
}
