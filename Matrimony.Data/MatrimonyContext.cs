using System;
using Matrimony.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Matrimony.Data
{
    public partial class MatrimonyContext : DbContext
    {
        public MatrimonyContext()
        {
        }

        public MatrimonyContext(DbContextOptions<MatrimonyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cities> Cities { get; set; }
        public virtual DbSet<Countries> Countries { get; set; }
        public virtual DbSet<InterestShortListed> InterestShortListed { get; set; }
        public virtual DbSet<MasterFieldValue> MasterFieldValue { get; set; }
        public virtual DbSet<MasterTableMetadata> MasterTableMetadata { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<MessageRoom> MessageRoom { get; set; }
        public virtual DbSet<Notification> Notification { get; set; }
        public virtual DbSet<RecentlyViewed> RecentlyViewed { get; set; }
        public virtual DbSet<States> States { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserImage> UserImage { get; set; }
        public virtual DbSet<UserInfo> UserInfo { get; set; }
        public virtual DbSet<UserLifeStyle> UserLifeStyle { get; set; }
        public virtual DbSet<UserPreferences> UserPreferences { get; set; }
        public virtual DbSet<UserProfileCompletion> UserProfileCompletion { get; set; }
        public virtual DbSet<UserVerification> UserVerification { get; set; }

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //                optionsBuilder.UseSqlServer("Server=matrimama-dev.database.windows.net,1433;Initial Catalog=matrimama-dev;Persist Security Info=False;User ID=matrimama-admin;Password=Secret@2020_Key;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        //            }
        //        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cities>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Countries>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Phonecode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Sortname)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<InterestShortListed>(entity =>
            {
                entity.Property(e => e.InterestDateTime).HasColumnType("datetime");

                entity.Property(e => e.ShortListedDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<MasterFieldValue>(entity =>
            {
                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MasterTableMetadata>(entity =>
            {
                entity.Property(e => e.TableName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Message1)
                    .HasColumnName("Message")
                    .IsUnicode(false);

                entity.Property(e => e.OfflineUserId).IsUnicode(false);

                entity.Property(e => e.ReceiverId)
                    .HasColumnName("ReceiverID")
                    .IsUnicode(false);

                entity.Property(e => e.SenderId)
                    .HasColumnName("SenderID")
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MessageRoom>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.DateTimeLogged).HasColumnType("datetime");

                entity.Property(e => e.ReceiverId)
                    .HasColumnName("ReceiverID")
                    .IsUnicode(false);

                entity.Property(e => e.SenderId)
                    .HasColumnName("SenderID")
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.SeenDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<RecentlyViewed>(entity =>
            {
                entity.Property(e => e.ViewDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<States>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.ContactName).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleNmae)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(100);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ProviderId).HasColumnName("ProviderID");

                entity.Property(e => e.SocialId)
                    .HasColumnName("SocialID")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<UserImage>(entity =>
            {
                entity.Property(e => e.ContentType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.Image).IsRequired();
            });

            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.Property(e => e.Dob)
                    .HasColumnName("DOB")
                    .HasColumnType("date");

                entity.Property(e => e.FamilyIncomeId).HasColumnName("FamilyIncomeID");

                entity.Property(e => e.FamilyLocation).HasMaxLength(100);

                entity.Property(e => e.Gothra).HasMaxLength(100);

                entity.Property(e => e.GrewUpIn).HasMaxLength(100);

                entity.Property(e => e.Institution).HasMaxLength(200);

                entity.Property(e => e.NativePlace).HasMaxLength(100);

                entity.Property(e => e.Origin).HasMaxLength(100);

                entity.Property(e => e.Pin).HasColumnName("PIN");

                entity.Property(e => e.University).HasMaxLength(200);
            });

            modelBuilder.Entity<UserLifeStyle>(entity =>
            {
                entity.Property(e => e.Books).HasMaxLength(500);

                entity.Property(e => e.Cuisines).HasMaxLength(500);

                entity.Property(e => e.Fitness).HasMaxLength(500);

                entity.Property(e => e.Hobies).HasMaxLength(500);

                entity.Property(e => e.Interests).HasMaxLength(500);

                entity.Property(e => e.Movies).HasMaxLength(500);

                entity.Property(e => e.Musics).HasMaxLength(500);
            });

            modelBuilder.Entity<UserPreferences>(entity =>
            {
                entity.Property(e => e.Caste).HasMaxLength(50);

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.Country).HasMaxLength(50);

                entity.Property(e => e.HighestQualification).HasMaxLength(50);

                entity.Property(e => e.MaritalStatus).HasMaxLength(50);

                entity.Property(e => e.MotherTongue).HasMaxLength(50);

                entity.Property(e => e.Occupation).HasMaxLength(50);

                entity.Property(e => e.Religion).HasMaxLength(50);

                entity.Property(e => e.Specialization).HasMaxLength(50);

                entity.Property(e => e.State).HasMaxLength(50);
            });

            modelBuilder.Entity<UserVerification>(entity =>
            {
                entity.Property(e => e.EmailCodeGenDateTime).HasColumnType("datetime");

                entity.Property(e => e.MobileCodeGenDateTime).HasColumnType("datetime");

                entity.Property(e => e.ProfileLoginLogged).HasColumnType("datetime");

                entity.Property(e => e.ProfileLogoutLogged).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
