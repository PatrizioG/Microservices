using Common.Contracts;
using MassTransit;
using Users.Mappers;
using Users.Models;

namespace Users.Consumers;

public class GetUserByIdConsumer : IConsumer<GetUserById>
{
    private readonly UsersDbContext _usersContext;
    private readonly ILogger<GetUserByIdConsumer> _logger;

    public GetUserByIdConsumer(UsersDbContext context, ILogger<GetUserByIdConsumer> logger)
    {
        _usersContext = context;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<GetUserById> context)
    {
        _logger.LogInformation("GetUserById consumed");

        var user = await _usersContext.Users.FindAsync(context.Message.Id);

        if (user == null)
            throw new ArgumentException("User not found");

        await context.RespondAsync(UserMapper.MapUser(user));
    }
}