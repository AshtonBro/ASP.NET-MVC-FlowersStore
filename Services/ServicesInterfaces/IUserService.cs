using FlowersStore.Models;
using System;

namespace FlowersStore.Services.ServicesInterfaces
{
    public interface IUserService
    {
        User GetUser(string userName);
    }
}
