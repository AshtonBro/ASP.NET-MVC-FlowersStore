using System;
using System.Threading.Tasks;
using FlowersStore.Core.CoreModels;
using FlowersStore.Core.Repositories;
using FlowersStore.Core.Services;

namespace FlowersStore.BusinessLogic
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Get(string userNameContext)
        {
            if (string.IsNullOrEmpty(userNameContext))
            {
                throw new ArgumentNullException(nameof(userNameContext));
            }

            return await _userRepository.Get(userNameContext);
        }

        public async Task<string> GetUserClaim(string userNameContext)
        {
            if (string.IsNullOrEmpty(userNameContext))
            {
                throw new ArgumentNullException(nameof(userNameContext));
            }

            return await _userRepository.GetUserClaim(userNameContext);
        }

        public async Task<bool> Update(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return await _userRepository.Update(user);
        }
    }
}