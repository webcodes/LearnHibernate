using System.Threading.Tasks;

namespace LearnHibernate.Core
{
    public interface ICommandHandler<T>
    {
        void Execute(T command);

        Task ExecuteAsync(T command);
    }
}
