using System.Threading.Tasks;
using FlowersStore.Core.CoreModels;

namespace FlowersStore.Core.Services
{
    public interface IUserService
    {
        Task<User> Get(string userNameContext);

        Task<string> GetUserClaim(string userNameContext);

        Task<bool> Update(User user);
    }
}