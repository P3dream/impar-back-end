namespace impar_back_end.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using global::impar_back_end.Models.Card;
    using global::impar_back_end.Services;
    using global::impar_back_end.Models.PageOptions;

    namespace impar_back_end.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class CardController : ControllerBase
        {
            private readonly CardService _cardService;

            public CardController(CardService cardService)
            {
                _cardService = cardService;
            }



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

            [HttpDelete("delete")]
            public async Task<IActionResult> DeleteCard(int carId, int photoId)
            {
                try
                {
                    var result = await _cardService.DeleteCard(carId, photoId);
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
    

            [HttpGet]
            public async Task<IActionResult> GetCards([FromQuery] PageOptionsDto pageOptionsDto)
            {
                try
                {
                    var cards = await _cardService.GetCards(pageOptionsDto);
                    return Ok(cards);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }
            [HttpPut]
            public async Task<IActionResult> UpdateCard([FromForm] UpdateCardDto updateCardDto)
            {
                try
                {
                    var result = await _cardService.UpdateCard(updateCardDto);
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
