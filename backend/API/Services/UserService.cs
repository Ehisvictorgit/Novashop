using API.DTOs;
using Core.Interfaces;
using MongoDB.Bson;

namespace API.Services;

public class UserDTOService
{
    private readonly IUserRepository _userRepository;

    public UserDTOService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDTO> GetByIdAsync(ObjectId userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user is null)
            return null;

        var userDTO = new UserDTO
        {
            Id = user.Id,
            User = user.Usr,
        };

        return userDTO;
    }

    public async Task<IEnumerable<UserDTO>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();

        if (users is null || !users.Any())
            return [];

        var usersDTO = new List<UserDTO>();

        foreach (var user in users)
        {
            var userDTO = new UserDTO
            {
                Id = user.Id,
                User = user.Usr
            };

            usersDTO.Add(userDTO);
        }

        return usersDTO;
    }
}
