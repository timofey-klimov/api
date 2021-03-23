using System;

namespace Domain.Models
{
    public class LogEntry : BaseEntity<int>
    {

        public int? UserId { get; private set; }
        public string Request { get; private set; }
        public DateTime CreateDate { get; private set; }

        public int Code { get; private set; }
        public string Response { get; private set; }

        private LogEntry()
            :base()
        {

        }

        public LogEntry(int? userId, string request, string response, int code)
        {
            UserId = userId;
            Request = request;
            Response = response;
            Code = code;
            CreateDate = DateTime.Now;
        }
    }
}
