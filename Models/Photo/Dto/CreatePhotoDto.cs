using System.ComponentModel.DataAnnotations;

namespace impar_back_end.Models.Photo.Dto
{
    public class CreatePhotoDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Base 64 is required")]
        public string Base64 { get; set; }
    }
}
