namespace Common
{
    public class Constants
    {
        public static readonly int SERVICE_CODE = 3;

        public static readonly string CONF_CROSS_ORIGIN = "CROSS_ORIGIN";
        public static readonly string CONF_STATE_SOURCE = "STATE_SOURCE";
        public static readonly string CONF_API_SECRET_KEY = "API_SECRET_KEY";
        public static readonly string CONF_API_PUBLIC_KEY = "PUBLIC_KEY";

        public static readonly string STATE_SOURCE_DEV = "dev";
        public enum TYPE_DATA_CAMPARE { STRING, DATE, DATE_TIME, INT, FLOAT, BOOL }

        #region list colors to generate default avatar
        public static List<string> COLOR_CODES = new List<string> {
            "#EEAD0E", "#8bbf61", "#DC143C", "#CD6889", "#8B8386", "#800080", "#9932CC", "#009ACD", "#00CED1", "#03A89E",
            "#00C78C", "#00CD66", "#66CD00", "#EEB422", "#FF8C00", "#EE4000", "#388E8E", "#8E8E38", "#7171C6" };
        #endregion  
    }
}