using A1Events.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A1Events.Application.Interfaces
{
    public interface IAdminUserRepository
    {
        Task<AdminUser> CreateAsync(AdminUser adminUser);
        Task<AdminUser?> GetByUsernameAsync(string username);
    }
}
