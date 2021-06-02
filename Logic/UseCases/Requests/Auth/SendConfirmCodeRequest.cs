using MediatR;

namespace Logic.Operations.Requests
{
    public class SendConfirmCodeRequest : IRequest
    {
        public string Email { get; private set; }

        public int UserId { get; private set; }

        public SendConfirmCodeRequest(string email, int userId)
        {
            Email = email;
            UserId = userId;
        }
    }
}
