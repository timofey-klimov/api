using ApplicationSettings.Settings;
using DAL;
using Domain.Models;
using Logic.Exceptions;
using Logic.Operations.Base;
using Logic.Operations.Requests;
using Logic.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Utils.Encription;

namespace Logic.Operations.Handlers
{
    public class AddImageRequestHandler : OperationBase<AddImgeRequest, Unit>
    {
        private readonly IFileShareImagesService _fileShareService;
        public AddImageRequestHandler(Func<DatabaseContext> dbCreator, ILogger logger, IFileShareImagesService fileShareService) 
            :base(dbCreator, logger)
        {
            _fileShareService = fileShareService ?? throw new ArgumentNullException("IFileShareImagesService");
        }
     
        protected override async Task<Unit> HandleCore(AddImgeRequest request, CancellationToken token)
        {
            var result = await _fileShareService.SaveFileAsync(request.FileName, request.UserId, request.Body);

            if (!result.IsSuccess)
                throw new OperationFailedException(result.ErrorMessage);

            var path = result.Data;
            var extension = GetExtensionFromFileName(request.FileName);
            var hash = GetHashCode(request.Body);
            var image = new Image(request.FileName, path, request.UserId, extension, hash);

            using (var dbContext = DbCreator())
            {
                var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
                user.AddFile(image);
                await dbContext.SaveChangesAsync();
            }

            return Unit.Value;
        }

        private string GetExtensionFromFileName(string name)
        {
            var lastIndexOfPoint = name.LastIndexOf('.');
            return name.Substring(lastIndexOfPoint, name.Length);
        }

        private byte[] GetHashCode(Stream stream)
        {
            return Encription.ComputeSha256Hash(stream);
        }
    }
}
