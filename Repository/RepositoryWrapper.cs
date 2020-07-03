using Contracts;
using Entities;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;
        private IPomodoroRepository _pomodoro;

        public IPomodoroRepository Pomodoro
        {
            get
            {
                if (_pomodoro == null)
                {
                    _pomodoro = new PomodoroRepository(_repoContext);
                }

                return _pomodoro;
            }
        }

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}