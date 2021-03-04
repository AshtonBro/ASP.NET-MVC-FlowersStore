using FlowersStore.Models;
using FlowersStore.ViewModels;

namespace FlowersStore.Services.ServicesInterfaces
{
    public interface IUserService
    {
        User GetUser(string userName);
        string GetUserRole(string userName);
        bool UserUpdate(User user);
    }
}