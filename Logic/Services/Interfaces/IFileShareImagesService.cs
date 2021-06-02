using Logic.Dto;
using System.IO;
using System.Threading.Tasks;

namespace Logic.Services.Interfaces
{
    public interface IFileShareImagesService
    {
        public Task<OperationResult<string>> SaveFileAsync(string name, int userId, Stream body);
    }
}
