using System.Threading.Tasks;
using UserCQRS.Domain.Interfaces.Transactions;

namespace UserCQRS.Domain.Commands
{
    public abstract class CommandHandler
    {
        public CommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
            Result = new CommandResult();            
        }

        private readonly IUnitOfWork _uow;
        public CommandResult Result { get; set; }

        protected async Task Commit()
        {
            await _uow.Commit();
        }

        protected CommandResult CommandResult(bool success, string message, object data)
        {
            return new CommandResult(success, message, data);
        }

    }
}
