using MongoDB.Driver;
using TFG.Application.Contracts.Persistence;
using TFG.Application.Contracts.Service;
using TFG.Domain.Entities;

namespace TFG.Application.Features.Service;

public class RoleService : IRoleService {
    private readonly IRoleRepository _roleRepository;

    public RoleService (IRoleRepository roleRepository) {
        _roleRepository = roleRepository;
    }
    public async Task CreateRole (Role role) {
        await _roleRepository.Create (role);
    }

    public async Task<Role> GetRoleByName (string rolename) {
        var filter = Builders<Role>.Filter.Eq (x => x.Name.ToLower (), rolename.ToLower ());
        var role = (await _roleRepository.GetAsync (filter)).FirstOrDefault ();

        return role;
    }
}