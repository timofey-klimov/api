using Domain.Models.ValueObjects;

namespace Domain.Models
{
    public class Image : File
    {
        public string Extension { get; private set; }
        protected Image()
        {

        }
        public Image(FileName name, FilePath filePath, int userId, string extension, HashCode hashCode) 
            :base(name, filePath, userId, hashCode)
        {
            Extension = extension;
        }
    }
}
