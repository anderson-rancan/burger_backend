using System;
using System.Collections.Generic;
using BurgerBackend.Identity.Interface.Exceptions;
using BurgerBackend.Identity.Interface.Services.Models;
using IdentityService.Domain;
using IdentityService.Repositories;

namespace BurgerBackend.Identity.Service.Services
{
    internal sealed class UserService : IUserService
    {
        private readonly ITokenService _tokenService;
        private readonly IUserInMemoryRespository _repository;

        public UserService(ITokenService tokenService, IUserInMemoryRespository userInMemoryRespository)
        {
            _tokenService = tokenService;
            _repository = userInMemoryRespository;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest request)
        {
            var user = _repository.GetUserByEmail(request.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                throw new BugerBackendIdentityException("User email or password is incorrect.");
            }

            // TODO implement a mapper
            return new AuthenticateResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                JwtToken = _tokenService.GenerateToken(user)
            };
        }

        public Guid? ValidateToken(string token)
        {
            var userId = _tokenService.ValidateToken(token);

            if (userId != null)
            {
                var user = _repository.GetUserById(userId.Value);
                if (user != null)
                {
                    return user.Id;
                }
            }

            return null;
        }

        public GetUserResponse GetById(Guid id)
        {
            var user = _repository.GetUserById(id);

            if (user == null)
            {
                throw new KeyNotFoundException("User does not exist.");
            }

            // TODO implement a mapper
            return new GetUserResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
        }

        public void Register(RegisterRequest request)
        {
            if (_repository.GetUserByEmail(request.Email) != null)
                throw new BugerBackendIdentityException("Email already exists.");

            // TODO implement a mapper
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            _repository.SaveOrUpdateUser(user);
        }

        public void Update(Guid id, UpdateRequest request)
        {
            var user = _repository.GetUserById(id);

            if (!string.Equals(request.Email, user.Email, StringComparison.OrdinalIgnoreCase)
                && _repository.GetUserByEmail(request.Email) != null)
            {
                throw new BugerBackendIdentityException("Email already exists.");
            }

            if (!string.IsNullOrWhiteSpace(request.Password))
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            }

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;

            _repository.SaveOrUpdateUser(user);
        }

        public void Delete(Guid id) => _repository.DeleteUser(id);
    }
}
