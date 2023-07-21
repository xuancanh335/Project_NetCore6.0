using System.Runtime.Serialization;

namespace Common.Params.Base
{
    public class SortParam
    {
        public string name_field { get; set; }
        private string typeSort { get; set; }

        public string type_sort
        {
            get
            {
                return typeSort;
            }
            set
            {
                typeSort = value;
                isAccessding = value == "ASC" ? true : false;
            }
        }
        [IgnoreDataMember]
        public bool isAccessding { get; set; }
    }
}
