using Domain.Models.ValueObjects;
using System;
using HashCode = Domain.Models.ValueObjects.HashCode;

namespace Domain.Models
{
    public abstract class File : BaseEntity<int>
    {
        public FileName FileName { get; private set; }
        
        public DateTime CreateDate { get; private set; }

        public FilePath FilePath { get; private set; }

        public int UserId { get; private set; }

        public HashCode HashCode { get; private set; }

        public User User { get; private set; }

        protected File()
        {

        }

        public File(FileName name, FilePath filePath, int userId, HashCode hashCode)
        {
            FileName = name;
            FilePath = filePath;
            CreateDate = DateTime.Now;
            UserId = userId;
            HashCode = hashCode;
        }

    }
}
