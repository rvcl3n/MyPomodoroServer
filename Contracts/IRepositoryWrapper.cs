namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IPomodoroRepository Pomodoro { get; }
        void Save();
    }
}
