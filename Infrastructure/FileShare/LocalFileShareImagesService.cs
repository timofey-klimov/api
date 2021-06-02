using ApplicationSettings.Settings;
using Logic.Dto;
using Logic.Services.Interfaces;
using Microsoft.Extensions.Options;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Infrastructure.FileShare
{
    public class LocalFileShareImagesService : IFileShareImagesService
    {
        private readonly string _path;
        private readonly ILogger _logger;
        public LocalFileShareImagesService(IOptions<ImageBaseFolderSettings> settings, ILogger logger)
        {
            _logger = logger;
            _path = settings?.Value?.ImageFolderPath ?? throw new ArgumentNullException("ImageBaseFolderSettings");
        }

        public async Task<OperationResult<string>> SaveFileAsync(string name, int userId, Stream body)
        {
            try
            {
                var userFolderPath = @$"{_path}\{userId}";

                if (!Directory.Exists(userFolderPath))
                {
                    Directory.CreateDirectory(userFolderPath);
                }


                var userImagePath = @$"{userFolderPath}\{name}";
                using (var fileStream = System.IO.File.Create(userImagePath))
                {
                    await body.CopyToAsync(fileStream);
                }

                return OperationResult<string>.Success(userImagePath);
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message);
                return OperationResult<string>.Fail("LocalFileShareService fail");
            }
        }
    }
}
