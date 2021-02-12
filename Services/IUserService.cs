using FlowersStore.Models;
using System;

namespace FlowersStore.Services
{
    public interface IUserService
    {
        Guid GetUser(string userName);
    }
}
