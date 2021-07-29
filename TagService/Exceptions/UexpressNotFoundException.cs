using System;

namespace TagService.Exceptions
{
    public class UexpressNotFoundException : Exception
    {
        public UexpressNotFoundException() { }

        public UexpressNotFoundException(string message) : base(message) { }

        public UexpressNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
