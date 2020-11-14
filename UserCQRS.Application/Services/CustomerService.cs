using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserCQRS.Domain.Commands.CustomerCommands.AddCustomerCommand;
using UserCQRS.Domain.Commands.CustomerCommands.DeleteCustomerCommand;
using UserCQRS.Domain.Commands.CustomerCommands.UpdateCustomerCommand;
using UserCQRS.Domain.Entities;
using UserCQRS.Domain.Interfaces.Commands;
using UserCQRS.Domain.Interfaces.Repositories;
using UserCQRS.Domain.Interfaces.Services;
using UserCQRS.Domain.Queries.GetCustomerById;
using UserCQRS.Domain.ViewModels;

namespace UserCQRS.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(IMediator mediator, IMapper mapper, ICustomerRepository customerRepository)
        {
            _mediator = mediator;
            _mapper = mapper;
            _customerRepository = customerRepository;
        }

        public async Task<ICommandResult> Add(CustomerViewModel customer)
        {
            var addCustomerCommand = _mapper.Map<AddCustomerCommand>(customer);
            return await _mediator.Send(addCustomerCommand);
        }

        public async Task<ICommandResult> Delete(int id)
        {
            var deleteCustomerCommand = new DeleteCustomerCommand(id);
            return await _mediator.Send(deleteCustomerCommand);
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _customerRepository.GetAll();            
        }

        public async Task<CustomerViewModel> GetById(int id)
        {
            var getByIdCommand = new GetCustomerByIdQuery(id);
            return await _mediator.Send(getByIdCommand);            
        }

        public async Task<ICommandResult> Update(Customer customer)
        {
            var updateCustomerCommand = _mapper.Map<UpdateCustomerCommand>(customer);
            return await _mediator.Send(updateCustomerCommand);
        }
    }
}
