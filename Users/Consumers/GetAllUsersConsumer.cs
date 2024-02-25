using Common.Contracts;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Users.Models;

namespace Users.Consumers
{
    public class GetAllUsersConsumer : IConsumer<GetAllUsers>
    {
        private readonly UsersDbContext _userContext;
        private readonly ILogger<GetAllUsersConsumer> _logger;

        public GetAllUsersConsumer(UsersDbContext context, ILogger<GetAllUsersConsumer> logger)
        {
            _userContext = context;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<GetAllUsers> context)
        {
            _logger.LogInformation("GetAllUsers consumed");

            var users = await _userContext.Users.ToListAsync() ?? [];
            await context.RespondAsync(new GetAllUsersResult
            {
                Users = users
                .Select(x => new Common.Contracts.User
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    Surname = x.Surname

                }).ToList()
            });
        }
    }
}
