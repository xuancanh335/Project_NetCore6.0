using System;
using System.ComponentModel.DataAnnotations;

namespace Example_Project.Models.Main
{
    public class UserResponse
    {
        public string username { get; set; }
        public string fullname { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string description { get; set; }        
        public DateTime create_time { get; set; }
        public string create_by { get; set; }
        public DateTime modify_time { get; set; }
        public string modify_by { get; set; }
       
    }

    public class UserCustomModel
    {
        public string username { get; set; }
        public string fullname { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string description { get; set; }
        public string password { get; set; }       
        public DateTime? block_time { get; set; }
        public int wrong_password_number { get; set; }
        public string language { get; set; }
        public string extension_number { get; set; }
        public string asterisk_id { get; set; }
        public DateTime create_time { get; set; }
        public string create_by { get; set; }
        public DateTime modify_time { get; set; }
        public string modify_by { get; set; }
        public Guid tenant_id { get; set; }
    }
    public class UserRequest : BaseModelSQL
    {
        [Required]
        [MaxLength(50)]
        public string username { get; set; }
        [Required]
        [MaxLength(150)]
        public string fullname { get; set; }
        [Required]
        [MaxLength(50)]
        public string phone { get; set; }
        [Required]
        [MaxLength(50)]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Invalid email format !")]
        public string email { get; set; }
        [Required]
        [MaxLength(50)]
        [RegularExpression(@"^.*(?=.{8})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$", ErrorMessage = "Password must be least 8 characters and contain one uppercase letter, one lowercase letter, one digit and one special character.")]
        public string password { get; set; }
        [MaxLength(150)]
        public string description { get; set; }        
    }
}
