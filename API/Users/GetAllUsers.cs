using Common.Contracts;
using FastEndpoints;
using MassTransit;

namespace API.Users
{
    public class GetAllUsers : EndpointWithoutRequest<GetAllUsersResponseDto>
    {
        private readonly IRequestClient<Common.Contracts.GetAllUsers> _requestClient;

        public GetAllUsers(IRequestClient<Common.Contracts.GetAllUsers> requestClient)
        {
            _requestClient = requestClient;
        }

        public override void Configure()
        {
            Get("users");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken cancellationToken)
        {
            var response = await _requestClient.GetResponse<GetAllUsersResult>(new { }, cancellationToken);
            var allUsers = response.Message;

            await SendAsync(new GetAllUsersResponseDto()
            {
                Users = allUsers.Users.Select(x => new CommonDtos.UserDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Surname = x.Surname,
                    Email = x.Email,

                }).ToList(),
            });
        }
    }
}
