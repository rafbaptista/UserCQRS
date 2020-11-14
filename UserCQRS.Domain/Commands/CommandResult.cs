using UserCQRS.Domain.Interfaces.Commands;

namespace UserCQRS.Domain.Commands
{
    public class CommandResult : ICommandResult
    {
        public CommandResult(bool success, string message, object data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public CommandResult()
        {

        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
