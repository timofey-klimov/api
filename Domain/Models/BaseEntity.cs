using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class BaseEntity<T> : BaseNotifyEntity
    {
        public T Id { get; private set; }

        protected BaseEntity()
            :base()
        {

        }
    }
}
