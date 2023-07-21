using System.Collections.Generic;

namespace Common.Params.Base
{
    public class PagingParam : BaseParam
    {
        public int page { get; set; }
        public int limit { get; set; }
        public List<SortParam> sorts { get; set; }
        public List<SearchParam> search_list { get; set; }
        public List<SearchParam> field_get_list { get; set; }
        public string[] table_search_list { get; set; }

        public PagingParam()
        {
            this.sorts = new List<SortParam>();
            this.search_list = new List<SearchParam>();
            this.field_get_list = new List<SearchParam>();
        }
    }
}
