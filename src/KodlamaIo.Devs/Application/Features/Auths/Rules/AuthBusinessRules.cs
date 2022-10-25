using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Core.Security.Hashing;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auths.Rules
{
    public class AuthBusinessRules
    {
        private readonly IUserRepository _userRepository;
        public AuthBusinessRules(IUserRepository userRepository)
        {
            _userRepository = userRepository;   
        }

        public async Task UserEmailCanNotBeDuplicatedWhenRegistered(string email)
        {
            User? user = await _userRepository.GetAsync(u => u.Email == email);
            if (user != null) throw new BusinessException("Email already exist");
        }        
        public void UserEmailShouldExistWhenLoggedIn(string email)
        {
            if (email == null) throw new BusinessException("Email does not exist");
        }
        public void VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            bool control = HashingHelper.VerifyPasswordHash(password, passwordHash, passwordSalt);
            if (!control) throw new BusinessException("Wrong password");
        }
    }
}
