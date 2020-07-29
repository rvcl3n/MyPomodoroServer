using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class PomodoroRepository : RepositoryBase<Pomodoro>, IPomodoroRepository
    {
        public PomodoroRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Pomodoro>> GetAllPomodoros()
        {
            return await FindAll()
                .OrderBy(p => p.StartTime)
                .ToListAsync();
        }

        public async Task<IEnumerable<Pomodoro>> GetAllPomodorosByUser(string externalId)
        {
            return await FindByCondition(pomodoro => pomodoro.User.ExternalId == externalId)
                .OrderBy(p => p.StartTime)
                .ToListAsync();
        }

        public async Task<Pomodoro> GetPomodoroById(Guid pomodoroId)
        {
            return await FindByCondition(pomodoro => pomodoro.Id.Equals(pomodoroId))
                    //.DefaultIfEmpty(new Pomodoro())
                    .FirstOrDefaultAsync();
        }

        public void CreatePomodoro(Pomodoro pomodoro)
        {
            Create(pomodoro);
        }

        public void UpdatePomodoro(Pomodoro dbPomodoro, Pomodoro pomodoro)
        {
            //dbOwner.Map(owner);
            Update(pomodoro);
        }

        public void DeletePomodoro(Pomodoro pomodoro)
        {
            Delete(pomodoro);
        }
    }
}
