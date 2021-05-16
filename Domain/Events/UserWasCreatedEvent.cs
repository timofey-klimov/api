namespace Domain.Events
{
    public class UserWasCreatedEvent : BaseEvent
    {
        public string Email { get; private set; }

        public UserWasCreatedEvent(string email)
        {
            Email = email;
        }
    }
}
