using Domain.Events;
using Domain.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using HashCode = Domain.Models.ValueObjects.HashCode;

namespace Domain.Models
{
    public class User : BaseEntity<int>
    {
        public Login Login { get; private set; }

        public Email Email { get; private set; }

        public PhoneNumber PhoneNumber { get; private set; }

        public DateTime CreateDate { get; private set; }

        public DateTime? UpdateDate { get; private set; }

        public HashCode Password { get; private set; }

        private List<File> _files;
        public IReadOnlyCollection<File> Files => _files.AsReadOnly();

        private User()
            :base()
        {
            _files = new List<File>();
        }

        public User(Login login, Email email, PhoneNumber phoneNumber, ValueObjects.HashCode password)
            :base()
        {
            Login = login;
            Email = email;
            Password = password;
            PhoneNumber = phoneNumber;
        }

        /// <summary>
        /// Решить проблему с дубликатами!!
        /// </summary>
        /// <param name="file"></param>
        public void AddFile(File file)
        {
            _files.Add(file);
        }

        public void AddFiles(ICollection<File> files)
        {
            foreach(var file in files)
            {
                _files.Add(file);
            }
        }

        public void RemoveAllFiles()
        {
            _files.Clear();
        }

        public ICollection<Image> GetAllImages()
        {
            return _files.OfType<Image>()
                .ToList();
        }

        public Image GetUserImageById(int id)
        {
            return (Image)_files.FirstOrDefault(x => x.Id == id);
        }
    }
}
