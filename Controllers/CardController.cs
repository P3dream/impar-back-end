namespace impar_back_end.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using global::impar_back_end.Models.Card;
    using global::impar_back_end.Services;
    using global::impar_back_end.Models.PageOptions;
    using Microsoft.AspNetCore.Authorization;

    namespace impar_back_end.Controllers
    {
        [Route("api/Card")]
        [ApiController]
        public class CardController : ControllerBase
        {
            private readonly CardService _cardService;
 

            public CardController(CardService cardService)
            {
                _cardService = cardService;
            }


            [Authorize]
            [HttpPost]
            public async Task<IActionResult> CreateCard([FromForm]CreateCardDto createCardDto)
            {
                try
                {
                    var success = await _cardService.CreateCarFromPhotoAndCar(createCardDto);
                    if (success)
                        return Ok("Card created successfully.");
                    else
                        return BadRequest("Failed to create card.");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }

            [Authorize]
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteCard(int id)
            {
                try
                {
                    var result = await _cardService.DeleteCard(id);
                    if (result)
                    {
                        return Ok("Card deleted successfully.");
                    }
                    else
                    {
                        return BadRequest("Failed to delete card.");
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"An error occurred: {ex.Message}");
                }
            }

            [Authorize]
            [HttpGet]
            public async Task<IActionResult> GetCards([FromQuery] PageOptionsDto pageOptionsDto,[FromQuery]String? searchString)
            {
                try
                {
                    var cards = await _cardService.GetCards(pageOptionsDto,searchString);
                    return Ok(cards);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }
            [Authorize]
            [HttpPut("{CarId}")]
            public async Task<IActionResult> UpdateCard(int CarId, [FromForm] UpdateCardDto updateCardDto)
            {
                try
                {                    
                    var result = await _cardService.UpdateCard(updateCardDto, CarId);
                    if (result)
                    {
                        return Ok("Card updated successfully.");
                    }
                    else
                    {
                        return BadRequest("Failed to update card.");
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"An error occurred: {ex.Message}");
                }
            }

        }
    }

}
