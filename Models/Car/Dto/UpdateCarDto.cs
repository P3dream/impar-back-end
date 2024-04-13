using impar_back_end.Models.Car.DTOs;
using System.ComponentModel.DataAnnotations;

namespace impar_back_end.Models.Car.Dto
{
    public class UpdateCarDto : CreateCarDto
    {
        [Required(ErrorMessage = "O ID do carro é obrigatório.")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "The Id must be an integer.")]
        public int Id { get; set; }
    }
}
