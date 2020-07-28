using Entities.Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Contracts
{
    public interface IPomodoroRepository : IRepositoryBase<Pomodoro>
    {
        Task<IEnumerable<Pomodoro>> GetAllPomodoros();

        IEnumerable<Pomodoro> GetAllPomodorosByUser(string externalId);
        Pomodoro GetPomodoroById(Guid pomodoroId);
        void CreatePomodoro(Pomodoro pomodoro);
        void UpdatePomodoro(Pomodoro dbPomodoro, Pomodoro pomodoro);
        void DeletePomodoro(Pomodoro pomodoro);
    }
}
