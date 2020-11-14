using MediatR;
using UserCQRS.Domain.ViewModels;

namespace UserCQRS.Domain.Queries.GetCustomerById
{
    public class GetCustomerByIdQuery : IRequest<CustomerViewModel>
    {
        public GetCustomerByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
