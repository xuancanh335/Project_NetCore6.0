using Common.Commons;
using Common.Params.Base;
using Repository.EF;
using Repository.CustomModel;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Repository.Queries
{
    public class Query<T>
    {
        public string baseQueryString { get; set; }
        public string conditions { get; set; }
        private string sort { get; set; }
        private string include { get; set; }
        private DbContextSql dbContext { get; set; }
        public int page { get; set; } = 0;
        public int limit { get; set; } = 0;

        public IStoreProcedureExecute _storeProcedure;
        public IStoreProcedureExecute StoreProcedure
        {
            get
            {
                return _storeProcedure ?? (_storeProcedure = new StoreProcedureExecute());
            }
        }
        public Query()
        {
            baseQueryString = $"SELECT * FROM [{"dbo"}].[{typeof(T).Name}] ";
        }

        public Query(DbContextSql dbContext)
        {
            this.dbContext = dbContext;
        }

        public Query(PagingParam param, DbContextSql dbContext)
        {
            this.dbContext = dbContext;
            baseQueryString = $"SELECT * FROM [{"dbo"}].[{typeof(T).Name}] ";
            this.page = param.page;
            this.limit = param.limit;

            // Search by tenant_id . If flag = false then does not add tenant_id to conditions 
            if (!String.IsNullOrEmpty(param.tenant_id.ToString()) && param.flag)
                this.Where(new SearchParam() { name_field = "tenant_id", value_search = param.tenant_id.ToString() });

            // Search accurate
            if (param.field_get_list != null && param.field_get_list.Count > 0)
                foreach (SearchParam searchParam in param.field_get_list)
                    this.Where(searchParam);

            // Search like
            if (param.search_list != null && param.search_list.Count > 0)
                foreach (SearchParam searchParam in param.search_list)
                    this.WhereLike(searchParam);

            // Search table
            if (param.table_search_list != null && param.table_search_list.Length > 0)
                foreach (string searchParam in param.table_search_list)
                    this.WhereTable(searchParam);

            // Sort
            if (param.sorts != null && param.sorts.Count > 0)
                foreach (SortParam sort in param.sorts)
                    this.OrderBy(sort);
        }

        // Search accurate
        public Query<T> Where(SearchParam param)
        {
            //// Check if name_field is valid
            //PropertyInfo property = typeof(T).GetProperty(param.name_field);
            //if (property == null)
            //    throw new KeyNotFoundException(param.name_field + " not found");

            if (String.IsNullOrEmpty(param.value_search.ToString()))
                return this;

            // Replace ' (single quote) in search value by '' (2 single quotes))
            param.value_search = param.value_search.ToString().Replace("'", "''");
            if (param.upper_bound != null)
                param.upper_bound = param.upper_bound.ToString().Replace("'", "''");

            // Add conjunction to query string
            if (String.IsNullOrEmpty(conditions))
                conditions = "WHERE ";
            else
                conditions += $"{param.conjunction} ";
            // Handle when no upper_bound is passed in
            if (param.upper_bound == null)
                conditions += $"{param.name_field} = '{param.value_search}' ";
            else
                conditions += $"({param.name_field} > '{param.value_search}' AND {param.name_field} < '{param.upper_bound})' ";

            return this;
        }

        // Search accurate
        public Query<T> WhereLike(SearchParam param)
        {
            // Check if name_field is valid
            string name_field_process = param.name_field.Contains(".") ? param.name_field.Split('.')[1] : param.name_field;
            PropertyInfo property = typeof(T).GetProperty(name_field_process);
            if (property == null)
                throw new KeyNotFoundException(param.name_field + " not found");
            if (String.IsNullOrEmpty(param.value_search.ToString()))
                return this;
            // Check if necessary to convert
            string valueSearch = param.value_search.ToString();
            bool isUnicode = CommonFuncMain.IsUnicode(valueSearch);

            // Replace ' (single quote) in valueSearch by '' (2 single quotes))
            valueSearch = valueSearch.Replace("'", "''");

            // Replace % (percentage sign) in valueSearch by [%]
            valueSearch = valueSearch.Replace("%", "[%]");

            // Replace _ (underscore) in valueSearch by [_]
            valueSearch = valueSearch.Replace("_", "[_]");

            // Add conjunction to query string
            if (String.IsNullOrEmpty(conditions))
                conditions = "WHERE ";
            else
                conditions += $"{param.conjunction} ";


            // Handle convert or not
            // if search value is unicode (accented) then search accurate to input string
            if (isUnicode)
                conditions += $"CONVERT(VARCHAR(max), {param.name_field}, 103) LIKE '%{valueSearch}%' ";
            // else convert datetime then search
            else if (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?))
                conditions += $"[dbo].[ufn_convertDatetime]({param.name_field}) LIKE '%{valueSearch}%' ";
            // else convert value from db then search unaccented
            else
                conditions += $"CONVERT(VARCHAR(max), dbo.ufn_removeMark({param.name_field}), 103) LIKE '%{valueSearch}%' ";

            return this;
        }

        // Search table
        public Query<T> WhereTable(string param)
        {
            // Get all properties that is not virtual (not mapped to other tables)
            PropertyInfo[] properties = typeof(T).GetProperties().Where(m => m.GetAccessors()[0].IsFinal ||
                                                                        !m.GetAccessors()[0].IsVirtual).ToArray();

            // Check if necessary to convert
            bool isUnicode = CommonFuncMain.IsUnicode(param);

            // Replace ' (single quote) in valueSearch by '' (2 single quotes))
            param = param.Replace("'", "''");

            // Replace % (percentage sign) in valueSearch by [%]
            param = param.Replace("%", "[%]");

            // Replace _ (underscore) in valueSearch by [_]
            param = param.Replace("_", "[_]");

            if (properties.Length > 0)
            {
                // Add conjunction to query string
                if (String.IsNullOrEmpty(conditions))
                    conditions = "WHERE (";
                else
                    conditions += $"AND (";

                foreach (PropertyInfo property in properties)
                {
                    // if search value is unicode (accented) then search accurate to input string
                    if (isUnicode)
                        conditions += $"CONVERT(VARCHAR(max), {property.Name}, 103) LIKE '%{param}%' OR ";
                    // else convert datetime then search
                    else if (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?))
                        conditions += $"[dbo].[ufn_convertDatetime]({property.Name}) LIKE '%{param}%' OR ";
                    // else convert value from db then search unaccented
                    else
                        conditions += $"CONVERT(VARCHAR(max), dbo.ufn_removeMark({property.Name}), 103) LIKE '%{param}%' OR ";
                }

                // Remove last " OR " and add closing parenthesis
                conditions = conditions.Remove(conditions.Length - 4);
                conditions += ") ";
            }

            return this;
        }

        // Handle Include
        public Query<T> Include()
        {
            // Get all properties that is virtual (mapped to other tables)
            PropertyInfo[] properties = typeof(T).GetProperties().Where(m => !m.GetAccessors()[0].IsFinal &&
                                                                        m.GetAccessors()[0].IsVirtual).ToArray();

            return this;
        }

        // Handle sort
        public Query<T> OrderBy(SortParam param)
        {
            // Check if name_field is valid
            PropertyInfo property = typeof(T).GetProperty(param.name_field);
            if (property == null)
                throw new KeyNotFoundException(param.name_field + " not found");

            if (String.IsNullOrEmpty(sort))
                sort = "ORDER BY ";
            else
                sort += ", ";

            sort += $"{param.name_field} {param.type_sort} ";

            return this;
        }

        // Return query string
        public override string ToString()
        {
            // Check if create_time is a valid field
            PropertyInfo property = typeof(T).GetProperty("modify_time");

            // Add sorting by create_time if type T has create_time property and
            // sort doesn't contain create_time
            string sortToString = sort;
            if (property != null)
            {
                if (String.IsNullOrEmpty(sortToString))
                    sortToString = "ORDER BY modify_time DESC ";
                else if (!sortToString.Contains("create_time"))
                    sortToString += ", modify_time DESC ";
            }

            // Add paging
            if (page > 0 && limit > 0)
            {
                if (String.IsNullOrEmpty(sortToString))
                    sortToString = $"ORDER BY CURRENT_TIMESTAMP OFFSET {(page - 1) * limit} ROWS FETCH NEXT {limit} ROWS ONLY ";
                else
                    sortToString += $"OFFSET {(page - 1) * limit} ROWS FETCH NEXT {limit} ROWS ONLY ";
            }

            return baseQueryString + conditions + include + sortToString;
        }

        // Return list from query string
        public async Task<List<T>> ToListAsync()
        {
            string queryString = ToString();
            var query = StoreProcedure.ExecuteReturnListBySql<T>(queryString).Result.ToList();

            return await Task.FromResult(query);
        }

        // Return count from query string
        public async Task<int> CountAsync()
        {
            string queryString = baseQueryString + conditions;
            return await Task.FromResult(StoreProcedure.ExecuteReturnListBySql<T>(queryString).Result.Count());
        }

        public async Task<ListResult<T>> SearchJoin(PagingParam param, string sqlString)
        {
            ListResult<T> listResult = new ListResult<T>();
            this.baseQueryString = sqlString;
            this.page = param.page;
            this.limit = param.limit;
            // Search by tenant_id

            if (!String.IsNullOrEmpty(param.tenant_id.ToString()) && param.flag)
                this.Where(new SearchParam() { name_field = "[b].tenant_id", value_search = param.tenant_id.ToString() });
            // Search accurate
            if (param.field_get_list != null && param.field_get_list.Count > 0)
                foreach (SearchParam searchParam in param.field_get_list)
                    this.Where(searchParam);
            // Search like
            if (param.search_list != null && param.search_list.Count > 0)
                foreach (SearchParam searchParam in param.search_list)
                    this.WhereLike(searchParam);
            // Search table
            if (param.table_search_list != null && param.table_search_list.Length > 0)
                foreach (string searchParam in param.table_search_list)
                    this.WhereTable(searchParam);
            // Sort
            if (param.sorts != null && param.sorts.Count > 0)
                foreach (SortParam sort in param.sorts)
                    this.OrderBy(sort);
            listResult.items = await ToListAsync();
            listResult.total = await CountAsync();
            return listResult;
        }
    }
}
