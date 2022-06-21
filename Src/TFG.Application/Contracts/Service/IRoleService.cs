using TFG.Domain.Entities;

namespace TFG.Application.Contracts.Service;

public interface IRoleService {
    Task CreateRole(Role role);
    Task<Role> GetRoleByName(string rolename);
}