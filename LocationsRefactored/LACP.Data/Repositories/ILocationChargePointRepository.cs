using LACP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LACP.Data.Repositories
{
    public interface ILocationChargePointRepository
    {
        Task CreateLocation(Location entity);
        Task EditLocation(Location entity);
        Task AddNewChargePoint(ChargePoint entity);
        Task UpdateExistingChargePoint(ChargePoint entity);
        Task<List<ChargePoint>> ChargePointsInLocation(string id);
        Task<Location> GetLocation(string id);
        Task<bool> LocationExists(string id);


    }
}
