using System;
using System.Collections.Generic;
using System.Text;
using Matrimony.Service.Contracts;
using Matrimony.Model.Base;
using Matrimony.Data;
using System.Linq;
using Matrimony_Model.Common;
using Matrimony.Model.User;

namespace Matrimony.Service.Common
{
    public class MasterDataService : IMasterDataService
    {
        private MatrimonyContext _context;
        public MasterDataService(MatrimonyContext context)
        {
            _context = context;
        }
        public Response GetMasterDate()
        {
            var errors = new List<Error>();
            IQueryable<MasterDataModel> IQueryData = null;
            List<MasterDataModel> lstMasterData = new List<MasterDataModel>();
            try
            {
                if (!errors.Any())
                {
                    //IQueryData = _context.MasterFieldValue
                    //    .Join(_context.MasterTableMetadata, v => v.MasterTableId, m => m.
                    //    (v, m) => new MasterDataModel
                    //    {
                            
                    //    });
                    //    Select(u => new UserModel { UserID = u.Id.ToString() });
                    IQueryData = (from v in _context.MasterFieldValue
                                  join m in _context.MasterTableMetadata on v.MasterTableId equals m.Id
                                  //where tableNames.Contains(m.TableName)
                                  select new MasterDataModel
                                  {
                                      Id = v.Id,
                                      Name = v.Value,
                                      MasterTableId = m.Id,
                                      TableName= m.TableName,
                                      DependentFieldId = v.DependentFieldId
                                  }
                                  ) ;
                    lstMasterData = IQueryData.ToList();
                }
            }
            catch (Exception ex)
            {
                errors.Add(new Error("Err101", ex.Message));
            }
            if (lstMasterData == null || Convert.ToInt32(lstMasterData.Count) == 0)
            {
                errors.Add(new Error("Err102", "No data found. Verify user entitlements."));
            }
            var metadata = new Metadata(!errors.Any(), Guid.NewGuid().ToString(), "Response Contains List of Master table data.");
            if (errors.Any())
            {
                return new ErrorResponse(metadata, errors);
            }
            return new MasterDataModelListResponse(metadata, lstMasterData);
        }
        public Response GetCountry()
        {
            var errors = new List<Error>();
            try
            {
                var listCountry = _context.Countries.Select(c => new
                {
                    Name = c.Name,
                    Id = c.Id
                }).ToList();

                var metadata = new Metadata(!errors.Any(), Guid.NewGuid().ToString(), "Response Contains List of Country.");
                if (errors.Any())
                {
                    return new ErrorResponse(metadata, errors);
                }
                return new AnonymousResponse(metadata, listCountry);
            }
            catch (Exception ex)
            {
                errors.Add(new Error("Err101", ex.Message));
                var metadata = new Metadata(!errors.Any(), Guid.NewGuid().ToString(), "Response Contains List of Country.");
                return new ErrorResponse(metadata, errors);
            }
        }
        public Response GetState(int countryId)
        {
            var errors = new List<Error>();
            try
            {
                var listState = _context.States.Where(s=> s.CountryId.Equals(countryId)).Select(c => new
                {
                    Name = c.Name,
                    Id = c.Id
                }).ToList();

                var metadata = new Metadata(!errors.Any(), Guid.NewGuid().ToString(), "Response Contains List of State.");
                if (errors.Any())
                {
                    return new ErrorResponse(metadata, errors);
                }
                return new AnonymousResponse(metadata, listState);
            }
            catch (Exception ex)
            {
                errors.Add(new Error("Err101", ex.Message));
                var metadata = new Metadata(!errors.Any(), Guid.NewGuid().ToString(), "Response Contains List of State.");
                return new ErrorResponse(metadata, errors);
            }
        }
        public Response GetCity(string stateIds)
        {
            var errors = new List<Error>();
            try
            {
                string[] stateArray = stateIds.Split(',');
                var listState = _context.Cities.Where(s => stateArray.Contains(s.StateId.ToString())).Select(c => new
                {
                    Name = c.Name,
                    Id = c.Id
                }).ToList();

                var metadata = new Metadata(!errors.Any(), Guid.NewGuid().ToString(), "Response Contains List of City.");
                if (errors.Any())
                {
                    return new ErrorResponse(metadata, errors);
                }
                return new AnonymousResponse(metadata, listState);
            }
            catch (Exception ex)
            {
                errors.Add(new Error("Err101", ex.Message));
                var metadata = new Metadata(!errors.Any(), Guid.NewGuid().ToString(), "Response Contains List of City.");
                return new ErrorResponse(metadata, errors);
            }
        }
    }
}
