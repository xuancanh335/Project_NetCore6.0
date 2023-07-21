using System.Collections.Generic;

namespace Repository.CustomModel
{
    public class ListResult<T>
    {
        public List<T> items { get; set; }
        public long total { get; set; }

        public ListResult()
        {
            items = null;
            total = 0;
        }
        public ListResult(List<T> items, long totalItems)
        {
            this.items = items;
            this.total = totalItems;
        }
    }

    public class ListResultMonitoring<T>
    {
        public List<T> items { get; set; }
        public long total { get; set; }
        public int totalReady { get; set; }

        public ListResultMonitoring()
        {
            items = null;
            total = 0;
            totalReady = 0;
        }
        public ListResultMonitoring(List<T> items, long totalItems,int totalReady)
        {
            this.items = items;
            this.total = totalItems;
            this.totalReady = totalReady;
        }
    }
}
