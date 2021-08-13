using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FlowersStore.Core.CoreModels;
using FlowersStore.Core.Repositories;
using AutoMapper;

namespace FlowersStore.DataAccess.MSSQL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FlowersStoreDbContext _context;
        private readonly IMapper _mapper;

        public UserRepository(FlowersStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<User> Get(string userNameContext)
        {
            if (string.IsNullOrEmpty(userNameContext))
            {
                throw new ArgumentNullException(nameof(userNameContext));
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(f => f.NormalizedUserName == userNameContext.ToUpper());

            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return _mapper.Map<Entities.User, Core.CoreModels.User>(user);
        }

        public async Task<string> GetUserClaim(string userNameContext)
        {
            if (string.IsNullOrEmpty(userNameContext))
            {
                throw new ArgumentNullException(nameof(userNameContext));
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(f => f.NormalizedUserName == userNameContext.ToUpper());

            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var UserClaims = await _context.UserClaims
                .FirstOrDefaultAsync(f => f.UserId == user.Id);

            if (UserClaims is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return UserClaims.ClaimValue;
        }

        public async Task<bool> Update(User user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var exsistedUser = await _context.Users
                .FirstOrDefaultAsync(f => f.Id == user.Id);

            if (exsistedUser is null)
            {
                throw new ArgumentNullException(nameof(exsistedUser));
            }

            _context.Entry(exsistedUser).State = EntityState.Modified;

            exsistedUser.Name = user.Name;
            exsistedUser.SecondName = user.SecondName;
            exsistedUser.PhoneNumber = user.PhoneNumber;
            exsistedUser.Email = user.Email;
            exsistedUser.NormalizedEmail = user.Email.ToUpper();
            exsistedUser.PasswordHash = user.PasswordHash;
            exsistedUser.UserName = user.Name;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}