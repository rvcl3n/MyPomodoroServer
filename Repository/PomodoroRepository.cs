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
            /*return FindAll()
                .OrderBy(ow => ow.Name)
                .ToList();*/

            return null;
        }

        public Pomodoro GetPomodoroById(Guid pomodoroId)
        {
            /*return FindByCondition(owner => owner.Id.Equals(ownerId)).ToList()
                    .DefaultIfEmpty(new Owner())
                    .FirstOrDefault();*/

            return null;
        }

        public void CreatePomodoro(Pomodoro pomodoro)
        {
            /*owner.Id = Guid.NewGuid();
            Create(owner);*/
        }

        public void UpdatePomodoro(Pomodoro dbPomodoro, Pomodoro pomodoro)
        {
            /*dbOwner.Map(owner);
            Update(dbOwner);*/
        }

        public void DeletePomodoro(Pomodoro pomodoro)
        {
            //Delete(pomodoro);
        }
    }
}
