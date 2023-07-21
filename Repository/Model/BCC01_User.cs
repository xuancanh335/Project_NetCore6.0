using System;
using System.Collections.Generic;

namespace Repository.EF
{
    public partial class BCC01_User
    {
        public Guid id { get; set; }
        public string username { get; set; } = null!;
        public string? fullname { get; set; }
        public string phone { get; set; } = null!;
        public string password { get; set; } = null!;
        public string email { get; set; } = null!;
        public string? description { get; set; }
        public DateTime create_time { get; set; }
        public string create_by { get; set; } = null!;
        public DateTime modify_time { get; set; }
        public string modify_by { get; set; } = null!;
    }
}
