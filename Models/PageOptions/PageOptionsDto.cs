using System.ComponentModel.DataAnnotations;

namespace impar_back_end.Models.PageOptions
{
    public class PageOptionsDto
    {
        [Required(ErrorMessage = "The number of pages is mandatory.")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "The number of pages must be an integer.")]
        public int Page { get; set; } = 1;

        [Required(ErrorMessage = "The page size is mandatory.")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "The page size must be an integer.")]
        [Range(1, int.MaxValue, ErrorMessage = "The page size must be greater than zero.")]
        public int PageSize { get; set; } = 9;
    }
}
