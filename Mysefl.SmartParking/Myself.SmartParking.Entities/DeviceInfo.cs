using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.SmartParking.Entities
{
    [Table("device_info")]
    public class DeviceInfo
    {
        [Key]
        [Column("device_id")]
        public int DeviceId { get; set; }
        [Column("device_name")]
        public string DeviceName { get; set; }
        [Column("addr_ip")]
        public string? AddrIp { get; set; }
        [Column("addr_port")]
        public int? AddrPort { get; set; }
        [Column("addr_com")]
        public string? AddrCom { get; set; }
        [Column("username")]
        public string? UserName { get; set; }
        [Column("password")]
        public string? Password { get; set; }
        [Column("addr_url1")]
        public string? CallbackUrl1 { get; set; }
        [Column("addr_url2")]
        public string? CallbackUrl2 { get; set; }
        [Column("addr_url3")]
        public string? CallbackUrl3 { get; set; }
    }
}
