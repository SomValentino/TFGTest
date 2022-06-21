using TFG.Application.Contracts.Persistence;
using TFG.Domain.Entities;
using TFG.Infrastructure.Data;

namespace TFG.Infrastructure.Repository;

public class RoleRepository : BaseRepository<Role>,IRoleRepository
{
    public RoleRepository(RoleDataContext roleDataContext) : base (roleDataContext)
    {
        
    }
}