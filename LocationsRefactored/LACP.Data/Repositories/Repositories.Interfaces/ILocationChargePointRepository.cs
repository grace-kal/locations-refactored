using LACP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LACP.Data.Repositories.Repositories.Interfaces
{
    public interface ILocationChargePointRepository
    {
        Task CreateLocation(Location entity);
        Task EditLocation(Location entity);
        Task AddNewChargePoint(ChargePoint entity);
        //when a cp already exists but is not in the given new list the Status property needs to be set to "Removed" hence updated
        //---another alternative is creating a new one with status removed and deleting the old one
        Task UpdateExistingChargePoint(ChargePoint entity);
        //we get the list of ChargePoints for specific location by the LocationId(id); 
        ////check if a charge point exists
        Task<List<ChargePoint>> ChargePointsInLocation(string id);
        //id==LocationId
        Task<Location> GetLocation(string id);
        //id==LocationId
        //alternative method for checking if location exists is by altering the GetLocationMethod
        Task<bool> LocationExists(string id);
    }
}
