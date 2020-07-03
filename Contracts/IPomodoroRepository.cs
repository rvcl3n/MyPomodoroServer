using Entities.Models;
using System;
using System.Collections.Generic;

namespace Contracts
{
    public interface IPomodoroRepository : IRepositoryBase<Pomodoro>
    {
        IEnumerable<Pomodoro> GetAllPomodoros();
        Pomodoro GetPomodoroById(Guid pomodoroId);
        void CreatePomodoro(Pomodoro pomodoro);
        void UpdatePomodoro(Pomodoro dbPomodoro, Pomodoro pomodoro);
        void DeletePomodoro(Pomodoro pomodoro);
    }
}
