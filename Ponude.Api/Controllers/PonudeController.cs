using Microsoft.AspNetCore.Mvc;
using Ponude.Application.Contracts;
using Ponude.Application.Interfaces;

namespace Ponude.Api.Controllers
{
    public class PonudeController : ControllerBase
    {
        private readonly IPonudeService _ponudeService;

        public PonudeController(IPonudeService ponudeService) => _ponudeService = ponudeService;

        [HttpPost(ApiEndpoints.Ponude.Create)]
        public async Task<IActionResult> Create([FromBody] PonudaCreateDTO request, CancellationToken token)
        {
            if (request is null) 
                return BadRequest();

            var ponudaDTO = await _ponudeService.CreateAsync(request, token);
            return CreatedAtAction(nameof(Get), new { idOrBrojPonude = ponudaDTO.Id }, ponudaDTO);
        }

        [HttpGet(ApiEndpoints.Ponude.Get)]
        public async Task<IActionResult> Get([FromRoute] string idOrBrojPonude, CancellationToken token)
        {
            var ponuda = Guid.TryParse(idOrBrojPonude, out var id)
                ? await _ponudeService.GetByIdAsync(id, token)
                : int.TryParse(idOrBrojPonude, out int brojPonude)
                ? await _ponudeService.GetByBrojPonudeAsync(int.Parse(idOrBrojPonude), token)
                : null;
            if (ponuda is null)
                return NotFound();

            return Ok(ponuda);
        }

        [HttpGet(ApiEndpoints.Ponude.GetByPage)]
        public async Task<IActionResult> GetByPage([FromRoute] int pageNumber, [FromQuery]int? pageSize, CancellationToken token)
        {
            int _pageSize = pageSize ?? 3;
            _pageSize = _pageSize < 0 || _pageSize > 100 ? 3 : _pageSize;
            var ponudePage = await _ponudeService.GetPonudeByPageAsync(pageNumber, _pageSize, token);
            if (ponudePage is null)
                return NotFound();

            return Ok(ponudePage);
        }

        [HttpPut(ApiEndpoints.Ponude.Update)]
        public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody]PonudaDTO request, CancellationToken token)
        {
            if(request is null)
                return BadRequest();

            var result = await _ponudeService.UpdateAsync(id, request, token);
            return Ok(result);
        }

        [HttpDelete(ApiEndpoints.Ponude.Delete)]
        public async Task<IActionResult> Delete([FromRoute]Guid id, CancellationToken token)
        {
            var deleted = await _ponudeService.DeleteAsync(id, token);

            if(!deleted) 
                return NotFound();

            return NoContent();
        }

    }
}
