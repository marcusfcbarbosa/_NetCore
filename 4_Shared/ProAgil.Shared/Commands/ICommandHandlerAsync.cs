using System.Threading.Tasks;

namespace ProAgil.Shared.Commands
{
    public interface ICommandHandlerAsync<T> where T : ICommand
    {
          Task<ICommandResult> Handle(T command);
    }
}