using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserCQRS.Domain.Entities;
using UserCQRS.Domain.Interfaces.Repositories;
using UserCQRS.Domain.Interfaces.Transactions;
using UserCQRS.Domain.ViewModels;

namespace UserCQRS.Domain.Commands.CustomerCommands.UpdateCustomerCommand
{
    public class UpdateCustomerCommandHandler : CommandHandler, IRequestHandler<UpdateCustomerCommand, CommandResult>
    {
        private readonly IUnitOfWork _uow;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerReadOnlyRepository _customerReadOnlyRepository;
        private readonly IMapper _mapper;

        public UpdateCustomerCommandHandler(IUnitOfWork uow, ICustomerRepository customerRepository, ICustomerReadOnlyRepository customerReadOnlyRepository, IMapper mapper)
            : base(uow)
        {
            _uow = uow;
            _customerRepository = customerRepository;
            _customerReadOnlyRepository = customerReadOnlyRepository;
            _mapper = mapper;
        }

        public async Task<CommandResult> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            if (request.IsValid())
            {
                Customer existingCustomer = await _customerReadOnlyRepository.GetByEmail(request.Email);

                if (existingCustomer != null && existingCustomer.Id != request.Id)                    
                    return CommandResult(success: false, message: "E-mail já cadastrado", data: null);

                Customer customerToUpdate = new Customer(request.Id, request.FirstName, request.LastName, request.Email, request.Address, request.Age);
                await _customerRepository.Update(customerToUpdate);
                await Commit();

                return CommandResult(success: true, message: "Usuário atualizado com sucesso", data: _mapper.Map<CustomerViewModel>(await _customerReadOnlyRepository.GetById(request.Id)));
            }
            else
            {
                return CommandResult(success: false, message: "Erro ao atualizar usuário", data: request.ValidationResult.Errors);
            }
        }
    }
}
