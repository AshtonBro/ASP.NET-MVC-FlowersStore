using System.Threading.Tasks;
using FlowersStore.Core.CoreModels;

namespace FlowersStore.Core.Repositories
{
    public interface IUserRepository
    {
        Task<User> Get(string userNameContext);

        Task<string> GetUserClaim(string userNameContext);

        Task<bool> Update(User user);
    }
}
