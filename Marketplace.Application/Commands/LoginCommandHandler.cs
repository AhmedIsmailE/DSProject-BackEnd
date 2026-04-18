using System;
using System.Threading.Tasks;
using MarketPlace.Application.DTOs;
using MarketPlace.Domain.Repositories;

namespace MarketPlace.Application.Commands
{
    public class LoginCommandHandler
    {
        private readonly IUserRepository _userRepository;

        public LoginCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<JsonEnvelope> HandleAsync(JsonEnvelope request)
        {
            // TODO:
            // 1. Deserialize request.Payload to extract Username and Password.
            // 2. Await _userRepository.GetByUsernameAsync(username).
            // 3. If user is null OR password hash doesn't match, return an "UNAUTHORIZED" JsonEnvelope.
            // 4. If successful, generate a session token (or just return the User details for now).
            // 5. Construct and return a "LOGIN_SUCCESS" JsonEnvelope.

            return new JsonEnvelope
            {
                CorrelationId = request.CorrelationId,
                Command = "LOGIN_FAILED",
                Payload = "{\"message\": \"Not implemented yet\"}"
            };
        }
    }
}