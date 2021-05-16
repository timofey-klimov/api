namespace Domain.Models
{
    public class NotificationTemplate : BaseEntity<int>
    {
        public byte NotificationType { get; private set; }

        public string NotificationMessage { get; private set; }

        private NotificationTemplate()
        {

        }

        public NotificationTemplate(byte notificationType, string notificationMessage)
        {
            NotificationType = notificationType;
            NotificationMessage = notificationMessage;
        }
    }
}
