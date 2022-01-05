using AutoMapper;
using LACP.Data.Repositories.Repositories.Interfaces;
using LACP.Models;
using LACP.Models.ViewModels;
using LACP.Services.Services.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LACP.Services.Services
{
    public class LocationChargePointService: ILocationChargePointService
    {
        private readonly ILocationChargePointRepository _repo;
        private readonly IMapper _mapper;
        public LocationChargePointService(ILocationChargePointRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<Location>GetLocation(string id)
        {
            return await _repo.GetLocation(id);
        }
        public async Task CreateLocation(LocationRequest model)
        {
            if (!model.Equals(null))
            {
                //mapping the given LocationRequest model to Location model
                Location loc = _mapper.Map<Location>(model);
                await _repo.CreateLocation(loc);
            }
        }

        public async Task EditLocation(PatchLocationRequest model)
        {
            if (!model.Equals(null))
            {
                if (await _repo.LocationExists(model.LocationId))
                {
                    //update only existing val
                    //mapping the given PatchLocationRequest model to Location model
                    Location loc = _mapper.Map<Location>(model);
                    await _repo.EditLocation(loc);
                }

            }
        }

        public async Task InsertChargePoints(ChargePointRequestViewModel model)
        {
            List<ChargePoint> cpsInLocation = await _repo.ChargePointsInLocation(model.LocationId);
            ChargePointRequest cpsGiven = _mapper.Map<ChargePointRequest>(model);

            foreach (ChargePoint cp in cpsInLocation)
            {
                if (!cpsGiven.ChargePoints.Contains(cp))
                {
                    cp.Status = "Removed";
                    cp.LastUpdated = DateTime.Now;
                    await _repo.UpdateExistingChargePoint(cp);
                }
            }
            foreach (ChargePoint cp in cpsGiven.ChargePoints)
            {
                cp.LocationId = cpsGiven.LocationId;
                await _repo.AddNewChargePoint(cp);
            }
        }
    }
}
