using System;
using System.Collections;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Session : BaseEntity<int>
    {
        public string Token { get; private set; }

        public DateTime CreateDate { get; private set; }

        public DateTime ExpireDate { get; private set; }

        public virtual ICollection<User> Users { get; private set; }
        
        private Session()
            :base()
        {

        }
    }
}
