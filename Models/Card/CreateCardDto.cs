using System.ComponentModel.DataAnnotations;

namespace impar_back_end.Models.Card
{
    public class CreateCardDto
    {
        [Required(ErrorMessage = "Car name is mandatory.")]
        public string CarName { get; set; }
        public IFormFile Image { get; set; }
        [Required(ErrorMessage = "Status is mandatory.")]
        public string Status { get; set; }
    }
}
