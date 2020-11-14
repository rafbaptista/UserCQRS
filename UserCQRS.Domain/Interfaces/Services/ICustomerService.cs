using System.Collections.Generic;
using System.Threading.Tasks;
using UserCQRS.Domain.Entities;
using UserCQRS.Domain.Interfaces.Commands;
using UserCQRS.Domain.ViewModels;

namespace UserCQRS.Domain.Interfaces.Services
{
    public interface ICustomerService
    {
        Task<ICommandResult> Add(CustomerViewModel customer);        
        Task<ICommandResult> Update(Customer customer);
        Task<ICommandResult> Delete(int id);
        Task<IEnumerable<Customer>> GetAll();
        Task<CustomerViewModel> GetById(int id);
    }
}
