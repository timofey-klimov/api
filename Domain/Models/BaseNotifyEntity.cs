using Domain.Events;
using System.Collections.Generic;

namespace Domain.Models
{
    public class BaseNotifyEntity
    {
        public ICollection<BaseEvent> Events { get; private set; }

        protected BaseNotifyEntity()
        {
            Events = new List<BaseEvent>();
        }

        public void ClearEvents()
        {
            Events.Clear();
        }
    }
}
