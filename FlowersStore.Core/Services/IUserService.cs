using FlowersStore.Core.CoreModels;

namespace FlowersStore.Core.Services
{
    public interface IUserService
    {
        User GetUser(string userName);
        string GetUserRole(string userName);
        bool UserUpdate(User user);
    }
}