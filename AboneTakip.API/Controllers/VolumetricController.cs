using AboneTakip.Business.Abstract;
using AboneTakip.Core.Enums;
using AboneTakip.DTOs.Volumetrics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AboneTakip.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VolumetricController : ControllerBase
    {
        private readonly IVolumetricService _volumetricService;

        public VolumetricController(IVolumetricService volumetricService)
        {
            _volumetricService = volumetricService;
        }

        [HttpGet("GetAllVolumetricPreloads")]
        public async Task<IActionResult> GetAllVolumetricPreloads()
        {
            var result = await _volumetricService.GetAll();

            if (result.ResultStatus != ResultStatus.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("GetVolumetricPreloadById")]
        public async Task<IActionResult> GetVolumetricPreloadById(Guid id)
        {
            var result = await _volumetricService.GetById(id);

            if (result.ResultStatus != ResultStatus.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("GetCustomerVolumetricPreloads")]
        public async Task<IActionResult> GetCustomerVolumetricPreloads(Guid id)
        {
            var result = await _volumetricService.GetCustomerVolumetric(id);

            if (result.ResultStatus != ResultStatus.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("GetCustomerNotInvoicedVolumetricPreloads")]
        public async Task<IActionResult> GetCustomerNotInvoicedVolumetricPreloads(Guid id)
        {
            var result = await _volumetricService.GetCustomerNotInvoicedPreloads(id);

            if (result.ResultStatus != ResultStatus.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("AddVolumetricPreload")]
        public async Task<IActionResult> AddVolumetricPreload(VolumetricCreateDTO volumetricCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _volumetricService.Add(volumetricCreateDTO);

            if (result.ResultStatus != ResultStatus.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut("UpdateVolumetricPreload")]
        public async Task<IActionResult> UpdateVolumetricPreload(VolumetricUpdateDTO volumetricUpdateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _volumetricService.Update(volumetricUpdateDTO);

            if (result.ResultStatus != ResultStatus.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("DeleteVolumetricPreload")]
        public async Task<IActionResult> DeleteVolumetricPreload(Guid id)
        {
            var result = await _volumetricService.Delete(id);

            if (result.ResultStatus != ResultStatus.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
