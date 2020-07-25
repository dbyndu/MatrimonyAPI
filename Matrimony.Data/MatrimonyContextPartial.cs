using Matrimony.Data.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Data
{
    public partial class MatrimonyContext
    {
        //public virtual DbSet<ProfileDisplayData> ProfileDisplayData { get; set; }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            //additional config
            modelBuilder.Entity<ProfileDisplayData>().HasNoKey();
            modelBuilder.Entity<NotificationData>().HasNoKey();
            modelBuilder.Entity<UserMatchPercent>().HasNoKey();
        }
        public ProfileDisplayData GetProfileDisplayDataAsync(int id)
        {
            // Initialization.  
           ProfileDisplayData data = new ProfileDisplayData();

            try
            {
                // Processing.  
                object[] sqlParams = {
                    new SqlParameter("@userId", id),
                };
                string sqlQuery = "dbo.GetAllCounts @userId = {0}";

                data = this.Set<ProfileDisplayData>().FromSqlRaw(sqlQuery, id).AsEnumerable().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // Info.  
            return data;
        }
        public async Task<List<NotificationData>> GetNotificationDataAsync(int id)
        {
            // Initialization.  
            List<NotificationData> list = new List<NotificationData>();

            try
            {
                // Processing.  
                string sqlQuery = "dbo.GetAllNotifications @userId = {0}, @IsAll = 1";

                list = await this.Set<NotificationData>().FromSqlRaw(sqlQuery, id).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // Info.  
            return list;
        }

        public UserMatchPercent GetMatchPercent(int SenderId, int ReceiverId)
        {
            // Initialization.  
            UserMatchPercent data = new UserMatchPercent();

            try
            {
                // Processing.  
                var senderParam = new SqlParameter("@senderUserId", SenderId);
                var receiverParam = new SqlParameter("@receiverUserId", ReceiverId);

                string sqlQuery = "dbo.GetProfileQuotient @senderUserId = {0}, @receiverUserId = {1}";

                data = this.Set<UserMatchPercent>().FromSqlRaw(sqlQuery, SenderId, ReceiverId).AsEnumerable().FirstOrDefault();
            }
            catch (Exception ex)
            {
                data = new UserMatchPercent();
            }

            // Info.  
            return data;
        }

    }
}

