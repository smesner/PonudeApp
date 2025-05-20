using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ponude.Application.Contracts;
using Ponude.Application.Interfaces;

namespace Ponude.Api.Controllers
{
    [ApiController]
    public class ArtikliController : ControllerBase
    {
        private readonly IArtikliService _artikliService;

        public ArtikliController(IArtikliService artikliService) => _artikliService = artikliService;

        [HttpPost(ApiEndpoints.Artikli.Create)]
        public async Task<IActionResult> Create([FromBody] ArtiklCreateDTO request, CancellationToken token)
        {
            if (request is null)
                return BadRequest();
            var response = _artikliService.CreateAsync(request, token);

            return CreatedAtAction(nameof(Get), new { id = response.Id }, response);
        }

        [HttpGet(ApiEndpoints.Artikli.Get)]
        public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken token)
        {
            var response = await _artikliService.GetByIdAsync(id, token);
            if (response is null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet(ApiEndpoints.Artikli.GetAll)]
        public async Task<IActionResult> GetAll(CancellationToken token)
        {
            var response = await _artikliService.GetAllAsync(token);
            if (response is null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet(ApiEndpoints.Artikli.Search)]
        public async Task<IActionResult> GetOnDemand([FromRoute] string request, CancellationToken token)
        {
            var response = await _artikliService.SearchAsync(request, token);
            if (response is null)
                return NotFound();
            
            return Ok(response);
        }

        [HttpPut(ApiEndpoints.Artikli.Update)]
        public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody]ArtiklDTO request, CancellationToken token)
        {
            if (request is null)
                return BadRequest();

            var response = await _artikliService.UpdateAsync(id, request, token);
            if(response is null)
                return NotFound();
            
            return Ok(response);
        }

        [HttpDelete(ApiEndpoints.Artikli.Delete)]
        public async Task<IActionResult> Delete([FromRoute]Guid id, CancellationToken token)
        {
            var response = await _artikliService.DeleteAsync(id, token);
            if (!response) 
                return NotFound();
            
            return NoContent();
        }
    }
}
