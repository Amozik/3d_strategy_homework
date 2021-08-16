namespace Abstractions.Commands
{
    public interface ICommandsQueue
    {
        void EnqueueCommand(ICommand command);
    }
}