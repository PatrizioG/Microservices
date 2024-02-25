using Common.Contracts;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Users.Mappers;
using Users.Models;

namespace Users.Consumers
{
    public class GetAllUsersConsumer : IConsumer<GetAllUsers>
    {
        private readonly UsersDbContext _usersContext;
        private readonly ILogger<GetAllUsersConsumer> _logger;

        public GetAllUsersConsumer(UsersDbContext context, ILogger<GetAllUsersConsumer> logger)
        {
            _usersContext = context;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<GetAllUsers> context)
        {
            _logger.LogInformation("GetAllUsers consumed");

            var users = await _usersContext.Users.ToListAsync() ?? [];

            await context.RespondAsync(new GetAllUsersResult
            {
                Users = users.Select(x => UserMapper.MapUser(x)).ToList()
            });
        }
    }
}
