using System.Threading.Tasks;
using Contracts;
using Entities;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;
        private IPomodoroRepository _pomodoro;
        private IUserRepository _user;

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

        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_repoContext);
                }

                return _user;
            }
        }

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        public async Task SaveAsync()
        {
            await _repoContext.SaveChangesAsync();
        }
    }
}