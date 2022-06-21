using TFG.Application.Contracts.Service;

namespace TFG.API.Middleware;

public class RoleMigration
{
    private readonly IRoleService _roleService;

    public RoleMigration(IRoleService roleService) => _roleService = roleService;

    public Task CreateRoles()
    {
        
    }
}