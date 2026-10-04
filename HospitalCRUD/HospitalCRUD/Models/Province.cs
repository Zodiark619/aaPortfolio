using System.ComponentModel.DataAnnotations;

namespace HospitalCRUD.Models
{
    public class Province
    {
        [Key]
        public string ProvinceId { get; set; } = string.Empty;
        public string ProvinceName { get; set; } = string.Empty;
    }
}
