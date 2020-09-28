using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Services
{
    public interface IUserService
    {
        Task<long> Create(User user);
        Task<User> Get(long id);
        Task<IEnumerable<User>> GetAll(CancellationToken cancellationToken);
        Task Update(User user);
        Task Delete(long id);
    }
}