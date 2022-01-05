using Castle.Core.Internal;
using LACP.Data.Repositories.Repositories.Interfaces;
using LACP.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LACP.Data.Repositories
{
    public class LocationChargePointRepository : ILocationChargePointRepository
    {
        private readonly LacpDbContext _context;
        public LocationChargePointRepository(LacpDbContext context)
        {
            _context = context;
        }

        public async Task CreateLocation(Location entity)
        {
            await _context.Locations.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task EditLocation(Location entity)
        {
            _context.Attach(entity);
            _context.Locations.Update(entity);
            await _context.SaveChangesAsync();
        }
        public async Task AddNewChargePoint(ChargePoint entity)
        {
            await _context.ChargePoints.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateExistingChargePoint(ChargePoint entity)
        {
            _context.ChargePoints.Update(entity);
            await _context.SaveChangesAsync();
        }
        public async Task<List<ChargePoint>> ChargePointsInLocation(string id)
        {
            List<ChargePoint> cpsInLocation = await _context.ChargePoints.Where(cp => cp.LocationId == id).ToListAsync();
            return cpsInLocation;
        }

        public async Task<Location> GetLocation(string id)
        {
            return await _context.Locations.FirstOrDefaultAsync(l => l.LocationId == id);
        }
        public async Task<bool> LocationExists(string id)
        {
            return await _context.Locations.AnyAsync(l => l.LocationId == id);
        }

    }
}
