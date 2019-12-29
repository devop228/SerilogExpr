using System;
namespace SerilogExpr {
    public class CustomException : Exception
    {
        public CustomException(string message) : base(message) { }
        public CustomException(string message, Exception ex) : base(message, ex) { }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid TrackId { get; set; } = Guid.NewGuid();
    }
}