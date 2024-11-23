namespace Myself.SmartParking.Entities
{
    public class SysUser
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public int? CompanyId { get; set; }

        public string? Password { get; set; }

        public int Status { get; set; }

        public string? Phone { get; set; }

        public string? Mobile { get; set; }

        public string? Address { get; set; }

        public string? Email { get; set; }

        public string Qq { get; set; }

        public string? WeChat { get; set; }

        public int? Gender { get; set; }

        public DateTime? LastLoginTime { get; set; }

        public DateTime? CreateTime { get; set; }

        public int? CreateId { get; set; }

        public DateTime? LastModifyTime { get; set; }

        public int? LastModifyId { get; set; }

    }
}
