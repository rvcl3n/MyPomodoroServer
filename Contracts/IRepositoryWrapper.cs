namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IPomodoroRepository Pomodoro { get; }
        IUserRepository User { get; }
        void Save();
    }
}
