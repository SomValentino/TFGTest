using TFG.Application.Contracts.Service;

namespace TFG.API.Migrations;

public class RoleMigration
{
    private readonly IRoleService _roleService;

    public RoleMigration(IRoleService roleService) => _roleService = roleService;

    public async Task CreateRoles()
    {
        var roles = new [] {"Administrator","User"};

        foreach (var role in roles)
        {
            var roleExist = await _roleService.GetRoleByName(role);

            if(roleExist == null) await _roleService.CreateRole(new Domain.Entities.Role{
                Name = role,
                CreatedAt = DateTime.UtcNow
            });

        }
    }
}