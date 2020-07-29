using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IPomodoroRepository Pomodoro { get; }
        IUserRepository User { get; }
        Task SaveAsync();
    }
}
