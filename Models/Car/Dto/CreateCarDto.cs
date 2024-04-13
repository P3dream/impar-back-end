// CreateCarDto.cs
using System.ComponentModel.DataAnnotations;

namespace impar_back_end.Models.Car.DTOs
{
    public class CreateCarDto
    {
        [Required(ErrorMessage = "O ID da foto é obrigatório.")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "The PhotoId must be an integer.")]
        public int PhotoId { get; set; }

        [Required(ErrorMessage = "O nome do carro é obrigatório.")]
        [StringLength(25, ErrorMessage = "O nome do carro deve ter no máximo 25 caracteres.")]

        public string Name { get; set; }

        [Required(ErrorMessage = "O status do carro é obrigatório.")]
        [StringLength(30, ErrorMessage = "O nome do carro deve ter no máximo 50 caracteres.")]
        public string Status { get; set; }
    }
}