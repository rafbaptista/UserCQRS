using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using UserCQRS.Domain.ViewModels;

namespace UserCQRS.Domain.Queries.GetAllCustomers
{
    public class GetAllCustomersQuery : IRequest<IEnumerable<CustomerViewModel>>
    {
    }
}
