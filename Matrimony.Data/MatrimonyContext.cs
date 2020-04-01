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

        public virtual DbSet<MasterFieldValue> MasterFieldValue { get; set; }
        public virtual DbSet<MasterTableMetadata> MasterTableMetadata { get; set; }
        public virtual DbSet<PreferenceMaster> PreferenceMaster { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserImage> UserImage { get; set; }
        public virtual DbSet<UserInfo> UserInfo { get; set; }
        public virtual DbSet<UserLifeStyle> UserLifeStyle { get; set; }
        public virtual DbSet<UserPreferenceSetting> UserPreferenceSetting { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=LAPTOP-DVGRKESI\\SQL2017;Database=Matrimony;Integrated Security=True");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MasterFieldValue>(entity =>
            {
                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.MasterTable)
                    .WithMany(p => p.MasterFieldValue)
                    .HasForeignKey(d => d.MasterTableId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MasterFieldValue_MasterTableMetadata");
            });

            modelBuilder.Entity<MasterTableMetadata>(entity =>
            {
                entity.Property(e => e.TableName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PreferenceMaster>(entity =>
            {
                entity.Property(e => e.DataType)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FieldName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.ReferenceTable)
                    .WithMany(p => p.PreferenceMaster)
                    .HasForeignKey(d => d.ReferenceTableId)
                    .HasConstraintName("FK_PreferenceMaster_MasterTableMetadata");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.ContactName).HasMaxLength(100);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

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

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<UserImage>(entity =>
            {
                entity.Property(e => e.ContentType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Image).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserImage)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserImage_User");
            });

            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.Property(e => e.Caste).HasMaxLength(100);

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

                entity.HasOne(d => d.AnualIncome)
                    .WithMany(p => p.UserInfoAnualIncome)
                    .HasForeignKey(d => d.AnualIncomeId)
                    .HasConstraintName("FK_UserInfo_MasterFieldValueAnualIncome");

                entity.HasOne(d => d.BloodGroup)
                    .WithMany(p => p.UserInfoBloodGroup)
                    .HasForeignKey(d => d.BloodGroupId)
                    .HasConstraintName("FK_UserInfo_MasterFieldValueBlood");

                entity.HasOne(d => d.BodyType)
                    .WithMany(p => p.UserInfoBodyType)
                    .HasForeignKey(d => d.BodyTypeId)
                    .HasConstraintName("FK_UserInfo_MasterFieldValueBody");

                entity.HasOne(d => d.Citizenship)
                    .WithMany(p => p.UserInfoCitizenship)
                    .HasForeignKey(d => d.CitizenshipId)
                    .HasConstraintName("FK_UserInfo_MasterFieldValueCitizenship");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.UserInfoCity)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_UserInfo_MasterFieldValueCity");

                entity.HasOne(d => d.Complexion)
                    .WithMany(p => p.UserInfoComplexion)
                    .HasForeignKey(d => d.ComplexionId)
                    .HasConstraintName("FK_UserInfo_MasterFieldValueComplexion");

                entity.HasOne(d => d.Comunity)
                    .WithMany(p => p.UserInfoComunity)
                    .HasForeignKey(d => d.ComunityId)
                    .HasConstraintName("FK_UserInfo_MasterFieldValueComunity");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.UserInfoCountry)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_UserInfo_MasterFieldValueCountry");

                entity.HasOne(d => d.Employer)
                    .WithMany(p => p.UserInfoEmployer)
                    .HasForeignKey(d => d.EmployerId)
                    .HasConstraintName("FK_UserInfo_MasterFieldValueEmployer");

                entity.HasOne(d => d.FamilyIncome)
                    .WithMany(p => p.UserInfoFamilyIncome)
                    .HasForeignKey(d => d.FamilyIncomeId)
                    .HasConstraintName("FK_UserInfo_MasterFieldValueFamilyIncome");

                entity.HasOne(d => d.FamilyType)
                    .WithMany(p => p.UserInfoFamilyType)
                    .HasForeignKey(d => d.FamilyTypeId)
                    .HasConstraintName("FK_UserInfo_MasterFieldValueFamilyType");

                entity.HasOne(d => d.FamilyValues)
                    .WithMany(p => p.UserInfoFamilyValues)
                    .HasForeignKey(d => d.FamilyValuesId)
                    .HasConstraintName("FK_UserInfo_MasterFieldValueFamilyValues");

                entity.HasOne(d => d.FatherStatus)
                    .WithMany(p => p.UserInfoFatherStatus)
                    .HasForeignKey(d => d.FatherStatusId)
                    .HasConstraintName("FK_UserInfo_MasterFieldValueFatherStatus");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.UserInfoGender)
                    .HasForeignKey(d => d.GenderId)
                    .HasConstraintName("FK_UserInfo_MasterFieldValueGender");

                entity.HasOne(d => d.HighestQualification)
                    .WithMany(p => p.UserInfoHighestQualification)
                    .HasForeignKey(d => d.HighestQualificationId)
                    .HasConstraintName("FK_UserInfo_MasterFieldValueHighestQualification");

                entity.HasOne(d => d.HighestSpecialization)
                    .WithMany(p => p.UserInfoHighestSpecialization)
                    .HasForeignKey(d => d.HighestSpecializationId)
                    .HasConstraintName("FK_UserInfo_MasterFieldValueHighestSpecialization");

                entity.HasOne(d => d.MaritalStatus)
                    .WithMany(p => p.UserInfoMaritalStatus)
                    .HasForeignKey(d => d.MaritalStatusId)
                    .HasConstraintName("FK_UserInfo_MasterFieldValueStatus");

                entity.HasOne(d => d.MotherStatus)
                    .WithMany(p => p.UserInfoMotherStatus)
                    .HasForeignKey(d => d.MotherStatusId)
                    .HasConstraintName("FK_UserInfo_MasterFieldValueMotherStatus");

                entity.HasOne(d => d.MotherTongue)
                    .WithMany(p => p.UserInfoMotherTongue)
                    .HasForeignKey(d => d.MotherTongueId)
                    .HasConstraintName("FK_UserInfo_MasterFieldValueMotherTongue");

                entity.HasOne(d => d.Religion)
                    .WithMany(p => p.UserInfoReligion)
                    .HasForeignKey(d => d.ReligionId)
                    .HasConstraintName("FK_UserInfo_MasterFieldValueReligion");

                entity.HasOne(d => d.SecondaryQualification)
                    .WithMany(p => p.UserInfoSecondaryQualification)
                    .HasForeignKey(d => d.SecondaryQualificationId)
                    .HasConstraintName("FK_UserInfo_MasterFieldValueSecondaryQualification");

                entity.HasOne(d => d.SecondarySpecialization)
                    .WithMany(p => p.UserInfoSecondarySpecialization)
                    .HasForeignKey(d => d.SecondarySpecializationId)
                    .HasConstraintName("FK_UserInfo_MasterFieldValueSecondarySpecialization");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.UserInfoState)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK_UserInfo_MasterFieldValueState");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserInfo)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserInfo_User");

                entity.HasOne(d => d.WorkDesignation)
                    .WithMany(p => p.UserInfoWorkDesignation)
                    .HasForeignKey(d => d.WorkDesignationId)
                    .HasConstraintName("FK_UserInfo_MasterFieldValueWorkDesignation");

                entity.HasOne(d => d.WorkingSector)
                    .WithMany(p => p.UserInfoWorkingSector)
                    .HasForeignKey(d => d.WorkingSectorId)
                    .HasConstraintName("FK_UserInfo_MasterFieldValueWorkingSector");
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

                entity.HasOne(d => d.ChildrenChoice)
                    .WithMany(p => p.UserLifeStyleChildrenChoice)
                    .HasForeignKey(d => d.ChildrenChoiceId)
                    .HasConstraintName("FK_UserLifeStyle_MasterFieldValueChildrenChoice");

                entity.HasOne(d => d.Diet)
                    .WithMany(p => p.UserLifeStyleDiet)
                    .HasForeignKey(d => d.DietId)
                    .HasConstraintName("FK_UserLifeStyle_MasterFieldValueDiet");

                entity.HasOne(d => d.Drinking)
                    .WithMany(p => p.UserLifeStyleDrinking)
                    .HasForeignKey(d => d.DrinkingId)
                    .HasConstraintName("FK_UserLifeStyle_MasterFieldValueDrinking");

                entity.HasOne(d => d.HouseLivingIn)
                    .WithMany(p => p.UserLifeStyleHouseLivingIn)
                    .HasForeignKey(d => d.HouseLivingInId)
                    .HasConstraintName("FK_UserLifeStyle_MasterFieldValueHouseLiving");

                entity.HasOne(d => d.Smoking)
                    .WithMany(p => p.UserLifeStyleSmoking)
                    .HasForeignKey(d => d.SmokingId)
                    .HasConstraintName("FK_UserLifeStyle_MasterFieldValueSmoking");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserLifeStyle)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserLifeStyle_User");

                entity.HasOne(d => d.WeadingStyle)
                    .WithMany(p => p.UserLifeStyleWeadingStyle)
                    .HasForeignKey(d => d.WeadingStyleId)
                    .HasConstraintName("FK_UserLifeStyle_MasterFieldValueWeadingStyle");
            });

            modelBuilder.Entity<UserPreferenceSetting>(entity =>
            {
                entity.Property(e => e.UnconstraineValue)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.FieldValue)
                    .WithMany(p => p.UserPreferenceSetting)
                    .HasForeignKey(d => d.FieldValueId)
                    .HasConstraintName("FK_UserPreferenceSetting_MasterFieldValue");

                entity.HasOne(d => d.PreferenceMaster)
                    .WithMany(p => p.UserPreferenceSetting)
                    .HasForeignKey(d => d.PreferenceMasterId)
                    .HasConstraintName("FK_UserPreferenceSetting_PreferenceMaster");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserPreferenceSetting)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserPreferenceSetting_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
