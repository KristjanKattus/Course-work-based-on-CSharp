using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;

namespace DAL.App.EF.Repositories
{
    public class EventTypeRepository : BaseRepository<Event_Type, AppDbContext>, IEventTypeRepository
    {
        public EventTypeRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}