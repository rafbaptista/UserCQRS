using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserCQRS.Domain.Entities;
using UserCQRS.Domain.Interfaces.Commands;
using UserCQRS.Domain.Interfaces.Repositories;
using UserCQRS.Domain.Interfaces.Transactions;

namespace UserCQRS.Domain.Commands.CustomerCommands.DeleteCustomerCommand
{
    public class DeleteCustomerCommandHandler : CommandHandler, IRequestHandler<DeleteCustomerCommand, ICommandResult>
    {
        private readonly IUnitOfWork _uow;
        private readonly ICustomerReadOnlyRepository _customerReadOnlyRepository;
        private readonly ICustomerRepository _customerRepository;

        public DeleteCustomerCommandHandler(IUnitOfWork uow, ICustomerReadOnlyRepository customerReadOnlyRepository, ICustomerRepository customerRepository)
            :base(uow)
        {
            _uow = uow;
            _customerReadOnlyRepository = customerReadOnlyRepository;
            _customerRepository = customerRepository;
        }

        public async Task<ICommandResult> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return CommandResult(success: false, message: "Id inválido", data: null);

            Customer customer = await _customerReadOnlyRepository.GetById(request.Id);
            if (customer == null)
                return CommandResult(success: false, message: "Usuário não encontrado", data: null);

            await _customerRepository.Delete(customer);
            await Commit();

            return CommandResult(success: true, message: "Usuário deletado", data: null);

        }
    }
}
