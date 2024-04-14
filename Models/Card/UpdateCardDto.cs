using System.ComponentModel.DataAnnotations;

namespace impar_back_end.Models.Card
{
    public class UpdateCardDto : CreateCardDto
    {
        [Required(ErrorMessage = "Photo id is mandatory.")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "The Id must be an integer.")]
        public int PhotoId { get; set; }

        [Required(ErrorMessage = "Photo id is mandatory.")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "The Id must be an integer.")]
        public int CarId { get; set; }
    }
}
