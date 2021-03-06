using System;

namespace Domain.Models
{
    public class LogEntry : BaseEntity<long>
    {
        public DateTime CreateDate { get; private set; }

        public string Request { get; private set; }

        public int Code { get; private set; }

        public int? UserId { get; private set; }

        public string Response { get; private set; }

        private LogEntry()
            :base()
        {

        }

        public LogEntry(string request, int code, int? userId, string response)
        {
            Request = request;
            Code = code;
            UserId = userId;
            Response = response;
        }
    }
}
