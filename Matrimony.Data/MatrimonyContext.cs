using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Matrimony.Data.Entities;

namespace Matrimony.Data
{
    public partial class MatrimonyContext : DbContext
    {
        //public MatrimonyContext()
        //{
        //}

        public MatrimonyContext(DbContextOptions<MatrimonyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MasterFieldValue> MasterFieldValue { get; set; }
        public virtual DbSet<MasterTableMetadata> MasterTableMetadata { get; set; }
        public virtual DbSet<PreferenceMaster> PreferenceMaster { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserBasicInfo> UserBasicInfo { get; set; }
        public virtual DbSet<UserCareer> UserCareer { get; set; }
        public virtual DbSet<UserEducation> UserEducation { get; set; }
        public virtual DbSet<UserFamilyInfo> UserFamilyInfo { get; set; }
        public virtual DbSet<UserLifeStyle> UserLifeStyle { get; set; }
        public virtual DbSet<UserLocation> UserLocation { get; set; }
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
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.MasterTableId).ValueGeneratedOnAdd();

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
                entity.Property(e => e.ContactName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
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
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<UserBasicInfo>(entity =>
            {
                entity.Property(e => e.Dob)
                    .HasColumnName("DOB")
                    .HasColumnType("date");

                entity.Property(e => e.Gothra)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.BloodGroup)
                    .WithMany(p => p.UserBasicInfoBloodGroup)
                    .HasForeignKey(d => d.BloodGroupId)
                    .HasConstraintName("FK_UserBasicInfo_MasterFieldValueBlood");

                entity.HasOne(d => d.Comunity)
                    .WithMany(p => p.UserBasicInfoComunity)
                    .HasForeignKey(d => d.ComunityId)
                    .HasConstraintName("FK_UserBasicInfo_MasterFieldValueComunity");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.UserBasicInfoGender)
                    .HasForeignKey(d => d.GenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserBasicInfo_MasterFieldValueGender");

                entity.HasOne(d => d.HealthInfo)
                    .WithMany(p => p.UserBasicInfoHealthInfo)
                    .HasForeignKey(d => d.HealthInfoId)
                    .HasConstraintName("FK_UserBasicInfo_MasterFieldValueHealth");

                entity.HasOne(d => d.MaritalStatus)
                    .WithMany(p => p.UserBasicInfoMaritalStatus)
                    .HasForeignKey(d => d.MaritalStatusId)
                    .HasConstraintName("FK_UserBasicInfo_MasterFieldValueStatus");

                entity.HasOne(d => d.MotherTongue)
                    .WithMany(p => p.UserBasicInfoMotherTongue)
                    .HasForeignKey(d => d.MotherTongueId)
                    .HasConstraintName("FK_UserBasicInfo_MasterFieldValueMotherTongue");

                entity.HasOne(d => d.Religion)
                    .WithMany(p => p.UserBasicInfoReligion)
                    .HasForeignKey(d => d.ReligionId)
                    .HasConstraintName("FK_UserBasicInfo_MasterFieldValueReligion");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserBasicInfo)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserBasicInfo_User");
            });

            modelBuilder.Entity<UserCareer>(entity =>
            {
                entity.HasOne(d => d.AnualIncome)
                    .WithMany(p => p.UserCareerAnualIncome)
                    .HasForeignKey(d => d.AnualIncomeId)
                    .HasConstraintName("FK_UserCareer_MasterFieldValueAnualIncome");

                entity.HasOne(d => d.Employer)
                    .WithMany(p => p.UserCareerEmployer)
                    .HasForeignKey(d => d.EmployerId)
                    .HasConstraintName("FK_UserCareer_MasterFieldValueEmployer");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserCareer)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserCareer_User");

                entity.HasOne(d => d.WorkDesignation)
                    .WithMany(p => p.UserCareerWorkDesignation)
                    .HasForeignKey(d => d.WorkDesignationId)
                    .HasConstraintName("FK_UserCareer_MasterFieldValueDesignation");

                entity.HasOne(d => d.WorkingSector)
                    .WithMany(p => p.UserCareerWorkingSector)
                    .HasForeignKey(d => d.WorkingSectorId)
                    .HasConstraintName("FK_UserCareer_MasterFieldValueSector");
            });

            modelBuilder.Entity<UserEducation>(entity =>
            {
                entity.Property(e => e.Institution)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.University)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.EducationField)
                    .WithMany(p => p.UserEducationEducationField)
                    .HasForeignKey(d => d.EducationFieldId)
                    .HasConstraintName("FK_UserEducation_MasterFieldValueField");

                entity.HasOne(d => d.EducationLevel)
                    .WithMany(p => p.UserEducationEducationLevel)
                    .HasForeignKey(d => d.EducationLevelId)
                    .HasConstraintName("FK_UserEducation_MasterFieldValueLevel");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserEducation)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_UserEducation_User");
            });

            modelBuilder.Entity<UserFamilyInfo>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FamilyLocation)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.NativePlace)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.FamilyAffluence)
                    .WithMany(p => p.UserFamilyInfoFamilyAffluence)
                    .HasForeignKey(d => d.FamilyAffluenceId)
                    .HasConstraintName("FK_UserFamilyInfo_MasterFieldValueAffluence");

                entity.HasOne(d => d.FamilyType)
                    .WithMany(p => p.UserFamilyInfoFamilyType)
                    .HasForeignKey(d => d.FamilyTypeId)
                    .HasConstraintName("FK_UserFamilyInfo_MasterFieldValueFamilyType");

                entity.HasOne(d => d.FamilyValues)
                    .WithMany(p => p.UserFamilyInfoFamilyValues)
                    .HasForeignKey(d => d.FamilyValuesId)
                    .HasConstraintName("FK_UserFamilyInfo_MasterFieldValueValues");

                entity.HasOne(d => d.FatherStatus)
                    .WithMany(p => p.UserFamilyInfoFatherStatus)
                    .HasForeignKey(d => d.FatherStatusId)
                    .HasConstraintName("FK_UserFamilyInfo_MasterFieldValueFather");

                entity.HasOne(d => d.MotherStatus)
                    .WithMany(p => p.UserFamilyInfoMotherStatus)
                    .HasForeignKey(d => d.MotherStatusId)
                    .HasConstraintName("FK_UserFamilyInfo_MasterFieldValueMother");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserFamilyInfo)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserFamilyInfo_User");
            });

            modelBuilder.Entity<UserLifeStyle>(entity =>
            {
                entity.Property(e => e.Hobies)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.ChildrenChoice)
                    .WithMany(p => p.UserLifeStyleChildrenChoice)
                    .HasForeignKey(d => d.ChildrenChoiceId)
                    .HasConstraintName("FK_UserLifeStyle_MasterFieldValueChildrenChoice");

                entity.HasOne(d => d.Diet)
                    .WithMany(p => p.UserLifeStyleDiet)
                    .HasForeignKey(d => d.DietId)
                    .HasConstraintName("FK_UserLifeStyle_MasterFieldValueDiet");

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

            modelBuilder.Entity<UserLocation>(entity =>
            {
                entity.Property(e => e.City)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.GrewUpIn)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Origin)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Pin).HasColumnName("PIN");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.UserLocationCountry)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_UserLocation_MasterFieldValueCountry");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.UserLocationState)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK_UserLocation_MasterFieldValueState");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserLocation)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserLocation_User");
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
