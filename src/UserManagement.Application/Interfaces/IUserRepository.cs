﻿using System.Threading;
using System.Threading.Tasks;
using UserManagement.Application.Common;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<PagedResult<User>> GetAll(PageInfo pageInfo, CancellationToken cancellationToken);
        Task<User> Get(long id);
        Task<long> Create(User user);
        Task Update(User user);
        Task Delete(long id);
    }
}
