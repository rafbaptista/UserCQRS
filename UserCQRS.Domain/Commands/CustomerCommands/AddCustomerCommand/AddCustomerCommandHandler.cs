using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserCQRS.Domain.Entities;
using UserCQRS.Domain.Interfaces.Repositories;
using UserCQRS.Domain.Interfaces.Transactions;
using UserCQRS.Domain.ViewModels;

namespace UserCQRS.Domain.Commands.CustomerCommands.AddCustomerCommand
{
    public class AddCustomerCommandHandler : CommandHandler, IRequestHandler<AddCustomerCommand, CommandResult>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public AddCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork uow, IMapper mapper)
            :base(uow)
        {
            _customerRepository = customerRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<CommandResult> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
        {           
            if (request.IsValid())
            {                
                await _customerRepository.Add(_mapper.Map<Customer>(request));
                await Commit();
                return CommandResult(success: true, message: "Usuário cadastrado com sucesso", data: _mapper.Map<CustomerViewModel>(request));                                                
            }
            else
            {
                return CommandResult(success: false, message: "Erro ao cadastrar usuário", data: request.ValidationResult.Errors);                                 
            }            
        }
    }
}
