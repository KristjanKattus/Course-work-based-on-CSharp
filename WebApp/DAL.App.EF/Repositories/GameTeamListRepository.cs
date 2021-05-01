using System;
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
    public class GameTeamListRepository : BaseRepository<DAL.App.DTO.GameTeamList, Domain.App.Game_Team_List, AppDbContext>, IGameTeamListRepository

    {
        public GameTeamListRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new GameTeamListMapper(mapper))
        {
        }


        public override async Task<IEnumerable<GameTeamList>> GetAllAsync(Guid userId, bool noTracking = true)
        {
            var query = InitializeQuery(userId, noTracking);

            var resQuery = query
                .Include(g => g.Person)
                .Include(g => g.GameTeam)
                .Include(g => g.Role)
                .Select(g => Mapper.Map(g));

            var res = await resQuery.ToListAsync();

            return res!;
        }

        public override async Task<GameTeamList?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = InitializeQuery(userId, noTracking);

            var resQuery = query
                .Include(g => g.Person)
                .Include(g => g.GameTeam)
                .Include(g => g.Role);
                

            var res = Mapper.Map(await resQuery.FirstOrDefaultAsync(g => g.Id == id));

            return res;
        }
    }
}