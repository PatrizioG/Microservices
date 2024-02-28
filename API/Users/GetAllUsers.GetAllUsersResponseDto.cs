using API.CommonDtos;

namespace API.Users;

public class GetAllUsersResponseDto
{
    public List<UserDto> Users { get; set; } = [];
}