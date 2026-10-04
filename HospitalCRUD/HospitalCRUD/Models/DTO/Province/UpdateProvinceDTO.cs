using System.ComponentModel.DataAnnotations;

namespace HospitalCRUD.Models.DTO.Province
{
    public class UpdateProvinceDTO
    {
        //[Required]
        //[StringLength(2, MinimumLength = 2)]
        //public string ProvinceId { get; set; } = string.Empty;

        [Required]
        public string ProvinceName { get; set; } = string.Empty;

    }
}
