using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class PomodoroRepository : RepositoryBase<Pomodoro>, IPomodoroRepository
    {
        public PomodoroRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<Pomodoro> GetAllPomodoros()
        {
            return FindAll()
                .OrderBy(p => p.StartTime)
                .ToList();
        }

        public Pomodoro GetPomodoroById(Guid pomodoroId)
        {
            return FindByCondition(pomodoro => pomodoro.Id.Equals(pomodoroId)).ToList()
                    //.DefaultIfEmpty(new Pomodoro())
                    .FirstOrDefault();
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
