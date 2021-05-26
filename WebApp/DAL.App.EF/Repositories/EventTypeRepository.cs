using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;


namespace DAL.App.EF.Repositories
{
    public class EventTypeRepository : BaseRepository<DAL.App.DTO.EventType, Domain.App.Event_Type, AppDbContext>, IEventTypeRepository
    {
        public EventTypeRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new EventTypeMapper(mapper))
        {
        }
        
        public override async Task<EventType?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = InitializeQuery(userId, noTracking);
            
            var resQuery = query
                .Include(c => c.Name)
                .ThenInclude(t => t!.Translations);
            var res = await resQuery.FirstOrDefaultAsync(m => m.Id == id);
            return Mapper.Map(res);
        }

        public override async Task<IEnumerable<EventType>> GetAllAsync(Guid userId, bool noTracking = true)
        {
            var query = InitializeQuery(userId, noTracking);
            
            var resQuery = query
                .Include(c => c.Name)
                .ThenInclude(t => t!.Translations)
                .Select(x => Mapper.Map(x));

            var res = await resQuery.ToListAsync();
            return res!;
        }
    }
}