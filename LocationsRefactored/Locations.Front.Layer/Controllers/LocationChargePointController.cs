using AutoMapper;
using LACP.Models.ViewModels;
using LACP.Models;
using LACP.Services.Services.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Locations.Front.Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationChargePointController : ControllerBase
    {
        private readonly ILocationChargePointService _service;
        private readonly IMapper _mapper;
        public LocationChargePointController(ILocationChargePointService service,IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetLocation(string id)
        {
            try
            {
                Location loc = await _service.GetLocation(id);
                //might want to move this mapping and adding of chargepoints in the service layer
                LocationViewModel locVM = _mapper.Map<LocationViewModel>(loc);
                locVM.ChargePoints.AddRange(_mapper.Map<List<ChargePointViewModel>>(loc.ChargePoints).ToList());
                return Ok(locVM);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        [HttpPost]
        public async Task<IActionResult> PostLocation(LocationRequest model)
        {
            try
            {
                if (Enum.IsDefined(typeof(LACP.Models.Type), model.Type))
                {
                    await _service.CreateLocation(model);
                    return Ok(model);
                }
                else
                    return BadRequest("Type: " + model.Type);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> PatchLocation(string id, PatchLocationRequest model)
        {
            try
            {
                if (id == model.LocationId)
                {
                    if (Enum.IsDefined(typeof(LACP.Models.Type), model.Type))
                    {
                        await _service.EditLocation(model);
                        return Ok();
                    }
                    else
                        return BadRequest("Type: " + model.Type);

                }
                else
                    return BadRequest("Id: " + model.LocationId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> PutChargePoints(string id, ChargePointRequestViewModel model)
        {
            try
            {
                if (id == model.LocationId)
                {
                    List<string> idsOfChargePoints = new List<string>();
                    foreach (ChargePointViewModel cpvm in model.ChargePoints)
                    {
                        if (!Enum.IsDefined(typeof(Status), cpvm.Status))
                        {
                            idsOfChargePoints.Add(cpvm.ChargePointId);
                        }
                    }
                    if (idsOfChargePoints.Count > 0)
                    {
                        return BadRequest("Number of ChargePoints with wrong Status written: " + idsOfChargePoints.Count);
                    }
                    else
                    {
                        await _service.InsertChargePoints(model);
                        return Ok();
                    }
                }
                else
                    return BadRequest("Id: " + model.LocationId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
