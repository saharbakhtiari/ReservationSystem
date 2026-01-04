using Domain.AdvanceSearchs;
using Domain.Common;
using Domain.UnitOfWork.Uow;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Extentions
{

    public static class SprocRepositoryExtensions
    {
        
        public static DbCommand LoadStoredProcedure(this DbContext context, string storedProcName)
        {
            var cmd = context.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = storedProcName;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            return cmd;
        }
        public static DbCommand WithSqlParams(this DbCommand cmd, params (string, object)[] nameValueParamPairs)
        {
            foreach (var pair in nameValueParamPairs)
            {
                var param = cmd.CreateParameter();
                param.ParameterName = pair.Item1;
                param.Value = pair.Item2 ?? DBNull.Value;
                cmd.Parameters.Add(param);
            }

            return cmd;
        }

        public static IList<T> ExecuteStoredProcedure<T>(this DbCommand command) where T : class
        {
            using (command)
            {
                if (command.Connection.State == System.Data.ConnectionState.Closed)
                    command.Connection.Open();
                try
                {
                    using (var reader = command.ExecuteReader())
                    {
                        return reader.MapToList<T>();
                    }
                }
                finally
                {
                    command.Connection.Close();
                }
            }
        }

        public static async Task<IList<T>> ExecuteStoredProcedureAsync<T>(this DbCommand command) where T : class
        {
            using (command)
            {
                if (command.Connection.State == System.Data.ConnectionState.Closed)
                    await command.Connection.OpenAsync();
                try
                {
                    using (var reader = command.ExecuteReader())
                    {
                        return reader.MapToList<T>();
                    }
                }
                finally
                {
                    command.Connection.Close();
                }
            }
        }
        public static async Task<IQueryable<T>> ExecuteStoredProcedureAsync<T>(this DbCommand command, CancellationToken cancellationToken) where T : class
        {
            using (command)
            {
                if (command.Connection.State == System.Data.ConnectionState.Closed)
                    await command.Connection.OpenAsync();
                try
                {
                    using (var reader = await command.ExecuteReaderAsync(cancellationToken))
                    {
                        return reader.MapToIQueryable<T>();
                    }
                }
                finally
                {
                    command.Connection.Close();
                }
            }
        }
        public static async Task<PagedList<T>> ExecuteStoredProcedureAsync<T>(this DbCommand command,int pageNumber,int pageSize, CancellationToken cancellationToken) where T : class
        {
            using (command)
            {
                if (command.Connection.State == System.Data.ConnectionState.Closed)
                    await command.Connection.OpenAsync();
                try
                {
                    using (var reader = await command.ExecuteReaderAsync(cancellationToken))
                    {
                        return  reader.MapToPagedList<T>(pageNumber,pageSize);
                    }
                }
                finally
                {
                    command.Connection.Close();
                }
            }
        }

        public static async Task<PagedList<FullTextResultDto>> ExecuteSearchProcedureAsync(this DbCommand command, int pageNumber, int pageSize, CancellationToken cancellationToken) 
        {
            using (command)
            {
                if (command.Connection.State == System.Data.ConnectionState.Closed)
                    await command.Connection.OpenAsync();
                try
                {
                    using (var reader = await command.ExecuteReaderAsync(cancellationToken))
                    {
                        return reader.MapToPagedSearchResultList(pageNumber, pageSize);
                    }
                }
                finally
                {
                    command.Connection.Close();
                }
            }
        }
        public static async Task<List<FullTextResultDto>> ExecuteSearchProcedureAsync(this DbCommand command, CancellationToken cancellationToken)
        {
            using (command)
            {
                if (command.Connection.State == System.Data.ConnectionState.Closed)
                    await command.Connection.OpenAsync();
                try
                {
                    using (var reader = await command.ExecuteReaderAsync(cancellationToken))
                    {
                        return reader.MapToSearchResultList();
                    }
                }
                finally
                {
                    command.Connection.Close();
                }
            }
        }
        private static IQueryable<T> MapToIQueryable<T>(this DbDataReader dr)
        {
            var objList = new List<T>();
            var props = typeof(T).GetRuntimeProperties();

            var colMapping = dr.GetColumnSchema()
                .Where(x => props.Any(y => y.Name.ToLower() == x.ColumnName.ToLower()))
                .ToDictionary(key => key.ColumnName.ToLower());
                     
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    T obj = Activator.CreateInstance<T>();
                    foreach (var prop in props)
                    {
                        var val = dr.GetValue(colMapping[prop.Name.ToLower()].ColumnOrdinal.Value);
                        prop.SetValue(obj, val == DBNull.Value ? null : val);
                    }
                    objList.Add(obj);
                }
            }
            return objList.AsQueryable();
        }
        private static PagedList<FullTextResultDto> MapToPagedSearchResultList(this DbDataReader dr,int pageNumber,int pageSize) 
        {
            var objList = new List<FullTextResultDto>();
           
            Dictionary<int,  List<FullTextResultDto> > allResult = new Dictionary<int, List<FullTextResultDto> > ();

            var props = typeof(FullTextResultDto).GetRuntimeProperties();

            var colMapping = dr.GetColumnSchema()
                .Where(x => props.Any(y => y.Name.ToLower() == x.ColumnName.ToLower()))
                .ToDictionary(key => key.ColumnName.ToLower());
          

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    FullTextResultDto obj = Activator.CreateInstance<FullTextResultDto>();
                    foreach (var prop in props)
                    {
                        var val = dr.GetValue(colMapping[prop.Name.ToLower()].ColumnOrdinal.Value);
                        prop.SetValue(obj, val == DBNull.Value ? null : val);
                    }
                    objList.Add(obj);
                }
            }
           
        
          
            var page = new List<FullTextResultDto>();
            var uniqueRules = objList.DistinctBy(x => x.RuleId).ToList();
           // objList= objList.OrderByDescending(x=>x.RuleId).ToList();
            for (int i =0; i < uniqueRules.Count(); i++)
            {
                page.AddRange(objList.Where(x => x.RuleId == uniqueRules[i].RuleId));
               
                if (page.DistinctBy(x => x.RuleId).Count() == pageSize )
                {
                    allResult.Add(allResult.Count, page);
                   
                    page=new List<FullTextResultDto>();
                    
                }
            }
            if (page.DistinctBy(x => x.RuleId).Count() >0 )
            {
                allResult.Add(allResult.Count, page);
            }
            /*
            var items = objList.DistinctBy(x => x.RuleId)
                 .Skip((pageNumber - 1) * pageSize)
                 .Take(pageSize)
                 .ToList();
              */
            var items =
                allResult.ContainsKey(pageNumber) ? allResult[pageNumber] : allResult.Count>0? allResult[0]: new  List<FullTextResultDto>();

            return new PagedList<FullTextResultDto>(items, uniqueRules.Count(), pageNumber, pageSize);
            
        }

        private static List<FullTextResultDto> MapToSearchResultList(this DbDataReader dr)
        {
            var objList = new List<FullTextResultDto>();

            Dictionary<int, List<FullTextResultDto>> allResult = new Dictionary<int, List<FullTextResultDto>>();

            var props = typeof(FullTextResultDto).GetRuntimeProperties();

            var colMapping = dr.GetColumnSchema()
                .Where(x => props.Any(y => y.Name.ToLower() == x.ColumnName.ToLower()))
                .ToDictionary(key => key.ColumnName.ToLower());


            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    FullTextResultDto obj = Activator.CreateInstance<FullTextResultDto>();
                    foreach (var prop in props)
                    {
                        var val = dr.GetValue(colMapping[prop.Name.ToLower()].ColumnOrdinal.Value);
                        prop.SetValue(obj, val == DBNull.Value ? null : val);
                    }
                    objList.Add(obj);
                }
            }

            return objList;

        }
        private static PagedList<T> MapToPagedList<T>(this DbDataReader dr, int pageNumber, int pageSize) 
        {
            var objList = new List<T>();
            var props = typeof(T).GetRuntimeProperties();

            var colMapping = dr.GetColumnSchema()
                .Where(x => props.Any(y => y.Name.ToLower() == x.ColumnName.ToLower()))
                .ToDictionary(key => key.ColumnName.ToLower());


            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    T obj = Activator.CreateInstance<T>();
                    foreach (var prop in props)
                    {
                        var val = dr.GetValue(colMapping[prop.Name.ToLower()].ColumnOrdinal.Value);
                        prop.SetValue(obj, val == DBNull.Value ? null : val);
                    }
                    objList.Add(obj);
                }
            }

            var items = objList
               .Skip((pageNumber - 1) * pageSize)
               .Take(pageSize)
               .ToList();

            return new PagedList<T>(items, objList.Count, pageNumber, pageSize);

        }

        private static IList<T> MapToList<T>(this DbDataReader dr)
        {
            var objList = new List<T>();
            var props = typeof(T).GetRuntimeProperties();

            var colMapping = dr.GetColumnSchema()
                .Where(x => props.Any(y => y.Name.ToLower() == x.ColumnName.ToLower()))
                .ToDictionary(key => key.ColumnName.ToLower());

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    T obj = Activator.CreateInstance<T>();
                    foreach (var prop in props)
                    {
                        var val = dr.GetValue(colMapping[prop.Name.ToLower()].ColumnOrdinal.Value);
                        prop.SetValue(obj, val == DBNull.Value ? null : val);
                    }
                    objList.Add(obj);
                }
            }
            return objList;
        }
    }
}
