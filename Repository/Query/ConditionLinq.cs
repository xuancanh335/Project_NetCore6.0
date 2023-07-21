using Common.Commons;
using Common.Params.Base;
using Repository.CustomModel;

namespace Repository.Queries
{
    public class ConditionLinq<T>
    {
        public List<T> WhereLike(List<T> list, List<SearchParam> listparam)
        {
            List<T> result = new List<T>();
            int index = 0;
            foreach (SearchParam param in listparam)
            {
                var property = typeof(T).GetProperty(param.name_field);
                foreach (var item in list)
                {
                    if (CommonFuncMain.IsUnicode(param.value_search.ToString()))
                    {
                        var a = item.GetValueObject(param.name_field).ToString();
                        if (a != null)
                        {
                            if (index == 0)
                            {
                                if (a.ToLower().Contains(param.value_search.ToString().ToLower()))
                                    result.Add(item);
                            }
                            else
                            {
                                if (!a.ToLower().Contains(param.value_search.ToString().ToLower()))
                                    result.Remove(item);
                            }
                        }
                    }
                    else
                    {
                        var a = "";
                        if (property.PropertyType.Equals(typeof(DateTime)) || property.PropertyType == typeof(DateTime?))
                        {
                            a = DateTime.Parse(item.GetValueObject(param.name_field).ToString()).ToString("dd/MM/yyyy HH:mm:ss");
                        }
                        else
                        {
                            a = CommonFuncMain.utf8Convert3(item.GetValueObject(param.name_field).ToString());
                        }
                        if (a != null)
                        {
                            if (index == 0)
                            {
                                if (a.ToLower().Contains(param.value_search.ToString().ToLower()))
                                    result.Add(item);
                            }
                            else
                            {
                                if (!a.ToLower().Contains(param.value_search.ToString().ToLower()))
                                    result.Remove(item);
                            }
                        }
                    }
                }
                index++;
            }
            return result;
        }
        public List<T> OrderBy(List<T> list, List<SortParam> listparam)
        {
            var orderstring = "";
            foreach (SortParam param in listparam)
            {
                if (string.IsNullOrEmpty(orderstring))
                    orderstring = param.name_field + " " + param.type_sort;
                else
                    orderstring = orderstring + "," + param.name_field + " " + param.type_sort;
            }
            list = list.AsQueryable<T>().OrderBy(orderstring).ToList();

            return list;
        }
        public ListResult<T> Process(PagingParam param, List<T> list)
        {

            ListResult<T> result = new ListResult<T>();
            // Search like
            if (param.search_list != null && param.search_list.Count > 0)
                list = WhereLike(list, param.search_list);
            // Sort
            if (param.sorts != null && param.sorts.Count > 0)
                list = OrderBy(list, param.sorts);

            // Add paging
            if (param.page > 0 && param.limit > 0)
            {
                result.total = list.Count;
                list = list.Skip(((param.page - 1) * param.limit)).Take(param.limit).ToList();
            }
            else { result.total = list.Count; }
            result.items = list;

            return result;
        }
    }
}
