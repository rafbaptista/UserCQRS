using System.Data;

namespace UserCQRS.Domain.Interfaces.Repositories
{
    public interface IConnectionFactory
    {
        IDbConnection Connection();
        string ConnectionString { get; set; }
    }
}
