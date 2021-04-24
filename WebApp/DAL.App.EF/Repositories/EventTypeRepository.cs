using System;
using System.Collections;
using System.Collections.Generic;
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
        
        
    }
}