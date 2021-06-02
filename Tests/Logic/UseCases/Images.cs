using DAL;
using Logic.Dto;
using Logic.Operations.Handlers;
using Logic.Operations.Requests;
using Logic.Services.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tests.Logic.UseCases
{
    [TestFixture]
    public class Images
    {
        private Mock<IFileShareImagesService> _fileShare;
        private Mock<Func<DatabaseContext>> _context;
        private Mock<Serilog.ILogger> _logger;


        [SetUp]
        public void Setup()
        {
            _fileShare = new Mock<IFileShareImagesService>();
            _context = new Mock<Func<DatabaseContext>>();
            _logger = new Mock<Serilog.ILogger>();
        }

        [Test]
        public void AddImageRequestHanderTest()
        {
            ///Arrange
            var itemRequest = new AddImgeRequest(1, "test.doc", new MemoryStream());
            var operationResult = new OperationResult<string>(true, string.Empty, "test.doc");
            _fileShare.Setup(x => x.SaveFileAsync(It.IsAny<string>(), It.IsAny<int>(), new MemoryStream()))
                .Returns(Task.FromResult(operationResult));
                
            ///Act
            var handler = new AddImageRequestHandler(_context.Object, _logger.Object, _fileShare.Object);
            var result = handler.Handle(itemRequest, CancellationToken.None).Result;
        }
    }
}
