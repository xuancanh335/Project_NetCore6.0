using System;

namespace Example_Project.Models.Main
{
    public class BaseModelSQL
    {
        public Guid id { get; set; }
        public Guid tenant_id { get; set; }
        public string create_by { get; set; }
        public string modify_by { get; set; }
        public DateTime create_time { get; set; }
        public DateTime modify_time { get; set; }

        public void AddInfo()
        {
            DateTime currentDateTime = DateTime.Now;
            id = Guid.NewGuid();
            tenant_id = Guid.Empty;//SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);
            create_by = "";//SessionStore.Get<string>(Constants.KEY_SESSION_USER_ID);
            modify_by = "";
            create_time = currentDateTime;
            modify_time = currentDateTime;
        }
        public void UpdateInfo()
        {
            DateTime currentDateTime = DateTime.Now;
            modify_by = ""; //SessionStore.Get<string>(Constants.KEY_SESSION_USER_ID);
            modify_time = currentDateTime;
            tenant_id = Guid.Empty;// SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);
        }
    }
}
