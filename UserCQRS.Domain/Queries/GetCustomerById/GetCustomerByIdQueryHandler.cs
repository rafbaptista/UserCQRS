using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using UserCQRS.Domain.Interfaces.Repositories;
using UserCQRS.Domain.ViewModels;

namespace UserCQRS.Domain.Queries.GetCustomerById
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerViewModel>
    {        
        private readonly ICustomerReadOnlyRepository _customerReadOnlyRepository;
        private readonly IMapper _mapper;

        public GetCustomerByIdQueryHandler(IMapper mapper, ICustomerReadOnlyRepository customerReadOnlyRepository)
        {
            _mapper = mapper;
            _customerReadOnlyRepository = customerReadOnlyRepository;
        }

        public async Task<CustomerViewModel> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {                        
            return _mapper.Map<CustomerViewModel>(await _customerReadOnlyRepository.GetById(request.Id));            
        }
    }
}
