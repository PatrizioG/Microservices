using Users.Models;

namespace Users.Mappers;

internal static class UserMapper
{
    public static Common.Contracts.User MapUser(UserEntity userEntity)
    {
        return new Common.Contracts.User
        {
            Id = userEntity.Id,
            Name = userEntity.Name,
            Email = userEntity.Email,
            Surname = userEntity.Surname
        };
    }
}