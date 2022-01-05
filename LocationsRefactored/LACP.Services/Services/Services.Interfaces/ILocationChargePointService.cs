using LACP.Models.ViewModels;
using LACP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LACP.Services.Services.Services.Interfaces
{
    public interface ILocationChargePointService
    {
        //get location
        Task<Location> GetLocation(string id);
        //post location
        Task CreateLocation(LocationRequest model);
        //patch location
        Task EditLocation(PatchLocationRequest model);
        //get list of ChargePoints to insert in existing location
        Task InsertChargePoints(ChargePointRequestViewModel model);

    }
}
