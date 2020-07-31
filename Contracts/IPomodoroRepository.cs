using Entities.Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Contracts
{
    public interface IPomodoroRepository : IRepositoryBase<Pomodoro>
    {
        Task<IEnumerable<Pomodoro>> GetAllPomodoros();

        Task<IEnumerable<Pomodoro>> GetAllPomodorosByUser(string externalId);
        Task<Pomodoro> GetPomodoroById(Guid pomodoroId);
        Task CreatePomodoro(Pomodoro pomodoro);
        Task UpdatePomodoro(Pomodoro dbPomodoro, Pomodoro pomodoro);
        Task DeletePomodoro(Pomodoro pomodoro);
    }
}
