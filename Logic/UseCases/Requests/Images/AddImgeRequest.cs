using MediatR;
using System.IO;

namespace Logic.Operations.Requests
{
    public class AddImgeRequest : IRequest
    {
        public int UserId { get; private set; }

        public string FileName { get; private set; }


        public Stream Body { get; private set; }

        public AddImgeRequest(int userId, string fileName, Stream body)
        {
            UserId = userId;
            FileName = fileName;
            Body = body;
        }
    }
}
